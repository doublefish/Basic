using System.Collections.Generic;

namespace Basic.BLL
{
	/// <summary>
	/// IBLL
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IBLL<T> : IBLL<T, Model.PageArg.BaseArg<T>>
		where T : class, new()
	{
	}

	/// <summary>
	/// IBLL
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <typeparam name="P"></typeparam>
	/// <typeparam name="A"></typeparam>
	public interface IBLL<T, A> : IBLL<T, int, A>
		where T : class, new()
		where A : Model.PageArg.BaseArg<T>
	{

	}

	/// <summary>
	/// IBLL
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <typeparam name="P"></typeparam>
	/// <typeparam name="A"></typeparam>
	public interface IBLL<T, P, A>
		where T : class, new()
		where A : Model.PageArg.BaseArg<T>
	{
		/// <summary>
		/// 验证状态
		/// </summary>
		/// <param name="status"></param>
		/// <returns></returns>
		bool ValidateStatus(int status);

		/// <summary>
		/// 验证
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		string Validate(T data);

		/// <summary>
		/// 新增
		/// </summary>
		/// <param name="data"></param>
		void Add(T data);

		/// <summary>
		/// 修改
		/// </summary>
		/// <param name="data"></param>
		void Update(T data);

		/// <summary>
		/// 修改状态
		/// </summary>
		/// <param name="id"></param>
		/// <param name="status"></param>
		void UpdateStatus(int id, int status);

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="pkValue"></param>
		void Delete(P pkValue);

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="pkValue"></param>
		/// <param name="useCache"></param>
		T Get(P pkValue, bool useCache);

		/// <summary>
		/// 查询
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
