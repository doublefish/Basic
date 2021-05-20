using Adai.Base;
using Adai.Standard.Model;
using Basic.DAL;
using Basic.Model;
using Basic.Model.PageArg;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Basic.BLL
{
	/// <summary>
	/// OrderBLL
	/// </summary>
	public class OrderBLL : BLL<Order, OrderArg<Order>>
	{
		/// <summary>
		/// Dal
		/// </summary>
		readonly OrderDAL Dal;

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="loginInfo"></param>
		public OrderBLL(Token<TokenData> loginInfo = null) : base(loginInfo)
		{
			IDal = Dal = new OrderDAL();
		}

		/// <summary>
		/// 验证状态
		/// </summary>
		/// <param name="status"></param>
		/// <returns></returns>
		public override bool ValidateStatus(int status)
		{
			return ConfigIntHelper<Model.Config.Order.Status>.ContainsKey(status);
		}

		/// <summary>
		/// 验证
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public override string Validate(Order data)
		{
			if (data.ProductId < 1)
			{
				return "产品Id无效。";
			}
			if (string.IsNullOrEmpty(data.Mobile))
			{
				return "手机号码不能为空。";
			}
			if (!ValidateStatus(data.Status))
			{
				return "状态标识无效。";
			}
			data.UpdateTime = DateTime.Now;
			if (data.Id == 0)
			{
				data.Number = string.Format("P{0}", CommonHelper.GenerateNumber("ProductOrder"));
				data.AccountId = LoginInfo.Id;
				var product = new ProductBLL().Get(data.ProductId, true);
				data.OriginalPrice = product.Price;
				//折扣信息
				var discount = new ProductDiscountBLL().Get(data.ProductId, data.UpdateTime);
				if (discount != null && discount.IsAvailable)
				{
					data.DiscountId = discount.Id;
					data.DiscountPrice = discount.Rate > decimal.Zero ? data.OriginalPrice * discount.Rate : data.OriginalPrice - discount.Amount;
					data.DiscountInfo = string.Format("{0},{1},{2},{3}", discount.Id, discount.Name, discount.Rate, discount.Amount);
				}
				data.Status = Model.Config.Order.Status.Submitted;
				data.CreateTime = data.UpdateTime;
			}
			return base.Validate(data);
		}

		/// <summary>
		/// 修改状态
		/// </summary>
		/// <param name="id"></param>
		/// <param name="status"></param>
		public override void UpdateStatus(int id, int status)
		{
			if (!ValidateStatus(status))
			{
				throw new CustomException("状态标识无效。");
			}
			var data = new Order()
			{
				Id = id,
				Status = status
			};
			Dal.Update(data, new string[] { "Status" });
		}

		/// <summary>
		/// 关联数据
		/// </summary>
		/// <param name="list"></param>
		public override void List(ICollection<Order> list)
		{
			var productIds = list.Select(o => o.ProductId).Distinct().ToArray();
			var products = new ProductBLL().ListByPks(productIds);
			var accountIds = list.Select(o => o.AccountId).Distinct().ToArray();
			var accounts = new AccountBLL().ListByPks(accountIds, true);
			foreach (var data in list)
			{
				data.Product = products.Where(o => o.Id == data.ProductId).FirstOrDefault();
				data.Account = accounts.Where(o => o.Id == data.AccountId).FirstOrDefault();
			}
		}

		/// <summary>
		/// 验证权限
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public override bool VerifyRight(Order data)
		{
			return data.AccountId == LoginInfo.Id;
		}

		#region 扩展

		/// <summary>
		/// 修改价格
		/// </summary>
		/// <param name="id"></param>
		/// <param name="adultPrice">成人价格</param>
		/// <param name="childPrice">儿童价格</param>
		/// <param name="note">说明</param>
		public void UpdatePrice(int id, decimal adultPrice, decimal childPrice, string note)
		{
			if (adultPrice <= decimal.Zero)
			{
				throw new CustomException("成人价格无效。");
			}
			if (childPrice <= decimal.Zero)
			{
				throw new CustomException("儿童价格无效。");
			}
			var result = Get(id, true);
			if (!result.IsSubmitted)
			{
				throw new CustomException("当前状态不可执行此操作。");
			}
			result.AdultPrice = adultPrice;
			result.ChildPrice = childPrice;
			result.TotalPrice = result.AdultPrice * result.Adults + result.ChildPrice * result.Children;
			result.UserId = LoginInfo.Id;
			result.Note = note;
			Dal.Update(result, new string[] { "AdultPrice", "ChildPrice", "TotalPrice", "Note" });
		}

		#endregion

		#region 业务

		/// <summary>
		/// 计算佣金
		/// </summary>
		/// <param name="id"></param>
		/// <param name="orderCommission"></param>
		/// <param name="accountCommission"></param>
		private Order CalcCommission(int id, out OrderCommission orderCommission)
		{
			var result = Get(id, true);
			if (!result.IsSubmitted)
			{
				throw new CustomException("订单已处理。");
			}
			if (!result.TotalPrice.HasValue || result.TotalPrice.Value <= decimal.Zero)
			{
				throw new CustomException("订单成交价格无效。");
			}

			orderCommission = new OrderCommission();

			//读取推广记录
			var accountPromotion = new AccountPromotionBLL().GetByAccountId(result.AccountId, true);
			if (accountPromotion != null)
			{
				//读取佣金规则
				var commissionRule = new CommissionRuleBLL().Get(result.ProductId, result.CreateTime.Year, result.CreateTime.Month);
				if (commissionRule == null || !commissionRule.IsEnabled)
				{
					throw new CustomException("佣金规则未设置。");
				}

				//用户佣金
				var accountCommission = new AccountCommission()
				{
					AccountId = accountPromotion.PromoterId,
					OrderId = result.Id,
					Status = Model.Config.Commission.Status.Paid,
					CreateTime = DateTime.Now,
					UpdateTime = DateTime.Now
				};
				if (commissionRule.Rate > decimal.Zero)
				{
					accountCommission.Rate = commissionRule.Rate;
					accountCommission.Amount = result.TotalPrice.Value * commissionRule.Rate;
				}
				else if (commissionRule.Amount > decimal.Zero)
				{
					accountCommission.Amount = commissionRule.Amount;
				}
				else
				{
					throw new CustomException("个人佣金规则未设置。");
				}
				//保留两位小数
				accountCommission.Amount = decimal.Round(accountCommission.Amount, 2);
				orderCommission.AccountPromotion = accountPromotion;
				orderCommission.AccountCommission = accountCommission;
			}
			return result;
		}

		/// <summary>
		/// 完成
		/// </summary>
		/// <param name="id"></param>
		public void Complete(int id)
		{
			var result = CalcCommission(id, out var orderCommission);
			result.UserId = LoginInfo.Id;
			result.Status = Model.Config.Order.Status.Completed;
			result.UpdateTime = DateTime.Now;
			Dal.Complete(result, orderCommission.AccountPromotion?.Id, orderCommission.AccountCommission);
		}

		/// <summary>
		/// 完成预览
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public OrderCommission CompletePreview(int id)
		{
			CalcCommission(id, out var orderCommission);
			if (orderCommission.AccountPromotion != null)
			{
				//读取关联数据
				new AccountPromotionBLL(LoginInfo).List(new List<AccountPromotion>() { orderCommission.AccountPromotion });
			}
			return orderCommission;
		}

		#endregion
	}
}
