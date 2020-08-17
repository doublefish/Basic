using Basic.Model;
using Basic.Model.PageArg;
using SqlSugar;

namespace Basic.DAL
{
	/// <summary>
	/// LogDAL
	/// </summary>
	internal class LogDAL : DAL<Log>
	{
		/// <summary>
		/// 分页查询条件
		/// </summary>
		/// <param name="arg"></param>
		/// <param name="query"></param>
		/// <returns></returns>
		public override ISugarQueryable<Log> Query(BaseArg<Log> arg, ISugarQueryable<Log> query)
		{
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
		public override ISugarQueryable<Log> Sort(ISugarQueryable<Log> query, string orderByField, OrderByType orderByType)
		{
			return orderByField switch
			{
				"Category" => query.OrderBy(o => o.Type, orderByType),
				"CreateTime" => query.OrderBy(o => o.CreateTime, orderByType),
				_ => query.OrderBy(o => o.Id, orderByType)
			};
		}
	}
}
