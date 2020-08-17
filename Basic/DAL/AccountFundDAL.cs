using Basic.Model;
using Basic.Model.PageArg;
using SqlSugar;

namespace Basic.DAL
{
	/// <summary>
	/// AccountFundDAL
	/// </summary>
	internal class AccountFundDAL : DAL<AccountFund>
	{
		/// <summary>
		/// 分页查询条件
		/// </summary>
		/// <param name="arg"></param>
		/// <param name="query"></param>
		/// <returns></returns>
		public override ISugarQueryable<AccountFund> Query(BaseArg<AccountFund> arg, ISugarQueryable<AccountFund> query)
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
		public override ISugarQueryable<AccountFund> Sort(ISugarQueryable<AccountFund> query, string orderByField, OrderByType orderByType)
		{
			return orderByField switch
			{
				"AccountId" => query.OrderBy(o => o.AccountId, orderByType),
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
