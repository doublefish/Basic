using Basic.Model;
using Basic.Model.PageArg;
using SqlSugar;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Basic.DAL
{
	/// <summary>
	/// AgentDAL
	/// </summary>
	internal class AgentDAL : TreeDAL<Agent, AgentArg<Agent>>
	{
		/// <summary>
		/// 创建查询表达式
		/// </summary>
		/// <returns></returns>
		public override Expression<Func<Agent, Agent>> CreatePersonalSelector()
		{
			return o => new Agent()
			{
				Level = o.Level,
				Balance = o.Balance,
				Freeze = o.Freeze
			};
		}

		/// <summary>
		/// 分页查询条件
		/// </summary>
		/// <param name="arg"></param>
		/// <param name="query"></param>
		/// <returns></returns>
		public override ISugarQueryable<Agent> Query(AgentArg<Agent> arg, ISugarQueryable<Agent> query)
		{
			//父节点Id
			if (arg.ParentId.HasValue)
			{
				query = query.Where(o => o.ParentId == arg.ParentId.Value);
			}
			//名称
			if (!string.IsNullOrEmpty(arg.Name))
			{
				query = query.Where(o => o.Name.Contains(arg.Name));
			}
			//状态
			if (arg.Status.HasValue)
			{
				query = query.Where(o => o.Status == arg.Status.Value);
			}
			else if (arg.Statuses != null && arg.Statuses.Length > 0)
			{
				query = query.Where(o => arg.Statuses.Contains(o.Status));
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
		public override ISugarQueryable<Agent> Sort(ISugarQueryable<Agent> query, string orderByField, OrderByType orderByType)
		{
			return orderByField switch
			{
				"Code" => query.OrderBy(o => o.Code, orderByType),
				"Name" => query.OrderBy(o => o.Name, orderByType),
				"Level" => query.OrderBy(o => o.Level, orderByType),
				"Balance" => query.OrderBy(o => o.Balance, orderByType),
				"Freeze" => query.OrderBy(o => o.Freeze, orderByType),
				"Status" => query.OrderBy(o => o.Status, orderByType),
				"CreateTime" => query.OrderBy(o => o.CreateTime, orderByType),
				"UpdateTime" => query.OrderBy(o => o.UpdateTime, orderByType),
				_ => query.OrderBy(o => o.Id, orderByType)
			};
		}
	}
}
