using Adai.Base.Ext;
using Adai.Standard;
using Adai.Standard.Ext;
using Adai.Standard.Model;
using SqlSugar;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;

namespace Basic.DAL
{
	/// <summary>
	/// DAL
	/// </summary>
	/// <typeparam name="T"></typeparam>
	internal abstract class DAL<T> : DAL<T, Model.PageArg.BaseArg<T>>, IDAL<T>
		where T : class, new()
	{
	}

	/// <summary>
	/// DAL
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <typeparam name="A"></typeparam>
	internal abstract class DAL<T, A> : DAL<T, int, A>, IDAL<T, A>
		where T : class, new()
		where A : Model.PageArg.BaseArg<T>
	{
		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="id"></param>
		/// <param name="useCache"></param>
		/// <returns></returns>
		public override T Get(int id, bool useCache = false)
		{
			return base.Get(id, useCache);
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="id"></param>
		public override void Delete(int id)
		{
			base.Delete(id);
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="ids"></param>
		public override void Delete(ICollection<int> ids)
		{
			base.Delete(ids);
		}

		/// <summary>
		/// 删除被引用数据
		/// </summary>
		/// <param name="id"></param>
		public override void DeleteReferenced(int id)
		{
			base.DeleteReferenced(id);
		}

		/// <summary>
		/// 删除被引用数据
		/// </summary>
		/// <param name="ids"></param>
		public override void DeleteReferenced(ICollection<int> ids)
		{
			base.DeleteReferenced(ids);
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="ids"></param>
		/// <param name="useCache"></param>
		/// <returns></returns>
		public override ICollection<T> ListByPks(ICollection<int> ids, bool useCache = false)
		{
			return base.ListByPks(ids, useCache);
		}
	}

	/// <summary>
	/// DAL
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <typeparam name="P"></typeparam>
	/// <typeparam name="A"></typeparam>
	internal abstract class DAL<T, P, A> : IDAL<T, P, A>
		where T : class, new()
		where A : Model.PageArg.BaseArg<T>
	{
		Type type;
		Type modelType;
		SqlSugarClient db;
		IDatabase cacheDb;
		bool? isCacheModel;

		/// <summary>
		/// 主键名称
		/// </summary>
		public string PkName { get; private set; }

		/// <summary>
		/// 当前类型
		/// </summary>
		public Type Type
		{
			get
			{
				if (type == null)
				{
					type = GetType();
				}
				return type;
			}
		}

		/// <summary>
		/// 实体类型
		/// </summary>
		public Type ModelType
		{
			get
			{
				if (modelType == null)
				{
					modelType = typeof(T);
				}
				return modelType;
			}
		}

		/// <summary>
		/// 语言
		/// </summary>
		public string Language => CultureInfo.CurrentCulture.ToString();

		/// <summary>
		/// 实体类型
		/// </summary>
		public string CacheKey => string.Format("{0}", ModelType.FullName);

		/// <summary>
		/// 数据库
		/// </summary>
		protected SqlSugarClient Db
		{
			get
			{
				if (db == null)
				{
					db = DbHelper.CreateDb();
				}
				return db;
			}
		}

		/// <summary>
		/// 缓存数据库
		/// </summary>
		protected IDatabase CacheDb
		{
			get
			{
				if (cacheDb == null)
				{
					cacheDb = RedisHelper.Db;
				}
				return cacheDb;
			}
		}

		/// <summary>
		/// 是否需要缓存
		/// </summary>
		public bool IsCacheModel
		{
			get
			{
				if (isCacheModel == null)
				{
					isCacheModel = DbHelper.IsCacheModel(ModelType.Name);
				}
				return isCacheModel.Value;
			}
		}

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="pkName"></param>
		public DAL(string pkName = "Id")
		{
			PkName = pkName;
		}

		/// <summary>
		/// 获取主键
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		protected virtual P GetPkValue(T data)
		{
			var pkValue = data.GetValue(PkName, ModelType);
			if (pkValue == null)
			{
				throw new Exception("未读取到主键");
			}
			if (pkValue.Equals(""))
			{
				throw new Exception("未读取到主键的值");
			}
			return (P)pkValue;
		}

		/// <summary>
		/// 获取主键
		/// </summary>
		/// <param name="datas"></param>
		/// <returns></returns>
		protected virtual ICollection<P> GetPkValues(ICollection<T> datas)
		{
			var pkValues = new HashSet<P>();
			foreach (var data in datas)
			{
				pkValues.Add(GetPkValue(data));
			}
			return pkValues;
		}

		/// <summary>
		/// 新增
		/// </summary>
		/// <param name="data"></param>
		public virtual void Add(T data)
		{
			Db.Insertable(data).ExecuteCommandIdentityIntoEntity();
		}

		/// <summary>
		/// 新增（批量）
		/// </summary>
		/// <param name="datas"></param>
		public virtual void Add(ICollection<T> datas)
		{
			Db.Insertable(datas.ToArray()).ExecuteCommandIdentityIntoEntity();
		}

		/// <summary>
		/// 修改
		/// </summary>
		/// <param name="data"></param>
		/// <param name="propertyNames"></param>
		public virtual void Update(T data, params string[] propertyNames)
		{
			if (propertyNames == null || propertyNames.Length == 0)
			{
				propertyNames = GetUpdateColumns().ToArray();
			}
			try
			{
				//重载指定事务的级别
				Db.Ado.BeginTran(System.Data.IsolationLevel.ReadCommitted);

				Db.Updateable(data).UpdateColumns(propertyNames).ExecuteCommand();
				DeleteCache(data);

				Db.Ado.CommitTran();
			}
			catch
			{
				Db.Ado.RollbackTran();
				throw;
			}
		}

		/// <summary>
		/// 修改（批量）
		/// </summary>
		/// <param name="datas"></param>
		/// <param name="propertyNames"></param>
		public virtual void Update(ICollection<T> datas, params string[] propertyNames)
		{
			if (propertyNames == null || propertyNames.Length == 0)
			{
				propertyNames = GetUpdateColumns().ToArray();
			}
			try
			{
				//重载指定事务的级别
				Db.Ado.BeginTran(System.Data.IsolationLevel.ReadCommitted);

				Db.Updateable(datas.ToArray()).UpdateColumns(propertyNames).ExecuteCommand();
				DeleteCache(datas);

				Db.Ado.CommitTran();
			}
			catch
			{
				Db.Ado.RollbackTran();
				throw;
			}
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="pkValue"></param>
		public virtual void Delete(P pkValue)
		{
			try
			{
				//重载指定事务的级别
				Db.Ado.BeginTran(System.Data.IsolationLevel.ReadCommitted);

				Db.Deleteable<T>().In(pkValue).ExecuteCommand();
				DeleteReferenced(pkValue);
				DeleteCache(pkValue);

				Db.Ado.CommitTran();
			}
			catch
			{
				Db.Ado.RollbackTran();
				throw;
			}
		}

		/// <summary>
		/// 删除（批量）
		/// </summary>
		/// <param name="pkValue"></param>
		public virtual void Delete(ICollection<P> pkValues)
		{
			try
			{
				//重载指定事务的级别
				Db.Ado.BeginTran(System.Data.IsolationLevel.ReadCommitted);

				Db.Deleteable<T>().In(pkValues.ToArray()).ExecuteCommand();
				DeleteReferenced(pkValues);
				DeleteCache(pkValues);

				Db.Ado.CommitTran();
			}
			catch
			{
				Db.Ado.RollbackTran();
				throw;
			}
		}

		/// <summary>
		/// 删除被引用数据
		/// </summary>
		/// <param name="pkValue"></param>
		public virtual void DeleteReferenced(P pkValue)
		{
		}

		/// <summary>
		/// 删除被引用数据
		/// </summary>
		/// <param name="pkValues"></param>
		public virtual void DeleteReferenced(ICollection<P> pkValues)
		{
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="pkValue"></param>
		/// <param name="useCache"></param>
		/// <returns></returns>
		public virtual T Get(P pkValue, bool useCache = false)
		{
			if (useCache == true && IsCacheModel == true)
			{
				var hashField = pkValue.ToString();
				var data = CacheDb.HashGet<T>(CacheKey, hashField);
				if (data == null)
				{
					data = Get(pkValue, false);
					CacheDb.HashSet(CacheKey, hashField, data);
				}
				return data;
			}
			return Db.Queryable<T>().InSingle(pkValue);
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="pkValues"></param>
		/// <param name="useCache"></param>
		/// <returns></returns>
		public virtual ICollection<T> ListByPks(ICollection<P> pkValues, bool useCache = false)
		{
			if (pkValues == null || pkValues.Count == 0)
			{
				return new T[] { };
			}
			if (useCache == true && IsCacheModel == true)
			{
				var key = CacheKey;
				var hashFields = new List<RedisValue>();
				foreach (var pkValue in pkValues)
				{
					hashFields.Add(pkValue.ToString());
				}
				var datas = CacheDb.HashGet<T>(key, hashFields.ToArray());
				if (datas == null || datas.Count == 0 || datas.Count != pkValues.Count)
				{
					datas = ListByPks(pkValues, false);
					var hashEntries = new List<HashEntry>();
					foreach (var data in datas)
					{
						var pkValue = GetPkValue(data);
						hashEntries.Add(new HashEntry(pkValue.ToString(), JsonHelper.SerializeObject(data)));
					}
					CacheDb.HashSet(key, hashEntries.ToArray());
				}
				return datas;
			}
			return Db.Queryable<T>().In(pkValues).ToList(Selector).ToArray();
		}

		/// <summary>
		/// 查询所有
		/// </summary>
		/// <param name="useCache"></param>
		/// <returns></returns>
		public virtual ICollection<T> ListAll(bool useCache = false)
		{
			if (useCache == true && IsCacheModel == true)
			{
				var key = CacheKey;
				var datas = CacheDb.HashValues<T>(key);
				var count = Db.Queryable<T>().Count();
				if (datas == null || datas.Count != count)
				{
					datas = ListAll(false);
					var hashEntries = new List<HashEntry>();
					foreach (var data in datas)
					{
						var pkValue = GetPkValue(data);
						hashEntries.Add(new HashEntry(pkValue.ToString(), JsonHelper.SerializeObject(data)));
					}
					CacheDb.HashSet(key, hashEntries.ToArray());
				}
				return datas;
			}
			return Db.Queryable<T>().ToList(Selector).ToArray();
		}

		/// <summary>
		/// 分页查询
		/// </summary>
		/// <param name="arg"></param>
		/// <returns></returns>
		public virtual A List(A arg)
		{
			var query = Db.Queryable<T>();
			//查询条件
			query = Query(arg, query);
			//排序方式
			var orderByType = arg.SortType == Adai.Standard.Model.SortType.Asc ? OrderByType.Asc : OrderByType.Desc;
			query = Sort(query, arg.SortName, orderByType);
			//统计方式
			if (arg.CountFlag != StatisticFlag.Not)
			{
				arg.TotalCount = query.Count();
				if (arg.CountFlag == StatisticFlag.Only)
				{
					return arg;
				}
			}
			//分页
			if (arg.PageSize > 0)
			{
				query = query.Skip(arg.PageSize * arg.PageNumber).Take(arg.PageSize);
			}
			//查询字段
			arg.Results = query.ToList(Selector);
			return arg;
		}

		/// <summary>
		/// 分页查询条件
		/// </summary>
		/// <param name="arg"></param>
		/// <param name="query"></param>
		/// <param name="orderByType"></param>
		/// <returns></returns>
		public virtual ISugarQueryable<T> Query(A arg, ISugarQueryable<T> query)
		{
			return query;
		}

		/// <summary>
		/// 分页查询排序
		/// </summary>
		/// <param name="query"></param>
		/// <param name="orderByField"></param>
		/// <param name="orderByType"></param>
		/// <returns></returns>
		public virtual ISugarQueryable<T> Sort(ISugarQueryable<T> query, string orderByField, OrderByType orderByType)
		{
			return query;
		}

		/// <summary>
		/// 获取需要更新的列
		/// </summary>
		/// <returns></returns>
		private ICollection<string> GetUpdateColumns()
		{
			if (ModelHelper.UpdateColumns.TryGetValue(ModelType.Name, out var columns))
			{
				return columns;
			}
			else
			{
				return ModelHelper.GetPropertyNames(ModelType);
			}
		}

		#region 提升性能

		Expression<Func<T, T>> selector;
		/// <summary>
		/// 查询表达式
		/// </summary>
		public Expression<Func<T, T>> Selector
		{
			get
			{
				if (selector == null)
				{
					selector = CreateSelector();
				}
				return selector;
			}
		}

		/// <summary>
		/// 创建查询表达式
		/// </summary>
		/// <returns></returns>
		public virtual Expression<Func<T, T>> CreateSelector()
		{
			var baseExpression = CreateBaseSelector();
			if (baseExpression == null)
			{
				return null;
			}
			var thisExpression = CreatePersonalSelector();
			if (thisExpression == null)
			{
				return baseExpression;
			}

			var baseInit = baseExpression.Body as MemberInitExpression;
			var thisInit = thisExpression.Body as MemberInitExpression;
			var bindings = baseInit.Bindings.Concat(thisInit.Bindings);

			var body = Expression.MemberInit(Expression.New(ModelType), bindings);
			var expression = Expression.Lambda<Func<T, T>>(body, thisExpression.Parameters);

			return expression;
		}

		/// <summary>
		/// 创建查询表达式
		/// </summary>
		/// <returns></returns>
		public virtual Expression<Func<T, T>> CreateBaseSelector()
		{
			//return o => new T()
			//{
			//	Id = o.Id,
			//	Status = o.Status,
			//	CreateTime = o.CreateTime,
			//	UpdateTime = o.UpdateTime,
			//	Note = o.Note
			//};
			return null;
		}

		/// <summary>
		/// 创建个性化查询表达式
		/// </summary>
		/// <returns></returns>
		public virtual Expression<Func<T, T>> CreatePersonalSelector()
		{
			return null;
		}

		#endregion

		#region 缓存

		/// <summary>
		/// 刷新缓存
		/// </summary>
		public virtual void RefreshCache()
		{
			var key = string.Format("{0}", CacheKey);
			CacheDb.KeyDelete(key);
		}

		/// <summary>
		/// 更新缓存
		/// </summary>
		/// <param name="data"></param>
		public virtual void AddCache(T data)
		{
			if (IsCacheModel == false) return;
			var pkValue = GetPkValue(data);
			CacheDb.HashSet(CacheKey, pkValue.ToString(), data);
		}

		/// <summary>
		/// 更新缓存（批量）
		/// </summary>
		/// <param name="datas"></param>
		public virtual void AddCache(ICollection<T> datas)
		{
			if (IsCacheModel == false) return;
			var hashEntries = new List<HashEntry>();
			foreach (var data in datas)
			{
				var pkValue = GetPkValue(data);
				hashEntries.Add(new HashEntry(pkValue.ToString(), JsonHelper.SerializeObject(data)));
			}
			CacheDb.HashSet(CacheKey, hashEntries.ToArray());
		}

		/// <summary>
		/// 删除缓存
		/// </summary>
		/// <param name="pkValue"></param>
		/// <returns></returns>
		public virtual void DeleteCache(P pkValue)
		{
			if (IsCacheModel == false) return;
			CacheDb.HashDelete(CacheKey, pkValue.ToString());
		}

		/// <summary>
		/// 删除缓存（批量）
		/// </summary>
		/// <param name="pkValues"></param>
		/// <returns></returns>
		public virtual void DeleteCache(ICollection<P> pkValues)
		{
			if (IsCacheModel == false) return;
			var hashFields = new RedisValue[pkValues.Count];
			for (int i = 0; i < pkValues.Count; i++)
			{
				hashFields[i] = pkValues.ElementAt(i).ToString();
			}
			CacheDb.HashDelete(CacheKey, hashFields);
		}

		/// <summary>
		/// 删除缓存
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public virtual void DeleteCache(T data)
		{
			if (IsCacheModel == false) return;
			var pkValue = GetPkValue(data);
			CacheDb.HashDelete(CacheKey, pkValue.ToString());
		}

		/// <summary>
		/// 删除缓存（批量）
		/// </summary>
		/// <param name="datas"></param>
		/// <returns></returns>
		public virtual void DeleteCache(ICollection<T> datas)
		{
			if (IsCacheModel == false) return;
			var pkValues = GetPkValues(datas);
			DeleteCache(pkValues);
		}

		#endregion
	}
}
