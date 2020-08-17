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
			var agentIds = new HashSet<int>();
			var agentUserIds = new HashSet<int>();
			foreach (var data in list)
			{
				accountIds.Add(data.AccountId);
				if (data.PromoterId.HasValue)
				{
					accountIds.Add(data.PromoterId.Value);
				}
				if (data.AgentId.HasValue)
				{
					agentIds.Add(data.AgentId.Value);
				}
				if (data.AgentUserId.HasValue)
				{
					agentUserIds.Add(data.AgentUserId.Value);
				}
			}
			var accounts = new AccountBLL().ListByPks(accountIds, true);
			var agents = new AgentBLL().ListByPks(agentIds, true);
			var agentUsers = new AgentUserBLL().ListByPks(agentUserIds, true);
			foreach (var data in list)
			{
				data.Account = accounts.Where(o => o.Id == data.AccountId).FirstOrDefault();
				if (data.AgentId.HasValue)
				{
					data.Agent = agents.Where(o => o.Id == data.AgentId.Value).FirstOrDefault();
				}
				if (data.AgentUserId.HasValue)
				{
					data.AgentUser = agentUsers.Where(o => o.Id == data.AgentUserId.Value).FirstOrDefault();
				}
				if (data.PromoterId.HasValue)
				{
					data.Promoter = accounts.Where(o => o.Id == data.PromoterId.Value).FirstOrDefault();
				}
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
				case "AgentUser":
					arg.AgentId = LoginInfo.Data.AgentId;
					if (!LoginInfo.Data.IsAdminOfAgent)
					{
						arg.AgentUserId = LoginInfo.Id;
					}
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
				var agentUser = new AgentUserBLL().GetByPromoCode(promoCode);
				if (agentUser != null)
				{
					var data = new AccountPromotion()
					{
						AgentId = agentUser.AgentId,
						AgentUserId = agentUser.Id,
						CreateTime = DateTime.Now
					};
					data.UpdateTime = data.CreateTime;
					return data;
				}
				return null;
			}
			else if (personalId.HasValue)
			{
				var data = new AccountPromotion()
				{
					PromoterId = personalId,
					CreateTime = DateTime.Now
				};
				data.UpdateTime = data.CreateTime;
				//读取上级
				var parent = Dal.GetByAccountId(personalId.Value);
				if (parent != null)
				{
					data.AgentId = parent.AgentId;
					data.AgentUserId = parent.AgentUserId;
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
