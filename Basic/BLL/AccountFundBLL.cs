using Adai.Standard.Models;
using Basic.DAL;
using Basic.Model;
using System.Collections.Generic;
using System.Linq;

namespace Basic.BLL
{
	/// <summary>
	/// AccountFundBLL
	/// </summary>
	public class AccountFundBLL : BLL<AccountFund>
	{
		/// <summary>
		/// Dal
		/// </summary>
		readonly AccountFundDAL Dal;

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="loginInfo"></param>
		public AccountFundBLL(Token<TokenData> loginInfo = null) : base(loginInfo)
		{
			IDal = Dal = new AccountFundDAL();
			if (Dal == null)
			{
				//为了编辑器不显示警告信息
			}
		}

		/// <summary>
		/// 关联数据
		/// </summary>
		/// <param name="list"></param>
		public override void List(ICollection<AccountFund> list)
		{
			var accountIds = new HashSet<int>();
			foreach (var data in list)
			{
				accountIds.Add(data.AccountId);
			}
			var accounts = new AccountBLL().ListByPks(accountIds, true);
			foreach (var data in list)
			{
				data.Account = accounts.Where(o => o.Id == data.AccountId).FirstOrDefault();
			}
		}

		#region 扩展

		#endregion
	}
}
