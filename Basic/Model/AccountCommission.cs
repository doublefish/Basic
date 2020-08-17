using System;

namespace Basic.Model
{
	/// <summary>
	/// 用户佣金
	/// </summary>
	public partial class AccountCommission
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
		/// Desc:订单Id
		/// Default:
		/// Nullable:False
		/// </summary>
		public int OrderId { get; set; }

		/// <summary>
		/// Desc:比例
		/// Default:
		/// Nullable:False
		/// </summary>
		public decimal Rate { get; set; }

		/// <summary>
		/// Desc:金额
		/// Default:
		/// Nullable:False
		/// </summary>
		public decimal Amount { get; set; }

		/// <summary>
		/// Desc:状态
		/// Default:
		/// Nullable:False
		/// </summary>
		public int Status { get; set; }

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
