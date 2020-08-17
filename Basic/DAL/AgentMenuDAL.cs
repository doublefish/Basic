using Adai.Standard.Ext;
using Basic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Basic.DAL
{
	/// <summary>
	/// AgentMenuDAL
	/// </summary>
	internal class AgentMenuDAL : TreeDAL<AgentMenu>
	{
		/// <summary>
		/// 创建个性化查询表达式
		/// </summary>
		/// <returns></returns>
		public override Expression<Func<AgentMenu, AgentMenu>> CreatePersonalSelector()
		{
			return o => new AgentMenu()
			{
				Type = o.Type,
				PageUrl = o.PageUrl,
				ApiUrl = o.ApiUrl,
				Icon = o.Icon,
				Level = o.Level,
				IsAdmin = o.IsAdmin
			};
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="level"></param>
		/// <param name="isAdmin"></param>
		/// <param name="useCache"></param>
		/// <returns></returns>
		public ICollection<AgentMenu> List(int level, bool isAdmin, bool useCache = false)
		{
			if (useCache == true && IsCacheModel == true)
			{
				var key = string.Format("{0}-Level", CacheKey);
				var hashField = string.Format("{0}-{1}", level, isAdmin);
				var pkValues = CacheDb.HashGet<ICollection<int>>(key, hashField);
				if (pkValues == null || pkValues.Count == 0)
				{
					var _query = Db.Queryable<AgentMenu>().Where(o => o.Level >= level);
					if (!isAdmin)
					{
						_query = _query.Where(o => o.IsAdmin == false);
					}
					var ids = _query.OrderBy(o => o.Sequence).OrderBy(o => o.Id).Select(o => o.Id).ToList();
					if (ids.Count == 0)
					{
						return new List<AgentMenu>();
					}
					pkValues = ids;
					CacheDb.HashSet(key, hashField, pkValues);
				}
				var results = ListByPks(pkValues, true);
				return results.ToArray();
			}
			var query = Db.Queryable<AgentMenu>().Where(o => o.Level >= level);
			if (!isAdmin)
			{
				query = query.Where(o => o.IsAdmin == false);
			}
			return query.OrderBy(o => o.Sequence).OrderBy(o => o.Id).ToList();
		}

		/// <summary>
		/// 刷新缓存
		/// </summary>
		public override void RefreshCache()
		{
			CacheDb.KeyDelete(string.Format("{0}-Level", CacheKey));
			base.RefreshCache();
		}
	}
}
