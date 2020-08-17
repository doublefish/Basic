using System;

namespace Basic.Model
{
	/// <summary>
	/// 代理商佣金规则
	/// </summary>
	public partial class AgentCommissionRule
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
		/// Desc:产品Id
		/// Default:
		/// Nullable:False
		/// </summary>
		public int ProductId { get; set; }

		/// <summary>
		/// Desc:年份
		/// Default:
		/// Nullable:False
		/// </summary>
		public int Year { get; set; }

		/// <summary>
		/// Desc:月份
		/// Default:
		/// Nullable:False
		/// </summary>
		public int Month { get; set; }

		/// <summary>
		/// Desc:下级佣金比例
		/// Default:
		/// Nullable:False
		/// </summary>
		public decimal Rate { get; set; }

		/// <summary>
		/// Desc:下级佣金金额
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
