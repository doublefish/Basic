using Basic.Model;
using SqlSugar;

namespace Basic.DAL
{
	/// <summary>
	/// AgentUserPasswordDAL
	/// </summary>
	internal class AgentUserPasswordDAL : DAL<AgentUserPassword>
	{
		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="agentUserId"></param>
		/// <param name="type"></param>
		/// <returns></returns>
		public AgentUserPassword Get(int agentUserId, int type)
		{
			return Db.Queryable<AgentUserPassword>().Where(o => o.AgentUserId == agentUserId && o.Type == type).OrderBy(o => o.Id, OrderByType.Desc).First();
		}
	}
}
