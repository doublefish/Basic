using System.Collections.Generic;

namespace Basic.BLL
{
	/// <summary>
	/// ITreeBLL
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface ITreeBLL<T> : ITreeBLL<T, Model.PageArg.BaseArg<T>>
		where T : Model.TreeModel<T>, new()
	{
	}

	/// <summary>
	/// ITreeBLL
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <typeparam name="A"></typeparam>
	public interface ITreeBLL<T, A> : IBLL<T, A>
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
		/// <param name="parentCode"></param>
		/// <param name="useCache"></param>
		/// <returns></returns>
		ICollection<T> ListByParentCode(string parentCode, bool useCache = false);

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="parentId"></param>
		/// <param name="useCache"></param>
		/// <returns></returns>
		ICollection<T> ListByParentId(int parentId, bool useCache = false);

		/// <summary>
		/// 查询所有子节点
		/// </summary>
		/// <param name="parentId"></param>
		/// <param name="useCache"></param>
		/// <returns></returns>
		ICollection<T> ListChildren(int parentId, bool useCache = false);
	}
}
