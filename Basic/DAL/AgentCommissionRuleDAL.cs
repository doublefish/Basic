using Basic.Model;
using Basic.Model.PageArg;
using SqlSugar;
using System.Collections.Generic;

namespace Basic.DAL
{
	/// <summary>
	/// AgentCommissionRuleDAL
	/// </summary>
	internal class AgentCommissionRuleDAL : DAL<AgentCommissionRule, CommissionRuleArg<AgentCommissionRule>>
	{
		/// <summary>
		/// 是否存在相同的记录
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public bool Exist(AgentCommissionRule data)
		{
			return Db.Queryable<AgentCommissionRule>().Any(o => o.AgentId == data.AgentId && o.ProductId == data.ProductId && o.Year == data.Year && o.Month == data.Month && o.Id != data.Id);
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="agentId"></param>
		/// <param name="productId"></param>
		/// <param name="year"></param>
		/// <param name="month"></param>
		/// <param name="status"></param>
		/// <returns></returns>
		public AgentCommissionRule Get(int agentId, int productId, int? year, int? month, int? status = null)
		{
			var query = Db.Queryable<AgentCommissionRule>().Where(o => o.AgentId == agentId && o.ProductId == productId);
			if (year.HasValue)
			{
				query = query.Where(o => o.Year == year);
			}
			if (month.HasValue)
			{
				query = query.Where(o => o.Month == month);
			}
			if (status.HasValue)
			{
				query = query.Where(o => o.Status == status.Value);
			}
			return Sort(query).First();
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="agentId"></param>
		/// <param name="productId"></param>
		/// <param name="status"></param>
		/// <returns></returns>
		public ICollection<AgentCommissionRule> List(int agentId, int productId, int? status = null)
		{
			var query = Db.Queryable<AgentCommissionRule>().Where(o => o.AgentId == agentId && o.ProductId == productId);
			if (status.HasValue)
			{
				query = query.Where(o => o.Status == status.Value);
			}
			return Sort(query).ToList();
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="agentId"></param>
		/// <param name="productIds"></param>
		/// <param name="status"></param>
		/// <returns></returns>
		public ICollection<AgentCommissionRule> List(int agentId, ICollection<int> productIds, int? status = null)
		{
			var query = Db.Queryable<AgentCommissionRule>().Where(o => o.AgentId == agentId && productIds.Contains(o.ProductId));
			if (status.HasValue)
			{
				query = query.Where(o => o.Status == status.Value);
			}
			return Sort(query).ToList();
		}

		/// <summary>
		/// 默认排序
		/// </summary>
		/// <param name="query"></param>
		/// <returns></returns>
		private ISugarQueryable<AgentCommissionRule> Sort(ISugarQueryable<AgentCommissionRule> query)
		{
			return query.OrderBy(o => o.Year, OrderByType.Desc).OrderBy(o => o.Month, OrderByType.Desc).OrderBy(o => o.Id, OrderByType.Desc);
		}

		/// <summary>
		/// 分页查询条件
		/// </summary>
		/// <param name="arg"></param>
		/// <param name="query"></param>
		/// <returns></returns>
		public override ISugarQueryable<AgentCommissionRule> Query(CommissionRuleArg<AgentCommissionRule> arg, ISugarQueryable<AgentCommissionRule> query)
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
			//产品Id
			if (arg.ProductId.HasValue)
			{
				query = query.Where(o => o.ProductId == arg.ProductId.Value);
			}
			//年份
			if (arg.Year.HasValue)
			{
				query = query.Where(o => o.Year == arg.Year.Value);
			}
			//月份
			if (arg.Month.HasValue)
			{
				query = query.Where(o => o.Month == arg.Month.Value);
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
		public override ISugarQueryable<AgentCommissionRule> Sort(ISugarQueryable<AgentCommissionRule> query, string orderByField, OrderByType orderByType)
		{
			return orderByField switch
			{
				"AgentId" => query.OrderBy(o => o.AgentId, orderByType),
				"ProductId" => query.OrderBy(o => o.ProductId, orderByType),
				"Year" => query.OrderBy(o => o.Year, orderByType).OrderBy(o => o.Month, orderByType),
				"Month" => query.OrderBy(o => o.Year, orderByType).OrderBy(o => o.Month, orderByType),
				"Rate" => query.OrderBy(o => o.Rate, orderByType),
				"Amount" => query.OrderBy(o => o.Amount, orderByType),
				"CreateTime" => query.OrderBy(o => o.CreateTime, orderByType),
				"UpdateTime" => query.OrderBy(o => o.UpdateTime, orderByType),
				_ => query.OrderBy(o => o.Id, orderByType)
			};
		}
	}
}
