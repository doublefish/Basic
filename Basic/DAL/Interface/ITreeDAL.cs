using System.Collections.Generic;

namespace Basic.DAL
{
	/// <summary>
	/// ITreeDAL
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface ITreeDAL<T> : ITreeDAL<T, Model.PageArg.BaseArg<T>>
		where T : Model.TreeModel<T>, new()
	{
	}

	/// <summary>
	/// ITreeTranslationDAL
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <typeparam name="A"></typeparam>
	public interface ITreeDAL<T, A> : IDAL<T, A>
		where T : Model.TreeModel<T>, new()
		where A : Model.PageArg.BaseArg<T>
	{
		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="code"></param>
		/// <param name="parentId"></param>
		/// <param name="useCache"></param>
		/// <returns></returns>
		T GetByCode(string code, int? parentId, bool useCache = false);

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="name"></param>
		/// <param name="parentId"></param>
		/// <param name="useCache"></param>
		/// <returns></returns>
		T GetByName(string name, int? parentId, bool useCache = false);

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="parentId"></param>
		/// <param name="useCache"></param>
		/// <returns></returns>
		ICollection<T> ListByParentId(int parentId, bool useCache = false);

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="parentIds"></param>
		/// <param name="useCache"></param>
		/// <returns></returns>
		ICollection<T> ListByParentIds(ICollection<int> parentId, bool useCache = false);

		/// <summary>
		/// 查询所有子节点
		/// </summary>
		/// <param name="parentId"></param>
		/// <param name="useCache"></param>
		/// <returns></returns>
		ICollection<T> ListChildren(int parentId, bool useCache = false);

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="ids"></param>
		/// <param name="useCache"></param>
		/// <returns></returns>
		IDictionary<int, bool> ExistChild(ICollection<int> ids, bool useCache = false);
	}
}
