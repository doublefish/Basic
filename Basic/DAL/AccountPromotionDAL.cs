using Adai.Standard.Ext;
using Basic.Model;
using Basic.Model.PageArg;
using SqlSugar;
using System;
using System.Linq;

namespace Basic.DAL
{
	/// <summary>
	/// AccountPromotionDAL
	/// </summary>
	internal class AccountPromotionDAL : DAL<AccountPromotion, PromotionArg<AccountPromotion>>
	{
		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="accountId"></param>
		/// <param name="useCache"></param>
		/// <returns></returns>
		public AccountPromotion GetByAccountId(int accountId, bool useCache = false)
		{
			if (useCache == true && IsCacheModel == true)
			{
				var key = string.Format("{0}-AccountId", CacheKey);
				var hashField = accountId;
				var pkValue = CacheDb.HashGet(key, hashField).ToInt32();
				if (pkValue <= 0)
				{
					var result = GetByAccountId(accountId, false);
					if (result == null)
					{
						return null;
					}
					pkValue = result.Id;
					CacheDb.HashSet(key, hashField, pkValue);
				}
			}
			return Db.Queryable<AccountPromotion>().Where(o => o.AccountId == accountId).First();
		}

		/// <summary>
		/// 分页查询条件
		/// </summary>
		/// <param name="arg"></param>
		/// <param name="query"></param>
		/// <returns></returns>
		public override ISugarQueryable<AccountPromotion> Query(PromotionArg<AccountPromotion> arg, ISugarQueryable<AccountPromotion> query)
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
			//推广人Id
			if (arg.PromoterId.HasValue)
			{
				query = query.Where(o => o.PromoterId == arg.PromoterId.Value);
			}
			//推广人用户名
			if (!string.IsNullOrEmpty(arg.PromoterUsername))
			{
				query = query.Where(o => SqlFunc.Subqueryable<Account>().Where(a => a.Id == o.PromoterId && a.Username == arg.PromoterUsername).Any());
			}
			//推广人手机号码
			if (!string.IsNullOrEmpty(arg.PromoterMobile))
			{
				query = query.Where(o => SqlFunc.Subqueryable<Account>().Where(a => a.Id == o.PromoterId && a.Mobile == arg.PromoterMobile).Any());
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
		public override ISugarQueryable<AccountPromotion> Sort(ISugarQueryable<AccountPromotion> query, string orderByField, OrderByType orderByType)
		{
			return orderByField switch
			{
				"Orders" => query.OrderBy(o => o.Orders, orderByType),
				"OrderAmount" => query.OrderBy(o => o.OrderAmount, orderByType),
				"Status" => query.OrderBy(o => o.Status, orderByType),
				"CreateTime" => query.OrderBy(o => o.CreateTime, orderByType),
				"UpdateTime" => query.OrderBy(o => o.UpdateTime, orderByType),
				_ => query.OrderBy(o => o.Id, orderByType)
			};
		}
	}
}
