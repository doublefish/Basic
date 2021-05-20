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
	/// AccountPromotionBLL
	/// </summary>
	public class AccountPromotionBLL : BLL<AccountPromotion, PromotionArg<AccountPromotion>>
	{
		/// <summary>
		/// Dal
		/// </summary>
		readonly AccountPromotionDAL Dal;

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="loginInfo"></param>
		public AccountPromotionBLL(Token<TokenData> loginInfo = null) : base(loginInfo)
		{
			IDal = Dal = new AccountPromotionDAL();
		}

		/// <summary>
		/// 验证
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public override string Validate(AccountPromotion data)
		{
			if (data.Id == 0)
			{
				var account = new AccountBLL().Get(data.AccountId, true);
				if (account == null)
				{
					return "用户不存在。";
				}
			}
			if (!ValidateStatus(data.Status))
			{
				return "状态标识无效。";
			}
			data.UpdateTime = DateTime.Now;
			if (data.Id == 0)
			{
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
			var data = new AccountPromotion()
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
		public override void List(ICollection<AccountPromotion> list)
		{
			var accountIds = new HashSet<int>();
			
			foreach (var data in list)
			{
				accountIds.Add(data.AccountId);
				accountIds.Add(data.PromoterId);
			}
			var accounts = new AccountBLL().ListByPks(accountIds, true);
			foreach (var data in list)
			{
				data.Account = accounts.Where(o => o.Id == data.AccountId).FirstOrDefault();
				data.Promoter = accounts.Where(o => o.Id == data.PromoterId).FirstOrDefault();
			}
		}

		/// <summary>
		/// 验证权限
		/// </summary>
		/// <param name="arg"></param>
		public override void VerifyRight(PromotionArg<AccountPromotion> arg)
		{
			switch (LoginInfo.Data.Type)
			{
				case "Account":
					arg.PromoterId = LoginInfo.Id;
					break;
				default: break;
			}
		}

		#region 扩展

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="accountId"></param>
		/// <param name="useCache"></param>
		/// <returns></returns>
		public AccountPromotion GetByAccountId(int accountId, bool useCache = false)
		{
			return Dal.GetByAccountId(accountId, useCache);
		}

		/// <summary>
		/// 生成推广关系
		/// </summary>
		/// <param name="promoCode"></param>
		/// <param name="personalId"></param>
		/// <returns></returns>
		public AccountPromotion Create(string promoCode = null, int? personalId = null)
		{
			//推广代码优先
			if (!string.IsNullOrEmpty(promoCode))
			{
				return null;
			}
			else if (personalId.HasValue)
			{
				var data = new AccountPromotion()
				{
					PromoterId = personalId.Value,
					CreateTime = DateTime.Now
				};
				data.UpdateTime = data.CreateTime;
				//读取上级
				var parent = Dal.GetByAccountId(personalId.Value);
				if (parent != null)
				{
				}
				return data;
			}
			else
			{
				return null;
			}
		}

		#endregion
	}
}
