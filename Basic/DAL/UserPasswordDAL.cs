using Basic.Model;
using SqlSugar;

namespace Basic.DAL
{
	/// <summary>
	/// UserPasswordDAL
	/// </summary>
	internal class UserPasswordDAL : DAL<UserPassword>
	{
		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="userId"></param>
		/// <param name="type"></param>
		/// <returns></returns>
		public UserPassword Get(int userId, int type)
		{
			return Db.Queryable<UserPassword>().Where(o => o.UserId == userId && o.Type == type).OrderBy(o => o.Id, OrderByType.Desc).First();
		}
	}
}
