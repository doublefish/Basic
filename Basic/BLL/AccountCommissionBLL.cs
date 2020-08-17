using Adai.Standard;
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
	/// AccountCommissionBLL
	/// </summary>
	public class AccountCommissionBLL : BLL<AccountCommission, CommissionArg<AccountCommission>>
	{
		/// <summary>
		/// Dal
		/// </summary>
		readonly AccountCommissionDAL Dal;

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="loginInfo"></param>
		public AccountCommissionBLL(Token<TokenData> loginInfo = null) : base(loginInfo)
		{
			IDal = Dal = new AccountCommissionDAL();
		}

		/// <summary>
		/// 验证状态
		/// </summary>
		/// <param name="status"></param>
		/// <returns></returns>
		public override bool ValidateStatus(int status)
		{
			return ConfigIntHelper<Model.Config.StatusOfPayment>.ContainsKey(status);
		}

		/// <summary>
		/// 验证
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public override string Validate(AccountCommission data)
		{
			if (data.Id == 0)
			{
				var account = new AccountBLL().Get(data.AccountId, true);
				if (account == null)
				{
					return "用户不存在。";
				}
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
			var data = new AccountCommission()
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
		public override void List(ICollection<AccountCommission> list)
		{
			var accountIds = new HashSet<int>();
			var orderIds = new HashSet<int>();
			foreach (var data in list)
			{
				accountIds.Add(data.AccountId);
				orderIds.Add(data.OrderId);
			}
			var accounts = new AccountBLL().ListByPks(accountIds, true);
			var orders = new OrderBLL().ListByPks(orderIds, true);
			foreach (var data in list)
			{
				data.Account = accounts.Where(o => o.Id == data.AccountId).FirstOrDefault();
				var order = orders.Where(o => o.Id == data.OrderId).FirstOrDefault();
				if (order != null)
				{
					data.OrderNumber = order.Number;
					data.OrderAmount = order.TotalPrice ?? decimal.Zero;
				}
			}
		}

		#region 扩展

		#endregion
	}
}
