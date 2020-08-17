using Basic.Model;
using Basic.Model.PageArg;
using SqlSugar;
using System.Collections.Generic;

namespace Basic.DAL
{
	/// <summary>
	/// CommissionRuleDAL
	/// </summary>
	internal class CommissionRuleDAL : DAL<CommissionRule, CommissionRuleArg<CommissionRule>>
	{
		/// <summary>
		/// 是否存在相同的记录
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public bool Exist(CommissionRule data)
		{
			return Db.Queryable<CommissionRule>().Any(o => o.ProductId == data.ProductId && o.Year == data.Year && o.Month == data.Month && o.Id != data.Id);
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="productId"></param>
		/// <param name="year"></param>
		/// <param name="month"></param>
		/// <param name="status"></param>
		/// <returns></returns>
		public CommissionRule Get(int productId, int? year, int? month, int? status = null)
		{
			var query = Db.Queryable<CommissionRule>().Where(o => o.ProductId == productId);
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
		/// <param name="productId"></param>
		/// <param name="status"></param>
		/// <returns></returns>
		public ICollection<CommissionRule> List(int productId, int? status = null)
		{
			var query = Db.Queryable<CommissionRule>().Where(o => o.ProductId == productId);
			if (status.HasValue)
			{
				query = query.Where(o => o.Status == status.Value);
			}
			return Sort(query).ToList();
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="productIds"></param>
		/// <param name="status"></param>
		/// <returns></returns>
		public ICollection<CommissionRule> List(ICollection<int> productIds, int? status = null)
		{
			var query = Db.Queryable<CommissionRule>().Where(o => productIds.Contains(o.ProductId));
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
		private ISugarQueryable<CommissionRule> Sort(ISugarQueryable<CommissionRule> query)
		{
			return query.OrderBy(o => o.Year, OrderByType.Desc).OrderBy(o => o.Month, OrderByType.Desc).OrderBy(o => o.Id, OrderByType.Desc);
		}

		/// <summary>
		/// 分页查询条件
		/// </summary>
		/// <param name="arg"></param>
		/// <param name="query"></param>
		/// <returns></returns>
		public override ISugarQueryable<CommissionRule> Query(CommissionRuleArg<CommissionRule> arg, ISugarQueryable<CommissionRule> query)
		{
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
		public override ISugarQueryable<CommissionRule> Sort(ISugarQueryable<CommissionRule> query, string orderByField, OrderByType orderByType)
		{
			return orderByField switch
			{
				"ProductId" => query.OrderBy(o => o.ProductId, orderByType),
				"Year" => query.OrderBy(o => o.Year, orderByType).OrderBy(o => o.Month, orderByType),
				"Month" => query.OrderBy(o => o.Year, orderByType).OrderBy(o => o.Month, orderByType),
				"AgentRate" => query.OrderBy(o => o.AgentRate, orderByType),
				"AgentAmount" => query.OrderBy(o => o.AgentAmount, orderByType),
				"PersonalRate" => query.OrderBy(o => o.PersonalRate, orderByType),
				"PersonalAmount" => query.OrderBy(o => o.PersonalAmount, orderByType),
				"CreateTime" => query.OrderBy(o => o.CreateTime, orderByType),
				"UpdateTime" => query.OrderBy(o => o.UpdateTime, orderByType),
				_ => query.OrderBy(o => o.Id, orderByType)
			};
		}
	}
}
