using Basic.Model;
using SqlSugar;

namespace Basic.DAL
{
	/// <summary>
	/// AccountPasswordDAL
	/// </summary>
	internal class AccountPasswordDAL : DAL<AccountPassword>
	{
		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="accountId"></param>
		/// <param name="type"></param>
		/// <returns></returns>
		public AccountPassword Get(int accountId, int type)
		{
			return Db.Queryable<AccountPassword>().Where(o => o.AccountId == accountId && o.Type == type).OrderBy(o => o.Id, OrderByType.Desc).First();
		}
	}
}
