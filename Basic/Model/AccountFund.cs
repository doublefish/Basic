using System;

namespace Basic.Model
{
	/// <summary>
	/// 用户资金流水
	/// </summary>
	public partial class AccountFund
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
		/// Desc:编号
		/// Default:
		/// Nullable:False
		/// </summary>
		public string Number { get; set; }

		/// <summary>
		/// Desc:类型
		/// Default:
		/// Nullable:False
		/// </summary>
		public int Type { get; set; }

		/// <summary>
		/// Desc:金额
		/// Default:
		/// Nullable:False
		/// </summary>
		public decimal Amount { get; set; }

		/// <summary>
		/// Desc:账户余额
		/// Default:
		/// Nullable:True
		/// </summary>
		public decimal Balance { get; set; }

		/// <summary>
		/// Desc:冻结金额
		/// Default:
		/// Nullable:True
		/// </summary>
		public decimal Freeze { get; set; }

		/// <summary>
		/// Desc:创建时间
		/// Default:
		/// Nullable:False
		/// </summary>
		public DateTime CreateTime { get; set; }

		/// <summary>
		/// Desc:说明
		/// Default:
		/// Nullable:True
		/// </summary>
		public string Note { get; set; }
	}
}
