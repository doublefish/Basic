using Adai.Standard;
using Adai.Standard.Models;
using Basic.DAL;
using Basic.Model;
using System.Collections.Generic;
using System.Linq;

namespace Basic.BLL
{
	/// <summary>
	/// AgentMenuBLL
	/// </summary>
	public class AgentMenuBLL : TreeBLL<AgentMenu>
	{
		/// <summary>
		/// Dal
		/// </summary>
		readonly AgentMenuDAL Dal;

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="loginInfo"></param>
		public AgentMenuBLL(Token<TokenData> loginInfo = null) : base(loginInfo)
		{
			IDal = Dal = new AgentMenuDAL();
		}

		/// <summary>
		/// 验证
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public override string Validate(AgentMenu data)
		{
			if (!ConfigIntHelper<Model.Config.Menu.Type>.ContainsKey(data.Type))
			{
				return "类型标识无效。";
			}
			return base.Validate(data);
		}

		#region 扩展

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="level"></param>
		/// <param name="isAdmin"></param>
		/// <param name="status"></param>
		/// <param name="useCache"></param>
		/// <returns></returns>
		public ICollection<AgentMenu> List(int level, bool isAdmin, int? status = null, bool useCache = false)
		{
			var results = Dal.List(level, isAdmin, useCache);
			if (status.HasValue)
			{
				results = results.Where(o => o.Status == status.Value).ToArray();
			}
			return results;
		}

		#endregion
	}
}
