using Adai.Standard.Ext;
using SqlSugar;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Basic.DAL
{
	/// <summary>
	/// TreeDAL
	/// </summary>
	/// <typeparam name="T"></typeparam>
	internal abstract class TreeDAL<T> : TreeDAL<T, Model.PageArg.BaseArg<T>>, ITreeDAL<T>
		where T : Model.TreeModel<T>, new()
	{
	}

	/// <summary>
	/// TreeDAL
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <typeparam name="A"></typeparam>
	internal abstract class TreeDAL<T, A> : DAL<T, A>, ITreeDAL<T, A>
		where T : Model.TreeModel<T>, new()
		where A : Model.PageArg.BaseArg<T>
	{
		/// <summary>
		/// 生成查询表达式
		/// </summary>
		/// <returns></returns>
		public override Expression<Func<T, T>> CreateBaseSelector()
		{
			return o => new T()
			{
				Id = o.Id,
				ParentId = o.ParentId,
				Parents = o.Parents,
				Code = o.Code,
				Name = o.Name,
				Sequence = o.Sequence,
				Status = o.Status,
				CreateTime = o.CreateTime,
				UpdateTime = o.UpdateTime,
				Note = o.Note
			};
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="code"></param>
		/// <param name="parentId"></param>
		/// <param name="useCache"></param>
		/// <returns></returns>
		public T GetByCode(string code, int? parentId, bool useCache = false)
		{
			if (useCache == true && IsCacheModel == true)
			{
				var key = string.Format("{0}-Code", CacheKey);
				var hashField = string.Format("{0}-{1}", parentId, code);
				var pkValue = CacheDb.HashGet(key, hashField).ObjToInt();
				if (pkValue <= 0)
				{
					var result = GetByCode(code, parentId, false);
					if (result == null)
					{
						return null;
					}
					pkValue = result.Id;
					CacheDb.HashSet(key, hashField, pkValue);
				}
				return Get(pkValue, true);
			}
			var query = Db.Queryable<T>().Where(o => o.Code == code);
			if (parentId.HasValue)
			{
				query = query.Where(o => o.ParentId == parentId.Value);
			}
			return query.First();
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="name"></param>
		/// <param name="parentId"></param>
		/// <param name="useCache"></param>
		/// <returns></returns>
		public T GetByName(string name, int? parentId, bool useCache = false)
		{
			if (useCache == true && IsCacheModel == true)
			{
				var key = string.Format("{0}-Name", CacheKey);
				var hashField = string.Format("{0}-{1}", name, parentId);
				var pkValue = CacheDb.HashGet(key, hashField).ObjToInt();
				if (pkValue <= 0)
				{
					var result = GetByName(name, parentId, false);
					if (result == null)
					{
						return null;
					}
					pkValue = result.Id;
					CacheDb.HashSet(key, hashField, pkValue);
				}
				return Get(pkValue, true);
			}
			var query = Db.Queryable<T>().Where(o => o.Name == name);
			if (parentId.HasValue)
			{
				query = query.Where(o => o.ParentId == parentId.Value);
			}
			return query.First();
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="parentId"></param>
		/// <param name="useCache"></param>
		/// <returns></returns>
		public virtual ICollection<T> ListByParentId(int parentId, bool useCache = false)
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
						return new List<T>();
					}
					pkValues = ids;
					CacheDb.HashSet(key, hashField, pkValues);
				}
				return ListByPks(pkValues, true).ToArray();
			}
			return ListByParentId(Db, parentId).ToList();
		}

		/// <summary>
		/// 查询子节点
		/// </summary>
		/// <param name="parentIds"></param>
		/// <param name="useCache"></param>
		/// <returns></returns>
		public virtual ICollection<T> ListByParentIds(ICollection<int> parentIds, bool useCache = false)
		{
			if (useCache == true && IsCacheModel == true)
			{
				var list = new List<T>();
				foreach (var parentId in parentIds)
				{
					var results = ListByParentId(parentId, true);
					list.AddRange(results);
				}
				return list;
			}
			return Db.Queryable<T>().Where(o => parentIds.Contains(o.ParentId)).Sort().ToList();
		}

		/// <summary>
		/// 查询所有子节点
		/// </summary>
		/// <param name="parentId"></param>
		/// <param name="useCache"></param>
		/// <returns></returns>
		public virtual ICollection<T> ListChildren(int parentId, bool useCache = false)
		{
			ICollection<T> results;
			if (useCache == true && IsCacheModel == true)
			{
				var key = string.Format("{0}-Children", CacheKey);
				var hashField = parentId;
				var pkValues = CacheDb.HashGet<ICollection<int>>(key, hashField);
				if (pkValues == null || pkValues.Count == 0)
				{
					var ids = ListChildren(Db, parentId).Select(o => o.Id).ToList();
					if (ids.Count == 0)
					{
						return new List<T>();
					}
					pkValues = ids;
					CacheDb.HashSet(key, hashField, pkValues);
				}
				results = ListByPks(pkValues, true);
			}
			else
			{
				results = ListChildren(Db, parentId).ToList();
			}
			return ModelHelper.Recursive(results, parentId);
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="ids"></param>
		/// <param name="useCache"></param>
		/// <returns></returns>
		public virtual IDictionary<int, bool> ExistChild(ICollection<int> ids, bool useCache = false)
		{
			if (useCache == true && IsCacheModel == true)
			{
				var key = string.Format("{0}-ExistChild", CacheKey);
				var hashFields = new List<RedisValue>();
				foreach (var id in ids)
				{
					hashFields.Add(id.ToString());
				}
				var values = CacheDb.HashGet(key, hashFields.ToArray());
				values = values.Where(o => o.HasValue == true).ToArray();
				if (values == null || values.Length == 0 || values.Length != ids.Count)
				{
					var datas = ExistChild(ids, false) as Dictionary<int, bool>;
					var hashEntries = new List<HashEntry>();
					foreach (var data in datas)
					{
						hashEntries.Add(new HashEntry(data.Key, data.Value ? 1 : 0));
					}
					CacheDb.HashSet(key, hashEntries.ToArray());
					return datas;
				}
				else
				{
					var datas = new Dictionary<int, bool>();
					for (var i = 0; i < ids.Count; i++)
					{
						datas.Add(ids.ElementAt(i), values.ElementAt(i).ObjToInt32() == 1);
					}
					return datas;
				}
			}
			var results = Db.Queryable<T>().Where(o => ids.Contains(o.Id)).Select(o => new
			{
				o.Id,
				HasChild = SqlFunc.Subqueryable<T>().Where(c => c.ParentId == o.Id).Any()
			}).ToList();
			var dic = new Dictionary<int, bool>();
			foreach (var result in results)
			{
				dic.Add(result.Id, result.HasChild);
			}
			return dic;
		}

		/// <summary>
		/// 刷新缓存
		/// </summary>
		public override void RefreshCache()
		{
			var keys = new RedisKey[] {
				string.Format("{0}-Code", CacheKey),
				string.Format("{0}-Name", CacheKey),
				string.Format("{0}-ParentId", CacheKey),
				string.Format("{0}-Children", CacheKey),
				string.Format("{0}-ExistChild", CacheKey)
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
		private ISugarQueryable<T> ListByParentId(SqlSugarClient db, int parentId)
		{
			return db.Queryable<T>().Where(o => o.ParentId == parentId).Sort();
		}

		/// <summary>
		/// 查询子节点
		/// </summary>
		/// <param name="db"></param>
		/// <param name="parentId"></param>
		/// <returns></returns>
		private ISugarQueryable<T> ListChildren(SqlSugarClient db, int parentId)
		{
			return db.Queryable<T>().Where(o => o.Parents.Contains(string.Format(",{0},", parentId))).Sort();
		}

		#endregion
	}
}
