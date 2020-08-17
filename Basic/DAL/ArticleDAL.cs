using Basic.Model;
using Basic.Model.PageArg;
using SqlSugar;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Basic.DAL
{
	/// <summary>
	/// ArticleDAL
	/// </summary>
	internal class ArticleDAL : DAL<Article, ArticleArg<Article>>
	{
		/// <summary>
		/// 创建查询表达式
		/// </summary>
		/// <returns></returns>
		public override Expression<Func<Article, Article>> CreateSelector()
		{
			return o => new Article()
			{
				Id = o.Id,
				Title = o.Title,
				Summary = o.Summary,
				Author = o.Author,
				Source = o.Source,
				//Content = o.Content,
				Cover = o.Cover,
				ReleaseTime = o.ReleaseTime,
				IsStick = o.IsStick,
				Clicks = o.Clicks,
				Favorites = o.Favorites,
				Shares = o.Shares,
				UserId = o.UserId,
				Status = o.Status,
				CreateTime = o.CreateTime,
				UpdateTime = o.UpdateTime,
				Note = o.Note
			};
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="section"></param>
		/// <param name="useCache"></param>
		/// <returns></returns>
		public Article GetBySection(int section, bool useCache = false)
		{
			if (useCache == true && IsCacheModel == true)
			{
				var key = string.Format("{0}-Section", CacheKey);
				var hashField = section;
				var pkValue = CacheDb.HashGet(key, hashField).ObjToInt();
				if (pkValue <= 0)
				{
					var result = GetBySection(section, false);
					if (result == null || result.Status != Model.Config.Article.Status.Released)
					{
						return null;
					}
					pkValue = result.Id;
					CacheDb.HashSet(key, hashField, pkValue);
				}
			}
			var query = Db.Queryable<Article>().Where(o => o.Status == Model.Config.Article.Status.Released && o.Sections.Contains(string.Format(",{0},", section)))
			.OrderBy(o => o.Id, OrderByType.Desc);
			return query.First();
		}

		/// <summary>
		/// 分页查询条件
		/// </summary>
		/// <param name="arg"></param>
		/// <param name="query"></param>
		/// <returns></returns>
		public override ISugarQueryable<Article> Query(ArticleArg<Article> arg, ISugarQueryable<Article> query)
		{
			//标题
			if (!string.IsNullOrEmpty(arg.Title))
			{
				query = query.Where(o => o.Title.Contains(arg.Title));
			}
			//版块
			if (arg.Section.HasValue)
			{
				query = query.Where(o => o.Sections.Contains(string.Format(",{0},", arg.Section.Value)));
			}
			else if (arg.Sections != null && arg.Sections.Length > 0)
			{
				query = query.WhereLike("Sections", arg.Sections, ",{0},");
			}
			//作者
			if (!string.IsNullOrEmpty(arg.Author))
			{
				query = query.Where(o => o.Author == arg.Author);
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
		public override ISugarQueryable<Article> Sort(ISugarQueryable<Article> query, string orderByField, OrderByType orderByType)
		{
			return orderByField switch
			{
				"Title" => query.OrderBy(o => o.Title, orderByType),
				"Clicks" => query.OrderBy(o => o.Clicks, orderByType),
				"Favorites" => query.OrderBy(o => o.Favorites, orderByType),
				"Shares" => query.OrderBy(o => o.Shares, orderByType),
				"ReleaseTime" => query.OrderBy(o => o.ReleaseTime, orderByType),
				"Status" => query.OrderBy(o => o.Status, orderByType),
				"CreateTime" => query.OrderBy(o => o.CreateTime, orderByType),
				"UpdateTime" => query.OrderBy(o => o.UpdateTime, orderByType),
				_ => query.OrderBy(o => o.Id, orderByType)
			};
		}
	}
}
