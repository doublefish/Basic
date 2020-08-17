using Adai.Standard;
using Adai.Standard.Models;
using Basic.DAL;
using Basic.Model;
using Basic.Model.PageArg;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Basic.BLL
{
	/// <summary>
	/// AgentUserCommissionBLL
	/// </summary>
	public class AgentUserCommissionBLL : BLL<AgentUserCommission, CommissionArg<AgentUserCommission>>
	{
		/// <summary>
		/// Dal
		/// </summary>
		readonly AgentUserCommissionDAL Dal;

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="loginInfo"></param>
		public AgentUserCommissionBLL(Token<TokenData> loginInfo = null) : base(loginInfo)
		{
			IDal = Dal = new AgentUserCommissionDAL();
		}

		/// <summary>
		/// 验证状态
		/// </summary>
		/// <param name="status"></param>
		/// <returns></returns>
		public override bool ValidateStatus(int status)
		{
			return ConfigIntHelper<Model.Config.StatusOfPayment>.ContainsKey(status);
		}

		/// <summary>
		/// 验证
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public override string Validate(AgentUserCommission data)
		{
			if (data.Id == 0)
			{
				var agent = new AgentBLL().Get(data.AgentId, true);
				if (agent == null)
				{
					return "代理商不存在。";
				}
				var agentUser = new AgentUserBLL().Get(data.AgentUserId, true);
				if (agentUser == null)
				{
					return "代理商用户不存在。";
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
			var data = new AgentUserCommission()
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
		public override void List(ICollection<AgentUserCommission> list)
		{
			var agentIds = new HashSet<int>();
			var agentUserIds = new HashSet<int>();
			var productOrderIds = new HashSet<int>();
			var orderIds = new HashSet<int>();
			foreach (var data in list)
			{
				agentIds.Add(data.AgentId);
				agentUserIds.Add(data.AgentUserId);
				orderIds.Add(data.OrderId);
			}
			var agents = new AgentBLL().ListByPks(agentIds, true);
			var agentUsers = new AgentUserBLL().ListByPks(agentUserIds, true);
			var orders = new OrderBLL().ListByPks(orderIds, true);
			foreach (var data in list)
			{
				data.Agent = agents.Where(o => o.Id == data.AgentId).FirstOrDefault();
				data.AgentUser = agentUsers.Where(o => o.Id == data.AgentUserId).FirstOrDefault();
				var order = orders.Where(o => o.Id == data.OrderId).FirstOrDefault();
				if (order != null)
				{
					data.OrderNumber = order.Number;
					data.OrderAmount = order.TotalPrice ?? decimal.Zero;
				}
			}
		}

		/// <summary>
		/// 验证权限
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public override bool VerifyRight(AgentUserCommission data)
		{
			return AgentBLL.CheckRight(LoginInfo, data.AgentId, data.AgentUserId);
		}

		#region 扩展

		#endregion

		#region 业务

		/// <summary>
		/// 计算佣金
		/// </summary>
		/// <param name="id"></param>
		/// <param name="commission"></param>
		private AgentUserCommission CalcCommission(int id, out AgentUserCommission commission)
		{
			var result = Get(id, true);
			if (!result.IsPaid)
			{
				throw new CustomException("订单已处理。");
			}

			var order = new OrderBLL().Get(result.OrderId, true);
			if (!order.TotalPrice.HasValue || order.TotalPrice.Value <= decimal.Zero)
			{
				throw new CustomException("订单成交价格无效。");
			}

			//读取佣金规则
			var commissionRule = new AgentCommissionRuleBLL(LoginInfo).Get(order.ProductId, order.CreateTime.Year, order.CreateTime.Month);
			if (commissionRule == null || !commissionRule.IsEnabled)
			{
				throw new CustomException("佣金规则未设置。");
			}

			//读取推广记录
			var accountPromotion = new AccountPromotionBLL().GetByAccountId(order.AccountId, true);
			if (accountPromotion == null || !accountPromotion.AgentUserId.HasValue)
			{
				throw new CustomException("没有找到关联的业务员。");
			}

			commission = new AgentUserCommission()
			{
				AgentId = accountPromotion.AgentId.Value,
				AgentUserId = accountPromotion.AgentUserId.Value,
				OrderId = result.OrderId,
				Status = Model.Config.Commission.Status.Distributed,
				CreateTime = DateTime.Now,
				UpdateTime = DateTime.Now
			};

			if (commissionRule.Rate > decimal.Zero)
			{
				commission.Rate = commissionRule.Rate;
				commission.Amount = order.TotalPrice.Value * commissionRule.Rate;
			}
			else if (commissionRule.Amount > decimal.Zero)
			{
				commission.Amount = commissionRule.Amount;
			}
			else
			{
				throw new CustomException("代理佣金规则未设置。");
			}
			//保留两位小数
			commission.Amount = decimal.Round(commission.Amount, 2);
			return result;
		}

		/// <summary>
		/// 分配
		/// </summary>
		/// <param name="id"></param>
		public void Distribute(int id)
		{
			var result = CalcCommission(id, out var commission);
			result.AgentUserId = LoginInfo.Id;
			result.Status = Model.Config.Commission.Status.Distributed;
			result.UpdateTime = DateTime.Now;
			Dal.Distribute(result, commission);
		}

		/// <summary>
		/// 分配预览
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public AgentUserCommission DistributePreview(int id)
		{
			CalcCommission(id, out var commission);
			if (commission != null)
			{
				//读取关联数据
				List(new List<AgentUserCommission>() { commission });
			}
			return commission;
		}

		#endregion
	}
}
