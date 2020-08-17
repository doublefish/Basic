using Adai.Standard.Models;
using Basic.DAL;
using Basic.Model;
using Basic.Model.PageArg;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Basic.BLL
{
	/// <summary>
	/// AccountBankCardBLL
	/// </summary>
	public class AccountBankCardBLL : BLL<AccountBankCard, BankCardArg<AccountBankCard>>
	{
		/// <summary>
		/// Dal
		/// </summary>
		readonly AccountBankCardDAL Dal;

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="loginInfo"></param>
		public AccountBankCardBLL(Token<TokenData> loginInfo = null) : base(loginInfo)
		{
			IDal = Dal = new AccountBankCardDAL();
		}

		/// <summary>
		/// 验证
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public override string Validate(AccountBankCard data)
		{
			var bank = new DictBLL().Get(data.BankId, true);
			if (bank == null)
			{
				return "银行不存在。";
			}
			if (string.IsNullOrEmpty(data.CardNumber))
			{
				return "卡号不能为空。";
			}
			if (string.IsNullOrEmpty(data.Cardholder))
			{
				return "持卡人不能为空。";
			}
			if (string.IsNullOrEmpty(data.Branch))
			{
				return "支行不能为空。";
			}
			data.UpdateTime = DateTime.Now;
			if (data.Id == 0)
			{
				data.AccountId = LoginInfo.Id;
				data.CreateTime = data.UpdateTime;
			}
			return base.Validate(data);
		}

		/// <summary>
		/// 关联数据
		/// </summary>
		/// <param name="list"></param>
		public override void List(ICollection<AccountBankCard> list)
		{
			var accountIds = new HashSet<int>();
			var dictIds = new HashSet<int>();
			foreach (var data in list)
			{
				accountIds.Add(data.AccountId);
				dictIds.Add(data.BankId);
			}
			var accounts = new AccountBLL().ListByPks(accountIds, true);
			var dicts = new DictBLL().ListByPks(dictIds, true);
			foreach (var data in list)
			{
				data.Account = accounts.Where(o => o.Id == data.AccountId).FirstOrDefault();
				data.BankName = dicts.GetName(data.BankId);
			}
		}

		#region 扩展

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="accountId"></param>
		/// <returns></returns>
		public AccountBankCard GetByAccountId(int accountId)
		{
			return Dal.GetByAccountId(accountId);
		}

		#endregion
	}
}
