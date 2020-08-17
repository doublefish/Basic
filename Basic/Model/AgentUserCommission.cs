using System;

namespace Basic.Model
{
	/// <summary>
	/// 代理商用户佣金
	/// </summary>
	public partial class AgentUserCommission
	{
		/// <summary>
		/// Desc:ID
		/// Default:
		/// Nullable:False
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Desc:代理商Id
		/// Default:
		/// Nullable:False
		/// </summary>
		public int AgentId { get; set; }

		/// <summary>
		/// Desc:代理商用户Id
		/// Default:
		/// Nullable:False
		/// </summary>
		public int AgentUserId { get; set; }

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
