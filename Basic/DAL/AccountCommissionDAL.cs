using Basic.Model;
using Basic.Model.PageArg;
using SqlSugar;
using System;
using System.Linq;

namespace Basic.DAL
{
	/// <summary>
	/// AccountCommissionDAL
	/// </summary>
	internal class AccountCommissionDAL : DAL<AccountCommission, CommissionArg<AccountCommission>>
	{
		/// <summary>
		/// 分页查询条件
		/// </summary>
		/// <param name="arg"></param>
		/// <param name="query"></param>
		/// <returns></returns>
		public override ISugarQueryable<AccountCommission> Query(CommissionArg<AccountCommission> arg, ISugarQueryable<AccountCommission> query)
		{
			//用户Id
			if (arg.AccountId.HasValue)
			{
				query = query.Where(o => o.AccountId == arg.AccountId.Value);
			}
			//用户名
			if (!string.IsNullOrEmpty(arg.Username))
			{
				query = query.Where(o => SqlFunc.Subqueryable<Account>().Where(a => a.Id == o.AccountId && a.Username == arg.Username).Any());
			}
			//用户手机号码
			if (!string.IsNullOrEmpty(arg.Mobile))
			{
				query = query.Where(o => SqlFunc.Subqueryable<Account>().Where(a => a.Id == o.AccountId && a.Mobile == arg.Mobile).Any());
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
		public override ISugarQueryable<AccountCommission> Sort(ISugarQueryable<AccountCommission> query, string orderByField, OrderByType orderByType)
		{
			return orderByField switch
			{
				"AccountId" => query.OrderBy(o => o.AccountId, orderByType),
				"OrderId" => query.OrderBy(o => o.OrderId, orderByType),
				"Amount" => query.OrderBy(o => o.Amount, orderByType),
				"Status" => query.OrderBy(o => o.Status, orderByType),
				"CreateTime" => query.OrderBy(o => o.CreateTime, orderByType),
				"UpdateTime" => query.OrderBy(o => o.UpdateTime, orderByType),
				_ => query.OrderBy(o => o.Id, orderByType)
			};
		}

		/// <summary>
		/// 新增
		/// </summary>
		/// <param name="db"></param>
		/// <param name="data"></param>
		internal static void Add(SqlSugarClient db, AccountCommission data)
		{
			db.Insertable(data).ExecuteCommand();
			var account = db.Queryable<Account>().InSingle(data.AccountId);
			account.Balance += data.Amount;
			db.Updateable(account).UpdateColumns(new string[] { "Balance" }).ExecuteCommand();
			db.Insertable(new AccountFund()
			{
				AccountId = data.AccountId,
				Number = CommonHelper.GenerateNumber("AccountFund"),
				Type = Model.Config.Fund.Type.Revenue.Commission,
				Amount = data.Amount,
				Balance = account.Balance,
				Freeze = account.Freeze,
				CreateTime = data.CreateTime,
				Note = ""
			}).ExecuteCommand();
		}
	}
}
