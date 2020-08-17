using Adai.Standard;
using Adai.Standard.Models;
using Basic.DAL;
using Basic.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Basic.BLL
{
	/// <summary>
	/// AgentUserMailBLL
	/// </summary>
	public class AgentUserMailBLL : BLL<AgentUserMail>
	{
		/// <summary>
		/// Dal
		/// </summary>
		readonly AgentUserMailDAL Dal;

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="loginInfo"></param>
		public AgentUserMailBLL(Token<TokenData> loginInfo = null) : base(loginInfo)
		{
			IDal = Dal = new AgentUserMailDAL();
		}

		/// <summary>
		/// 验证
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public override string Validate(AgentUserMail data)
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
			if (string.IsNullOrEmpty(data.Email))
			{
				return "电子邮箱不能为空。";
			}
			if (!ConfigIntHelper<Model.Config.Mail.Type>.ContainsKey(data.Type))
			{
				return "类型标识无效。";
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
			var data = new AgentUserMail()
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
		public override void List(ICollection<AgentUserMail> list)
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

		/// <summary>
		/// 验证权限
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public override bool VerifyRight(AgentUserMail data)
		{
			return AgentBLL.CheckRight(LoginInfo, data.AgentId, data.AgentUserId);
		}

		#region 扩展

		#endregion
	}
}
