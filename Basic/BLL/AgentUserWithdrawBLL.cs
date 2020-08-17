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
	/// AgentUserWithdrawBLL
	/// </summary>
	public class AgentUserWithdrawBLL : BLL<AgentUserWithdraw, BankCardArg<AgentUserWithdraw>>
	{
		/// <summary>
		/// Dal
		/// </summary>
		readonly AgentUserWithdrawDAL Dal;

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="loginInfo"></param>
		public AgentUserWithdrawBLL(Token<TokenData> loginInfo = null) : base(loginInfo)
		{
			IDal = Dal = new AgentUserWithdrawDAL();
		}

		/// <summary>
		/// 验证
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public override string Validate(AgentUserWithdraw data)
		{
			var bank = new DictBLL().Get(data.BankId, true);
			if (bank == null)
			{
				return "银行不存在。";
			}
			if (string.IsNullOrEmpty(data.CardNumber))
			{
				return "卡号不能为空。";
			}
			if (string.IsNullOrEmpty(data.Cardholder))
			{
				return "持卡人不能为空。";
			}
			if (string.IsNullOrEmpty(data.Branch))
			{
				return "支行不能为空。";
			}
			data.UpdateTime = DateTime.Now;
			if (data.Id == 0)
			{
				data.AgentId = LoginInfo.Data.AgentId;
				data.AgentUserId = LoginInfo.Id;
				data.Status = Model.Config.StatusOfProcess.Applied;
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
			var data = new AgentUserWithdraw()
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
		public override void List(ICollection<AgentUserWithdraw> list)
		{
			var agentIds = new HashSet<int>();
			var agentUserIds = new HashSet<int>();
			var dictIds = new HashSet<int>();
			foreach (var data in list)
			{
				agentIds.Add(data.AgentId);
				agentUserIds.Add(data.AgentUserId);
				dictIds.Add(data.BankId);
			}
			var agents = new AgentBLL().ListByPks(agentIds, true);
			var agentUsers = new AgentUserBLL().ListByPks(agentUserIds, true);
			var dicts = new DictBLL().ListByPks(dictIds, true);
			foreach (var data in list)
			{
				data.Agent = agents.Where(o => o.Id == data.AgentId).FirstOrDefault();
				data.AgentUser = agentUsers.Where(o => o.Id == data.AgentUserId).FirstOrDefault();
				data.BankName = dicts.GetName(data.BankId);
			}
		}

		/// <summary>
		/// 验证权限
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public override bool VerifyRight(AgentUserWithdraw data)
		{
			return AgentBLL.CheckRight(LoginInfo, data.AgentId, data.AgentUserId);
		}

		#region 扩展

		#endregion
	}
}
