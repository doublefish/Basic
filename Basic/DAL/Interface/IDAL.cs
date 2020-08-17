using System.Collections.Generic;

namespace Basic.DAL
{
	/// <summary>
	/// IDAL
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IDAL<T> : IDAL<T, Model.PageArg.BaseArg<T>>
		where T : class, new()
	{
	}

	/// <summary>
	/// IDAL
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <typeparam name="A"></typeparam>
	public interface IDAL<T, A> : IDAL<T, int, A>
		where T : class, new()
		where A : Model.PageArg.BaseArg<T>
	{
	}

	/// <summary>
	/// IDAL
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <typeparam name="P"></typeparam>
	/// <typeparam name="A"></typeparam>
	public interface IDAL<T, P, A>
		where T : class, new()
		where A : Model.PageArg.BaseArg<T>
	{
		/// <summary>
		/// 新增
		/// </summary>
		/// <param name="data"></param>
		void Add(T data);

		/// <summary>
		/// 新增（批量）
		/// </summary>
		/// <param name="data"></param>
		void Add(ICollection<T> datas);

		/// <summary>
		/// 修改
		/// </summary>
		/// <param name="data"></param>
		/// <param name="propertyNames"></param>
		void Update(T data, params string[] propertyNames);

		/// <summary>
		/// 修改（批量）
		/// </summary>
		/// <param name="data"></param>
		/// <param name="propertyNames"></param>
		void Update(ICollection<T> datas, params string[] propertyNames);

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="pkValue"></param>
		void Delete(P pkValue);

		/// <summary>
		/// 删除（批量）
		/// </summary>
		/// <param name="pkValues"></param>
		void Delete(ICollection<P> pkValues);

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="pkValue"></param>
		/// <param name="useCache"></param>
		T Get(P pkValue, bool useCache = false);

		/// <summary>
		/// 根据主键查询
		/// </summary>
		/// <param name="pkValues"></param>
		/// <param name="useCache"></param>
		ICollection<T> ListByPks(ICollection<P> pkValues, bool useCache = false);

		/// <summary>
		/// 查询所有
		/// </summary>
		/// <param name="useCache"></param>
		/// <returns></returns>
		ICollection<T> ListAll(bool useCache = false);

		/// <summary>
		/// 分页查询
		/// </summary>
		/// <param name="arg"></param>
		/// <returns></returns>
		A List(A arg);

		/// <summary>
		/// 刷新缓存
		/// </summary>
		void RefreshCache();
	}
}
