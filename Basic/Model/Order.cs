using System;

namespace Basic.Model
{
	/// <summary>
	/// 订单
	/// </summary>
	public partial class Order
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
		/// Desc:编号
		/// Default:
		/// Nullable:False
		/// </summary>
		public string Number { get; set; }

		/// <summary>
		/// Desc:手机号码
		/// Default:
		/// Nullable:False
		/// </summary>
		public string Mobile { get; set; }

		/// <summary>
		/// Desc:出发日期
		/// Default:
		/// Nullable:False
		/// </summary>
		public DateTime Date { get; set; }

		/// <summary>
		/// Desc:成年人数
		/// Default:
		/// Nullable:False
		/// </summary>
		public int Adults { get; set; }

		/// <summary>
		/// Desc:儿童人数
		/// Default:
		/// Nullable:False
		/// </summary>
		public int Children { get; set; }

		/// <summary>
		/// Desc:用户Id
		/// Default:
		/// Nullable:False
		/// </summary>
		public int AccountId { get; set; }

		/// <summary>
		/// Desc:原始价格
		/// Default:
		/// Nullable:False
		/// </summary>
		public decimal OriginalPrice { get; set; }

		/// <summary>
		/// Desc:折扣Id
		/// Default:
		/// Nullable:True
		/// </summary>
		public int? DiscountId { get; set; }

		/// <summary>
		/// Desc:折扣金额
		/// Default:
		/// Nullable:True
		/// </summary>
		public decimal? DiscountPrice { get; set; }

		/// <summary>
		/// Desc:折扣信息
		/// Default:
		/// Nullable:True
		/// </summary>
		public string DiscountInfo { get; set; }

		/// <summary>
		/// Desc:成人价格
		/// Default:
		/// Nullable:True
		/// </summary>
		public decimal? AdultPrice { get; set; }

		/// <summary>
		/// Desc:儿童价格
		/// Default:
		/// Nullable:True
		/// </summary>
		public decimal? ChildPrice { get; set; }

		/// <summary>
		/// Desc:成交总价
		/// Default:
		/// Nullable:True
		/// </summary>
		public decimal? TotalPrice { get; set; }

		/// <summary>
		/// Desc:操作用户Id
		/// Default:
		/// Nullable:True
		/// </summary>
		public int? UserId { get; set; }

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
