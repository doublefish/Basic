using Basic.Model;
using Basic.Model.PageArg;
using SqlSugar;
using System;
using System.Linq;

namespace Basic.DAL
{
	/// <summary>
	/// OrderDAL
	/// </summary>
	internal class OrderDAL : DAL<Order, OrderArg<Order>>
	{
		/// <summary>
		/// 增加
		/// </summary>
		/// <param name="data"></param>
		/// <param name="discountId"></param>
		public override void Add(Order data)
		{
			try
			{
				//重载指定事务的级别
				Db.Ado.BeginTran(System.Data.IsolationLevel.ReadCommitted);

				//新增
				Db.Insertable(data).ExecuteCommandIdentityIntoEntity();
				if (data.DiscountId.HasValue)
				{
					//修改产品折扣统计
					var discount = Db.Queryable<ProductDiscount>().InSingle(data.DiscountId.Value);
					discount.Used++;
					Db.Updateable(discount).UpdateColumns(new string[] { "Used" }).ExecuteCommand();
				}

				Db.Ado.CommitTran();
			}
			catch
			{
				Db.Ado.RollbackTran();
				throw;
			}
		}

		/// <summary>
		/// 分页查询条件
		/// </summary>
		/// <param name="arg"></param>
		/// <param name="query"></param>
		/// <returns></returns>
		public override ISugarQueryable<Order> Query(OrderArg<Order> arg, ISugarQueryable<Order> query)
		{
			//产品Id
			if (arg.ProductId > 0)
			{
				query = query.Where(o => o.ProductId == arg.ProductId);
			}
			//编号
			if (!string.IsNullOrEmpty(arg.Number))
			{
				query = query.Where(o => o.Number == arg.Number);
			}
			//出行日期.开始时间
			if (arg.DateStart.HasValue)
			{
				var start = arg.DateStart.Value.Date;
				query = query.Where(o => o.Date >= start);
			}
			//出行日期.结束时间
			if (arg.DateEnd.HasValue)
			{
				var end = arg.DateEnd.Value.Date.AddDays(1);
				query = query.Where(o => o.Date < end);
			}
			//手机号码
			if (!string.IsNullOrEmpty(arg.Mobile))
			{
				query = query.Where(o => o.Mobile == arg.Mobile);
			}
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
		public override ISugarQueryable<Order> Sort(ISugarQueryable<Order> query, string orderByField, OrderByType orderByType)
		{
			return orderByField switch
			{
				"ProductId" => query.OrderBy(o => o.ProductId, orderByType),
				"Number" => query.OrderBy(o => o.Number, orderByType),
				"Mobile" => query.OrderBy(o => o.Mobile, orderByType),
				"Date" => query.OrderBy(o => o.Date, orderByType),
				"Audlts" => query.OrderBy(o => o.Adults, orderByType),
				"Children" => query.OrderBy(o => o.Children, orderByType),
				"OriginalPrice" => query.OrderBy(o => o.OriginalPrice, orderByType),
				"DiscountPrice" => query.OrderBy(o => o.DiscountPrice, orderByType),
				"AdultPrice" => query.OrderBy(o => o.AdultPrice, orderByType),
				"ChildPrice" => query.OrderBy(o => o.ChildPrice, orderByType),
				"TotalPrice" => query.OrderBy(o => o.TotalPrice, orderByType),
				"Status" => query.OrderBy(o => o.Status, orderByType),
				"CreateTime" => query.OrderBy(o => o.CreateTime, orderByType),
				"UpdateTime" => query.OrderBy(o => o.UpdateTime, orderByType),
				_ => query.OrderBy(o => o.Id, orderByType)
			};
		}

		#region 业务

		/// <summary>
		/// 完成
		/// </summary>
		/// <param name="data"></param>
		/// <param name="accountPromotionId"></param>
		/// <param name="accountCommission"></param>
		/// <param name="agentUserCommission"></param>
		public void Complete(Order data, int? accountPromotionId = null, AccountCommission accountCommission = null)
		{
			var propertyNames = new string[] { "UserId", "Status", "UpdateTime" };
			try
			{
				//重载指定事务的级别
				Db.Ado.BeginTran(System.Data.IsolationLevel.ReadCommitted);

				//修改
				Db.Updateable(data).UpdateColumns(propertyNames).ExecuteCommand();
				if (accountPromotionId.HasValue)
				{
					//修改用户推广统计
					var accountPromotion = Db.Queryable<AccountPromotion>().InSingle(accountPromotionId.Value);
					accountPromotion.Orders++;
					accountPromotion.OrderAmount += data.TotalPrice.Value;
					Db.Updateable(accountPromotion).UpdateColumns(new string[] { "Orders", "OrderAmount" }).ExecuteCommand();
					//新增佣金记录
					if (accountCommission != null)
					{
						AccountCommissionDAL.Add(Db, accountCommission);
					}
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
	}
}
