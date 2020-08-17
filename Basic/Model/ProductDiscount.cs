using System;

namespace Basic.Model
{
	/// <summary>
	/// 产品折扣
	/// </summary>
	public partial class ProductDiscount
	{
		/// <summary>
		/// Desc:ID
		/// Default:
		/// Nullable:False
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Desc:产品Id
		/// Default:
		/// Nullable:False
		/// </summary>
		public int ProductId { get; set; }

		/// <summary>
		/// Desc:名称
		/// Default:
		/// Nullable:False
		/// </summary>
		public string Name { get; set; }

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
		/// Desc:总数
		/// Default:
		/// Nullable:False
		/// </summary>
		public int Total { get; set; }

		/// <summary>
		/// Desc:已用数量
		/// Default:
		/// Nullable:False
		/// </summary>
		public int Used { get; set; }

		/// <summary>
		/// Desc:开始时间
		/// Default:
		/// Nullable:False
		/// </summary>
		public DateTime StartTime { get; set; }

		/// <summary>
		/// Desc:结束时间
		/// Default:
		/// Nullable:False
		/// </summary>
		public DateTime EndTime { get; set; }

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
