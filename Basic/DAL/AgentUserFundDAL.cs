using Basic.Model;
using Basic.Model.PageArg;
using SqlSugar;

namespace Basic.DAL
{
	/// <summary>
	/// AgentUserFundDAL
	/// </summary>
	internal class AgentUserFundDAL : DAL<AgentUserFund>
	{
		/// <summary>
		/// 分页查询条件
		/// </summary>
		/// <param name="arg"></param>
		/// <param name="query"></param>
		/// <returns></returns>
		public override ISugarQueryable<AgentUserFund> Query(BaseArg<AgentUserFund> arg, ISugarQueryable<AgentUserFund> query)
		{
			//代理商Id
			if (arg.AgentId.HasValue)
			{
				query = query.Where(o => o.AgentId == arg.AgentId.Value);
			}
			//代理商名称
			if (!string.IsNullOrEmpty(arg.AgentName))
			{
				query = query.Where(o => SqlFunc.Subqueryable<Agent>().Where(a => a.Id == o.AgentId && a.Name.Contains(arg.AgentName)).Any());
			}
			//代理商用户Id
			if (arg.AgentUserId.HasValue)
			{
				query = query.Where(o => o.AgentUserId == arg.AgentUserId.Value);
			}
			//代理商用户名
			if (!string.IsNullOrEmpty(arg.AgentUsername))
			{
				query = query.Where(o => SqlFunc.Subqueryable<AgentUser>().Where(au => au.Id == o.AgentUserId && au.Username == arg.AgentUsername).Any());
			}
			//代理商用户手机号码
			if (!string.IsNullOrEmpty(arg.AgentUserMobile))
			{
				query = query.Where(o => SqlFunc.Subqueryable<AgentUser>().Where(au => au.Id == o.AgentUserId && au.Mobile == arg.AgentUserMobile).Any());
			}
			//编号
			if (!string.IsNullOrEmpty(arg.Number))
			{
				query = query.Where(o => o.Number == arg.Number);
			}
			//类型
			if (arg.Type.HasValue)
			{
				query = query.Where(o => o.Type == arg.Type.Value);
			}
			//开始时间
			if (arg.Start.HasValue)
			{
				var start = arg.Start.Value.Date;
				query = query.Where(o => o.CreateTime >= start);
			}
			//结束时间
			if (arg.End.HasValue)
			{
				var end = arg.End.Value.Date.AddDays(1);
				query = query.Where(o => o.CreateTime < end);
			}
			return base.Query(arg, query);
		}

		/// <summary>
		/// 分页查询排序
		/// </summary>
		/// <param name="query"></param>
		/// <param name="orderByField"></param>
		/// <param name="orderByType"></param>
		/// <returns></returns>
		public override ISugarQueryable<AgentUserFund> Sort(ISugarQueryable<AgentUserFund> query, string orderByField, OrderByType orderByType)
		{
			return orderByField switch
			{
				"AgentId" => query.OrderBy(o => o.AgentId, orderByType),
				"AgentUserId" => query.OrderBy(o => o.AgentUserId, orderByType),
				"Number" => query.OrderBy(o => o.Number, orderByType),
				"Type" => query.OrderBy(o => o.Type, orderByType),
				"Amount" => query.OrderBy(o => o.Amount, orderByType),
				"Balance" => query.OrderBy(o => o.Balance, orderByType),
				"Freeze" => query.OrderBy(o => o.Freeze, orderByType),
				"CreateTime" => query.OrderBy(o => o.CreateTime, orderByType),
				_ => query.OrderBy(o => o.Id, orderByType)
			};
		}
	}
}
