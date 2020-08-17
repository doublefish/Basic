using System;

namespace Basic.Model
{
	/// <summary>
	/// 目的地
	/// </summary>
	public partial class Destination
	{
		/// <summary>
		/// Desc:ID
		/// Default:
		/// Nullable:False
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Desc:地区Id
		/// Default:
		/// Nullable:False
		/// </summary>
		public int RegionId { get; set; }

		/// <summary>
		/// Desc:封面地址
		/// Default:
		/// Nullable:True
		/// </summary>
		public string Cover { get; set; }

		/// <summary>
		/// Desc:热门指数
		/// Default:
		/// Nullable:False
		/// </summary>
		public int PopularIndex { get; set; }

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
