using Basic.Model;
using Basic.Model.PageArg;
using SqlSugar;

namespace Basic.DAL
{
	/// <summary>
	/// AccountBankCardDAL
	/// </summary>
	internal class AccountBankCardDAL : DAL<AccountBankCard, BankCardArg<AccountBankCard>>
	{
		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="accountId"></param>
		/// <returns></returns>
		public AccountBankCard GetByAccountId(int accountId)
		{
			return Db.Queryable<AccountBankCard>().Where(o => o.AccountId == accountId).OrderBy(o => o.Id, OrderByType.Desc).First();
		}

		/// <summary>
		/// 分页查询条件
		/// </summary>
		/// <param name="arg"></param>
		/// <param name="query"></param>
		/// <returns></returns>
		public override ISugarQueryable<AccountBankCard> Query(BankCardArg<AccountBankCard> arg, ISugarQueryable<AccountBankCard> query)
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
			//银行Id
			if (arg.BankId.HasValue)
			{
				query = query.Where(o => o.BankId == arg.BankId.Value);
			}
			//卡号
			if (!string.IsNullOrEmpty(arg.CardNumber))
			{
				query = query.Where(o => o.CardNumber == arg.CardNumber);
			}
			//持卡人
			if (!string.IsNullOrEmpty(arg.Cardholder))
			{
				query = query.Where(o => o.Cardholder == arg.Cardholder);
			}
			//支行
			if (!string.IsNullOrEmpty(arg.Branch))
			{
				query = query.Where(o => o.Branch.Contains(arg.Branch));
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
		public override ISugarQueryable<AccountBankCard> Sort(ISugarQueryable<AccountBankCard> query, string orderByField, OrderByType orderByType)
		{
			return orderByField switch
			{
				"AccountId" => query.OrderBy(o => o.AccountId, orderByType),
				"BankId" => query.OrderBy(o => o.BankId, orderByType),
				"CardNumber" => query.OrderBy(o => o.CardNumber, orderByType),
				"Cardholder" => query.OrderBy(o => o.Cardholder, orderByType),
				"Branch" => query.OrderBy(o => o.Branch, orderByType),
				"CreateTime" => query.OrderBy(o => o.CreateTime, orderByType),
				"UpdateTime" => query.OrderBy(o => o.UpdateTime, orderByType),
				_ => query.OrderBy(o => o.Id, orderByType)
			};
		}
	}
}
