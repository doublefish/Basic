using Basic.Model;
using Basic.Model.PageArg;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Basic.DAL
{
	/// <summary>
	/// ProductDiscountDAL
	/// </summary>
	internal class ProductDiscountDAL : DAL<ProductDiscount, ProductArg<ProductDiscount>>
	{
		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="productId"></param>
		/// <param name="date"></param>
		/// <returns></returns>
		public ProductDiscount Get(int productId, DateTime date)
		{
			date = date.Date;
			return Db.Queryable<ProductDiscount>().Where(o => o.ProductId == productId && o.StartTime <= date && date <= o.EndTime).OrderBy(o => o.Id, OrderByType.Desc).First();
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="productId"></param>
		/// <param name="date"></param>
		/// <param name="status"></param>
		/// <returns></returns>
		public ICollection<ProductDiscount> List(int productId, DateTime? date = null, int? status = null)
		{
			var query = Db.Queryable<ProductDiscount>().Where(o => o.ProductId == productId);
			if (date.HasValue)
			{
				date = date.Value.Date;
				query = query.Where(o => o.StartTime <= date && date <= o.EndTime);
			}
			if (status.HasValue)
			{
				query = query.Where(o => o.Status == status.Value);
			}
			return query.ToList();
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="productIds"></param>
		/// <param name="date"></param>
		/// <param name="status"></param>
		/// <returns></returns>
		public ICollection<ProductDiscount> List(ICollection<int> productIds, DateTime? date = null, int? status = null)
		{
			var query = Db.Queryable<ProductDiscount>().Where(o => productIds.Contains(o.ProductId));
			if (date.HasValue)
			{
				date = date.Value.Date;
				query = query.Where(o => o.StartTime <= date && date <= o.EndTime);
			}
			if (status.HasValue)
			{
				query = query.Where(o => o.Status == status.Value);
			}
			return query.ToList();
		}

		/// <summary>
		/// 分页查询条件
		/// </summary>
		/// <param name="arg"></param>
		/// <param name="query"></param>
		/// <returns></returns>
		public override ISugarQueryable<ProductDiscount> Query(ProductArg<ProductDiscount> arg, ISugarQueryable<ProductDiscount> query)
		{
			//产品Id
			if (arg.ProductId.HasValue)
			{
				query = query.Where(o => o.ProductId == arg.ProductId.Value);
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
		public override ISugarQueryable<ProductDiscount> Sort(ISugarQueryable<ProductDiscount> query, string orderByField, OrderByType orderByType)
		{
			return orderByField switch
			{
				"ProductId" => query.OrderBy(o => o.ProductId, orderByType),
				"Name" => query.OrderBy(o => o.Name, orderByType),
				"Rate" => query.OrderBy(o => o.Rate, orderByType),
				"Amount" => query.OrderBy(o => o.Amount, orderByType),
				"Total" => query.OrderBy(o => o.Total, orderByType),
				"StartTime" => query.OrderBy(o => o.StartTime, orderByType),
				"EndTime" => query.OrderBy(o => o.EndTime, orderByType),
				"Status" => query.OrderBy(o => o.Status, orderByType),
				"CreateTime" => query.OrderBy(o => o.CreateTime, orderByType),
				"UpdateTime" => query.OrderBy(o => o.UpdateTime, orderByType),
				_ => query.OrderBy(o => o.Id, orderByType)
			};
		}
	}
}
