﻿using System;

namespace Basic.Model
{
	/// <summary>
	/// 用户银行卡
	/// </summary>
	public partial class AccountBankCard
	{
		/// <summary>
		/// Desc:ID
		/// Default:
		/// Nullable:False
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Desc:用户Id
		/// Default:
		/// Nullable:False
		/// </summary>
		public int AccountId { get; set; }

		/// <summary>
		/// Desc:银行Id
		/// Default:
		/// Nullable:False
		/// </summary>
		public int BankId { get; set; }

		/// <summary>
		/// Desc:卡号
		/// Default:
		/// Nullable:False
		/// </summary>
		public string CardNumber { get; set; }

		/// <summary>
		/// Desc:持卡人
		/// Default:
		/// Nullable:False
		/// </summary>
		public string Cardholder { get; set; }

		/// <summary>
		/// Desc:支行
		/// Default:
		/// Nullable:False
		/// </summary>
		public string Branch { get; set; }

		/// <summary>
		/// Desc:创建时间
		/// Default:
		/// Nullable:False
		/// </summary>
		public DateTime CreateTime { get; set; }

		/// <summary>
		/// Desc:修改时间
		/// Default:
		/// Nullable:False
		/// </summary>
		public DateTime UpdateTime { get; set; }

		/// <summary>
		/// Desc:说明
		/// Default:
		/// Nullable:True
		/// </summary>
		public string Note { get; set; }
	}
}
