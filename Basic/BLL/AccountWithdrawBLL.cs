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
	/// AccountWithdrawBLL
	/// </summary>
	public class AccountWithdrawBLL : BLL<AccountWithdraw, BankCardArg<AccountWithdraw>>
	{
		/// <summary>
		/// Dal
		/// </summary>
		readonly AccountWithdrawDAL Dal;

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="loginInfo"></param>
		public AccountWithdrawBLL(Token<TokenData> loginInfo = null) : base(loginInfo)
		{
			IDal = Dal = new AccountWithdrawDAL();
		}

		/// <summary>
		/// 验证
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public override string Validate(AccountWithdraw data)
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
				data.Status = Model.Config.StatusOfProcess.Applied;
				data.CreateTime = data.UpdateTime;
			}
			return base.Validate(data);
		}

		/// <summary>
		/// 修改状态
		/// </summary>
		/// <param name="id"></param>
		/// <param name="status"></param>
		public override void UpdateStatus(int id, int status)
		{
			if (!ValidateStatus(status))
			{
				throw new CustomException("状态标识无效。");
			}
			var data = new AccountWithdraw()
			{
				Id = id,
				Status = status
			};
			Dal.Update(data, new string[] { "Status" });
		}

		/// <summary>
		/// 关联数据
		/// </summary>
		/// <param name="list"></param>
		public override void List(ICollection<AccountWithdraw> list)
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

		#endregion
	}
}
