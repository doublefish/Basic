using Adai.Base;
using Adai.Standard.Model;
using Basic.DAL;
using Basic.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Basic.BLL
{
	/// <summary>
	/// AccountMailBLL
	/// </summary>
	public class AccountMailBLL : BLL<AccountMail>
	{
		/// <summary>
		/// Dal
		/// </summary>
		readonly AccountMailDAL Dal;

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="loginInfo"></param>
		public AccountMailBLL(Token<TokenData> loginInfo = null) : base(loginInfo)
		{
			IDal = Dal = new AccountMailDAL();
			if (Dal == null)
			{
				//为了编辑器不显示警告信息
			}
		}

		/// <summary>
		/// 验证
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public override string Validate(AccountMail data)
		{
			if (data.Id == 0)
			{
				var account = new AccountBLL().Get(data.AccountId, true);
				if (account == null)
				{
					return "用户不存在。";
				}
			}
			if (string.IsNullOrEmpty(data.Email))
			{
				return "电子邮箱不能为空。";
			}
			if (!ConfigIntHelper<Model.Config.Mail.Type>.ContainsKey(data.Type))
			{
				return "类型标识无效。";
			}
			if (!ValidateStatus(data.Status))
			{
				return "状态标识无效。";
			}
			data.UpdateTime = DateTime.Now;
			if (data.Id == 0)
			{
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
			var data = new AccountMail()
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
		public override void List(ICollection<AccountMail> list)
		{
			var accountIds = list.Select(o => o.AccountId).Distinct().ToArray();
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
