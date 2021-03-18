using Adai.Standard.Ext;
using Basic.Model;
using Basic.Model.PageArg;
using SqlSugar;
using System;
using System.Linq;

namespace Basic.DAL
{
	/// <summary>
	/// RoleDAL
	/// </summary>
	internal class RoleDAL : DAL<Role>
	{
		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="code"></param>
		/// <param name="useCache"></param>
		/// <returns></returns>
		public Role GetByCode(string code, bool useCache = false)
		{
			if (useCache == true && IsCacheModel == true)
			{
				var key = string.Format("{0}-Code", CacheKey);
				var hashField = code;
				var pkValue = CacheDb.HashGet(key, hashField).ToInt32();
				if (pkValue <= 0)
				{
					var result = GetByCode(code, false);
					if (result == null)
					{
						return null;
					}
					pkValue = result.Id;
					CacheDb.HashSet(key, hashField, pkValue);
				}
			}
			return Db.Queryable<Role>().Where(o => o.Code == code).First();
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="name"></param>
		/// <param name="useCache"></param>
		/// <returns></returns>
		public Role GetByName(string name, bool useCache = false)
		{
			if (useCache == true && IsCacheModel == true)
			{
				var key = string.Format("{0}-Name", CacheKey);
				var hashField = name;
				var pkValue = CacheDb.HashGet(key, hashField).ToInt32();
				if (pkValue <= 0)
				{
					var result = GetByName(name, false);
					if (result == null)
					{
						return null;
					}
					pkValue = result.Id;
					CacheDb.HashSet(key, hashField, pkValue);
				}
			}
			return Db.Queryable<Role>().Where(o => o.Name == name).First();
		}

		/// <summary>
		/// 分页查询条件
		/// </summary>
		/// <param name="arg"></param>
		/// <param name="query"></param>
		/// <returns></returns>
		public override ISugarQueryable<Role> Query(BaseArg<Role> arg, ISugarQueryable<Role> query)
		{
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
		public override ISugarQueryable<Role> Sort(ISugarQueryable<Role> query, string orderByField, OrderByType orderByType)
		{
			return orderByField switch
			{
				"Code" => query.OrderBy(o => o.Code, orderByType),
				"Name" => query.OrderBy(o => o.Name, orderByType),
				"Status" => query.OrderBy(o => o.Status, orderByType),
				"CreateTime" => query.OrderBy(o => o.CreateTime, orderByType),
				"UpdateTime" => query.OrderBy(o => o.UpdateTime, orderByType),
				_ => query.OrderBy(o => o.Id, orderByType)
			};
		}
	}
}
