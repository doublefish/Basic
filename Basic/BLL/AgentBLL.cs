using Adai.Standard.Models;
using Basic.DAL;
using Basic.Model;
using Basic.Model.PageArg;
using System.Collections.Generic;
using System.Linq;

namespace Basic.BLL
{
	/// <summary>
	/// AgentBLL
	/// </summary>
	public class AgentBLL : TreeBLL<Agent, AgentArg<Agent>>
	{
		/// <summary>
		/// Dal
		/// </summary>
		readonly AgentDAL Dal;

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="loginInfo"></param>
		public AgentBLL(Token<TokenData> loginInfo = null) : base(loginInfo)
		{
			IDal = Dal = new AgentDAL();
		}

		/// <summary>
		/// 验证
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public override string Validate(Agent data)
		{
			if (data.Id == 0)
			{
				if (LoginInfo.Data.AgentId > 0)
				{
					data.ParentId = LoginInfo.Data.AgentId;
					var parent = Dal.Get(data.ParentId, true);
					data.Level = parent.Level + 1;
				}
				else
				{
					data.Level = 1;
				}
			}
			data.Sequence = 99;
			return base.Validate(data);
		}

		/// <summary>
		/// 新增
		/// </summary>
		/// <param name="data"></param>
		/// <param name="username"></param>
		/// <param name="password"></param>
		public void Add(Agent data, string username, string password)
		{
			Add(data);
			var dataUser = new AgentUser()
			{
				AgentId = data.Id,
				Username = username,
				IsAdmin = true,
				Status = Model.Config.Status.Enabled
			};
			new AgentUserBLL().Add(dataUser, password);
		}

		/// <summary>
		/// 关联数据
		/// </summary>
		/// <param name="list"></param>
		public override void List(ICollection<Agent> list)
		{
			var parentIds = list.Where(o => o.ParentId > 0).Select(o => o.ParentId).Distinct().ToArray();
			var parents = ListByPks(parentIds, true);
			foreach (var data in list)
			{
				if (data.ParentId > 0)
				{
					data.Parent = parents.Where(o => o.Id == data.ParentId).FirstOrDefault();
				}
			}
		}

		/// <summary>
		/// 验证权限
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public override bool VerifyRight(Agent data)
		{
			return data.Id == LoginInfo.Data.AgentId || (LoginInfo.Data.IsAdminOfAgent && data.ParentId == LoginInfo.Data.AgentId);
		}

		/// <summary>
		/// 验证权限
		/// </summary>
		/// <param name="arg"></param>
		public override void VerifyRight(AgentArg<Agent> arg)
		{
			switch (LoginInfo.Data.Type)
			{
				case "AgentUser":
					arg.ParentId = LoginInfo.Data.AgentId;
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
		/// 是否存在相同编码
		/// </summary>
		/// <param name="id"></param>
		/// <param name="code"></param>
		/// <param name="parentId"></param>
		/// <param name="useCache"></param>
		/// <returns></returns>
		public bool ExistByCode(int id, string code, bool useCache = false)
		{
			return base.ExistByCode(id, code, LoginInfo.Data.AgentId, useCache);
		}

		/// <summary>
		/// 是否存在相同名称
		/// </summary>
		/// <param name="id"></param>
		/// <param name="name"></param>
		/// <param name="useCache"></param>
		/// <returns></returns>
		public bool ExistByName(int id, string name, bool useCache = false)
		{
			return base.ExistByName(id, name, LoginInfo.Data.AgentId, useCache);
		}

		#endregion

		/// <summary>
		/// 验证权限
		/// </summary>
		/// <param name="tokenData"></param>
		/// <param name="data"></param>
		public static void CheckRight(TokenData tokenData, Agent data)
		{
			if (data.Id != tokenData.AgentId && data.ParentId != tokenData.AgentId)
			{
				throw new CustomException("没有操作该数据的权限。");
			}
		}

		/// <summary>
		/// 验证权限
		/// </summary>
		/// <param name="loginInfo"></param>
		/// <param name="agentId"></param>
		/// <param name="agentUserId"></param>
		/// <returns></returns>
		public static bool CheckRight(Token<TokenData> loginInfo, int agentId, int agentUserId = 0)
		{
			var result = loginInfo.Data.Type == "AgentUser" && loginInfo.Data.AgentId == agentId;
			if (!loginInfo.Data.IsAdminOfAgent)
			{
				//不是管理员只能查看自己的信息
				result = loginInfo.Data.Id == agentUserId;
			}
			return result;
		}
	}
}
