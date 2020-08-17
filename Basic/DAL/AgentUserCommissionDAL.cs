using Basic.Model;
using Basic.Model.PageArg;
using SqlSugar;
using System;
using System.Linq;

namespace Basic.DAL
{
	/// <summary>
	/// AgentUserCommissionDAL
	/// </summary>
	internal class AgentUserCommissionDAL : DAL<AgentUserCommission, CommissionArg<AgentUserCommission>>
	{
		/// <summary>
		/// 分页查询条件
		/// </summary>
		/// <param name="arg"></param>
		/// <param name="query"></param>
		/// <returns></returns>
		public override ISugarQueryable<AgentUserCommission> Query(CommissionArg<AgentUserCommission> arg, ISugarQueryable<AgentUserCommission> query)
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
			//产品Id
			if (arg.ProductId.HasValue)
			{
				query = query.Where(o => SqlFunc.Subqueryable<Order>().Where(po => po.Id == o.OrderId && po.ProductId == arg.ProductId.Value).Any());
			}
			//订单Id
			if (arg.OrderId.HasValue)
			{
				query = query.Where(o => o.OrderId == arg.OrderId.Value);
			}
			//订单编号
			if (!string.IsNullOrEmpty(arg.OrderNumber))
			{
				query = query.Where(o => SqlFunc.Subqueryable<Order>().Where(po => po.Id == o.OrderId && po.Number == arg.OrderNumber).Any());
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
		public override ISugarQueryable<AgentUserCommission> Sort(ISugarQueryable<AgentUserCommission> query, string orderByField, OrderByType orderByType)
		{
			return orderByField switch
			{
				"AgentId" => query.OrderBy(o => o.AgentId, orderByType),
				"AgentUserId" => query.OrderBy(o => o.AgentUserId, orderByType),
				"OrderId" => query.OrderBy(o => o.OrderId, orderByType),
				"Amount" => query.OrderBy(o => o.Amount, orderByType),
				"Status" => query.OrderBy(o => o.Status, orderByType),
				"CreateTime" => query.OrderBy(o => o.CreateTime, orderByType),
				"UpdateTime" => query.OrderBy(o => o.UpdateTime, orderByType),
				_ => query.OrderBy(o => o.Id, orderByType)
			};
		}

		#region 业务

		/// <summary>
		/// 分配
		/// </summary>
		/// <param name="data"></param>
		/// <param name="newData"></param>
		public void Distribute(AgentUserCommission data, AgentUserCommission newData)
		{
			var propertyNames = new string[] { "Status", "UpdateTime" };
			try
			{
				//重载指定事务的级别
				Db.Ado.BeginTran(System.Data.IsolationLevel.ReadCommitted);

				//修改
				Db.Updateable(data).UpdateColumns(propertyNames).ExecuteCommand();
				//新增佣金记录
				if (newData != null)
				{
					Add(Db, newData);
				}
				//删除缓存
				DeleteCache(data.Id);

				Db.Ado.CommitTran();
			}
			catch
			{
				Db.Ado.RollbackTran();
				throw;
			}
		}

		#endregion

		/// <summary>
		/// 新增
		/// </summary>
		/// <param name="db"></param>
		/// <param name="data"></param>
		internal static void Add(SqlSugarClient db, AgentUserCommission data)
		{
			db.Insertable(data).ExecuteCommand();
			var agent = db.Queryable<Agent>().InSingle(data.AgentId);
			agent.Balance += data.Amount;
			db.Updateable(agent).UpdateColumns(new string[] { "Balance" }).ExecuteCommand();
			if (data.AgentUserId > 0)
			{
				var agentUser = db.Queryable<AgentUser>().InSingle(data.AgentUserId);
				agentUser.Balance += data.Amount;
				db.Updateable(agentUser).UpdateColumns(new string[] { "Balance" }).ExecuteCommand();
				db.Insertable(new AgentUserFund()
				{
					AgentId = data.AgentId,
					AgentUserId = data.AgentUserId,
					Number = CommonHelper.GenerateNumber("AgentUserFund"),
					Type = Model.Config.Fund.Type.Revenue.Commission,
					Amount = data.Amount,
					Balance = agentUser.Balance,
					Freeze = agentUser.Freeze,
					CreateTime = data.CreateTime,
					Note = ""
				}).ExecuteCommand();
			}
		}
	}
}
