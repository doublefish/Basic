using Adai.Standard.Ext;
using Basic.Model;
using SqlSugar;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Linq;

namespace Basic.DAL
{
	/// <summary>
	/// DestinationDAL
	/// </summary>
	internal class DestinationDAL : DAL<Destination>
	{
		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="regionId"></param>
		/// <param name="useCache"></param>
		/// <returns></returns>
		public Destination GetByRegionId(int regionId, bool useCache = false)
		{
			if (useCache == true && IsCacheModel == true)
			{
				var key = string.Format("{0}-RegionId", CacheKey);
				var hashField = regionId;
				var pkValue = CacheDb.HashGet(key, hashField).ObjToInt();
				if (pkValue <= 0)
				{
					var result = GetByRegionId(regionId, false);
					if (result == null)
					{
						return null;
					}
					pkValue = result.Id;
					CacheDb.HashSet(key, hashField, pkValue);
				}
			}
			return Db.Queryable<Destination>().Where(o => o.RegionId == regionId).First();
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="take"></param>
		/// <param name="orderByField"></param>
		/// <param name="orderByType"></param>
		/// <returns></returns>
		public ICollection<Destination> List(int take, string orderByField = "Sequence", OrderByType orderByType = OrderByType.Asc)
		{
			var query = Db.Queryable<Destination>();
			query = Sort(query, orderByField, orderByType);
			return query.Take(take).Select(CreateSelector()).ToList();
		}

		/// <summary>
		/// 查询子节点
		/// </summary>
		/// <param name="parentId"></param>
		/// <param name="useCache"></param>
		/// <returns></returns>
		public virtual ICollection<Destination> ListByParentId(int parentId, bool useCache = false)
		{
			if (useCache == true && IsCacheModel == true)
			{
				var key = string.Format("{0}-ParentId", CacheKey);
				var hashField = parentId;
				var pkValues = CacheDb.HashGet<ICollection<int>>(key, hashField);
				if (pkValues == null || pkValues.Count == 0)
				{
					var ids = ListByParentId(Db, parentId).Select(o => o.Id).ToList();
					if (ids.Count == 0)
					{
						return new List<Destination>();
					}
					pkValues = ids;
					CacheDb.HashSet(key, hashField, pkValues);
				}
				return ListByPks(pkValues, true).ToArray();
			}
			return ListByParentId(Db, parentId).ToList();
		}

		/// <summary>
		/// 分页查询排序
		/// </summary>
		/// <param name="query"></param>
		/// <param name="orderByField"></param>
		/// <param name="orderByType"></param>
		/// <returns></returns>
		public override ISugarQueryable<Destination> Sort(ISugarQueryable<Destination> query, string orderByField, OrderByType orderByType)
		{
			return orderByField switch
			{
				"PopularIndex" => query.OrderBy(o => o.PopularIndex, orderByType),
				"Sequence" => query.OrderBy(o => o.Sequence, orderByType),
				"Status" => query.OrderBy(o => o.Status, orderByType),
				"CreateTime" => query.OrderBy(o => o.CreateTime, orderByType),
				"UpdateTime" => query.OrderBy(o => o.UpdateTime, orderByType),
				_ => query.OrderBy(o => o.Id, orderByType)
			};
		}

		/// <summary>
		/// 刷新缓存
		/// </summary>
		public override void RefreshCache()
		{
			var keys = new RedisKey[] {
				string.Format("{0}-RegionId", CacheKey),
				string.Format("{0}-ParentId", CacheKey)
			};
			CacheDb.KeyDelete(keys);
			base.RefreshCache();
		}

		#region 重复SQL

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="db"></param>
		/// <param name="parentId"></param>
		/// <returns></returns>
		private ISugarQueryable<Destination> ListByParentId(SqlSugarClient db, int parentId)
		{
			return db.Queryable<Destination>().Where(o => SqlFunc.Subqueryable<Region>().Where(r => r.Id == o.RegionId && r.ParentId == parentId).Any());
		}

		#endregion
	}
}
