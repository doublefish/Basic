using Basic.Model;
using Basic.Model.PageArg;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Basic.DAL
{
	/// <summary>
	/// ProductDAL
	/// </summary>
	internal class ProductDAL : DAL<Product, ProductArg<Product>>
	{
		/// <summary>
		/// 创建查询表达式
		/// </summary>
		/// <returns></returns>
		public override Expression<Func<Product, Product>> CreateSelector()
		{
			return o => new Product()
			{
				Id = o.Id,
				Name = o.Name,
				Type = o.Type,
				Tags = o.Tags,
				Themes = o.Themes,
				Price = o.Price,
				//Overview = o.Overview,
				//Feature = o.Feature,
				//Cost = o.Cost,
				//Notice = o.Notice,
				//Book = o.Book,
				Cover = o.Cover,
				Cover1 = o.Cover1,
				Orders = o.Orders,
				Recommends = o.Recommends,
				Clicks = o.Clicks,
				Sequence = o.Sequence,
				Status = o.Status,
				CreateTime = o.CreateTime,
				UpdateTime = o.UpdateTime,
				Note = o.Note
			};
		}

		/// <summary>
		/// 更新点击次数
		/// </summary>
		/// <param name="id"></param>
		public void UpdateClicks(int id)
		{
			Db.Updateable<Product>().Where(o => o.Id == id).UpdateColumns(o => new { o.Clicks }).ReSetValue(o => o.Clicks == (o.Clicks + 1)).ExecuteCommand();
		}

		/// <summary>
		/// 删除被引用数据
		/// </summary>
		/// <param name="id"></param>
		public override void DeleteReferenced(int id)
		{
			Db.Deleteable<ProductImage>().Where(o => o.ProductId == id).ExecuteCommand();
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="type"></param>
		/// <param name="tag"></param>
		/// <param name="take"></param>
		/// <param name="orderByField"></param>
		/// <param name="orderByType"></param>
		/// <returns></returns>
		public ICollection<Product> List(int? type, int? tag, int take, string orderByField = null, OrderByType orderByType = OrderByType.Asc)
		{
			var query = Db.Queryable<Product>();
			if (type.HasValue)
			{
				query = query.Where(o => o.Type == type.Value);
			}
			if (tag.HasValue)
			{
				query = query.Where(o => o.Tags.Contains(string.Format(",{0},", tag.Value)));
			}
			query = Sort(query, orderByField, orderByType);
			return query.Take(take).Select(CreateSelector()).ToList();
		}

		/// <summary>
		/// 分页查询条件
		/// </summary>
		/// <param name="arg"></param>
		/// <param name="query"></param>
		/// <returns></returns>
		public override ISugarQueryable<Product> Query(ProductArg<Product> arg, ISugarQueryable<Product> query)
		{
			//名称
			if (!string.IsNullOrEmpty(arg.Name))
			{
				query = query.Where(o => o.Name.Contains(arg.Name));
			}
			//类型
			if (arg.Type.HasValue)
			{
				query = query.Where(o => o.Type == arg.Type.Value);
			}
			//标签
			if (arg.Tags != null && arg.Tags.Length > 0)
			{
				query = query.WhereLike("Tags", arg.Tags, ",{0},");
			}
			//主题
			if (arg.Themes != null && arg.Themes.Length > 0)
			{
				query = query.WhereLike("Themes", arg.Themes, ",{0},");
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
		public override ISugarQueryable<Product> Sort(ISugarQueryable<Product> query, string orderByField, OrderByType orderByType)
		{
			return orderByField switch
			{
				"Name" => query.OrderBy(o => o.Name, orderByType),
				"Type" => query.OrderBy(o => o.Type, orderByType),
				"Price" => query.OrderBy(o => o.Price, orderByType),
				"Orders" => query.OrderBy(o => o.Orders, orderByType),
				"Recommends" => query.OrderBy(o => o.Recommends, orderByType),
				"Clicks" => query.OrderBy(o => o.Clicks, orderByType),
				"Sequence" => query.OrderBy(o => o.Sequence, orderByType),
				"Status" => query.OrderBy(o => o.Status, orderByType),
				"CreateTime" => query.OrderBy(o => o.CreateTime, orderByType),
				"UpdateTime" => query.OrderBy(o => o.UpdateTime, orderByType),
				_ => query.OrderBy(o => o.Id, orderByType)
			};
		}
	}
}
