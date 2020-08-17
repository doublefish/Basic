using System;

namespace Basic.Model
{
	/// <summary>
	/// 产品
	/// </summary>
	public partial class Product
	{
		/// <summary>
		/// Desc:ID
		/// Default:
		/// Nullable:False
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Desc:名称
		/// Default:
		/// Nullable:False
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Desc:类型
		/// Default:
		/// Nullable:False
		/// </summary>
		public int Type { get; set; }

		/// <summary>
		/// Desc:标签
		/// Default:
		/// Nullable:False
		/// </summary>
		public string Tags { get; set; }

		/// <summary>
		/// Desc:主题
		/// Default:
		/// Nullable:False
		/// </summary>
		public string Themes { get; set; }

		/// <summary>
		/// Desc:价格
		/// Default:
		/// Nullable:False
		/// </summary>
		public decimal Price { get; set; }

		/// <summary>
		/// Desc:概要
		/// Default:
		/// Nullable:True
		/// </summary>
		public string Overview { get; set; }

		/// <summary>
		/// Desc:特色
		/// Default:
		/// Nullable:True
		/// </summary>
		public string Feature { get; set; }

		/// <summary>
		/// Desc:预定须知
		/// Default:
		/// Nullable:True
		/// </summary>
		public string Notice { get; set; }

		/// <summary>
		/// Desc:预定说明
		/// Default:
		/// Nullable:True
		/// </summary>
		public string Book { get; set; }

		/// <summary>
		/// Desc:费用说明
		/// Default:
		/// Nullable:True
		/// </summary>
		public string Cost { get; set; }

		/// <summary>
		/// Desc:封面地址
		/// Default:
		/// Nullable:True
		/// </summary>
		public string Cover { get; set; }

		/// <summary>
		/// Desc:封面地址
		/// Default:
		/// Nullable:True
		/// </summary>
		public string Cover1 { get; set; }

		/// <summary>
		/// Desc:订单数
		/// Default:
		/// Nullable:False
		/// </summary>
		public int Orders { get; set; }

		/// <summary>
		/// Desc:推荐人数
		/// Default:
		/// Nullable:False
		/// </summary>
		public int Recommends { get; set; }

		/// <summary>
		/// Desc:点击次数
		/// Default:
		/// Nullable:False
		/// </summary>
		public int Clicks { get; set; }

		/// <summary>
		/// Desc:序号
		/// Default:
		/// Nullable:False
		/// </summary>
		public int Sequence { get; set; }

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
