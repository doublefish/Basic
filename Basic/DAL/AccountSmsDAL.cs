using Basic.Model;
using Basic.Model.PageArg;
using SqlSugar;
using System;
using System.Linq;

namespace Basic.DAL
{
	/// <summary>
	/// AccountSmsDAL
	/// </summary>
	internal class AccountSmsDAL : DAL<AccountSms>
	{
		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="mobile"></param>
		/// <param name="type"></param>
		/// <returns></returns>
		public AccountSms Get(string mobile, int type)
		{
			return Db.Queryable<AccountSms>().Where(o => o.Mobile == mobile && o.Type == type).OrderBy(o => o.Id, OrderByType.Desc).First();
		}

		/// <summary>
		/// 分页查询条件
		/// </summary>
		/// <param name="arg"></param>
		/// <param name="query"></param>
		/// <returns></returns>
		public override ISugarQueryable<AccountSms> Query(BaseArg<AccountSms> arg, ISugarQueryable<AccountSms> query)
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
			//手机号码
			if (!string.IsNullOrEmpty(arg.Mobile))
			{
				query = query.Where(o => o.Mobile == arg.Mobile);
			}
			//类型
			if (arg.Type.HasValue)
			{
				query = query.Where(o => o.Type == arg.Type.Value);
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
		public override ISugarQueryable<AccountSms> Sort(ISugarQueryable<AccountSms> query, string orderByField, OrderByType orderByType)
		{
			return orderByField switch
			{
				"AccountId" => query.OrderBy(o => o.AccountId, orderByType),
				"Mobile" => query.OrderBy(o => o.Mobile, orderByType),
				"Type" => query.OrderBy(o => o.Type, orderByType),
				"Status" => query.OrderBy(o => o.Status, orderByType),
				"CreateTime" => query.OrderBy(o => o.CreateTime, orderByType),
				"UpdateTime" => query.OrderBy(o => o.UpdateTime, orderByType),
				_ => query.OrderBy(o => o.Id, orderByType)
			};
		}
	}
}
