using Adai.Standard.Models;
using Basic.DAL;
using Basic.Model;
using System.Collections.Generic;
using System.Linq;

namespace Basic.BLL
{
	/// <summary>
	/// AgentUserFundBLL
	/// </summary>
	public class AgentUserFundBLL : BLL<AgentUserFund>
	{
		/// <summary>
		/// Dal
		/// </summary>
		readonly AgentUserFundDAL Dal;

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="loginInfo"></param>
		public AgentUserFundBLL(Token<TokenData> loginInfo = null) : base(loginInfo)
		{
			IDal = Dal = new AgentUserFundDAL();
			if (Dal == null)
			{
				//为了编辑器不显示警告信息
			}
		}

		/// <summary>
		/// 关联数据
		/// </summary>
		/// <param name="list"></param>
		public override void List(ICollection<AgentUserFund> list)
		{
			var agentIds = new HashSet<int>();
			var agentUserIds = new HashSet<int>();
			foreach (var data in list)
			{
				agentIds.Add(data.AgentId);
				agentUserIds.Add(data.AgentUserId);
			}
			var agents = new AgentBLL().ListByPks(agentIds, true);
			var agentUsers = new AgentUserBLL().ListByPks(agentUserIds, true);
			foreach (var data in list)
			{
				data.Agent = agents.Where(o => o.Id == data.AgentId).FirstOrDefault();
				data.AgentUser = agentUsers.Where(o => o.Id == data.AgentUserId).FirstOrDefault();
			}
		}


		#region 扩展

		#endregion
	}
}
