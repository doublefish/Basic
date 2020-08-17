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
	/// AgentBankCardBLL
	/// </summary>
	public class AgentBankCardBLL : BLL<AgentBankCard, BankCardArg<AgentBankCard>>
	{
		/// <summary>
		/// Dal
		/// </summary>
		readonly AgentBankCardDAL Dal;

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="loginInfo"></param>
		public AgentBankCardBLL(Token<TokenData> loginInfo = null) : base(loginInfo)
		{
			IDal = Dal = new AgentBankCardDAL();
		}

		/// <summary>
		/// 验证
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public override string Validate(AgentBankCard data)
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
				var agent = new AgentUserBLL().Get(LoginInfo.Id, true);
				data.AgentId = agent.Id;
				data.CreateTime = data.UpdateTime;
			}
			return base.Validate(data);
		}

		/// <summary>
		/// 关联数据
		/// </summary>
		/// <param name="list"></param>
		public override void List(ICollection<AgentBankCard> list)
		{
			var agentIds = new HashSet<int>();
			var dictIds = new HashSet<int>();
			foreach (var data in list)
			{
				agentIds.Add(data.AgentId);
				dictIds.Add(data.BankId);
			}
			var agents = new AgentBLL().ListByPks(agentIds, true);
			var dicts = new DictBLL().ListByPks(dictIds, true);
			foreach (var data in list)
			{
				data.Agent = agents.Where(o => o.Id == data.AgentId).FirstOrDefault();
				data.BankName = dicts.GetName(data.BankId);
			}
		}

		/// <summary>
		/// 验证权限
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public override bool VerifyRight(AgentBankCard data)
		{
			return AgentBLL.CheckRight(LoginInfo, data.AgentId);
		}

		#region 扩展

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="agentId"></param>
		/// <returns></returns>
		public AgentBankCard GetByAgentId(int agentId)
		{
			return Dal.GetByAgentId(agentId);
		}

		#endregion
	}
}
