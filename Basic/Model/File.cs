using System;

namespace Basic.Model
{
	/// <summary>
	/// 文件表
	/// </summary>
	public partial class File
	{
		/// <summary>
		/// Desc:ID
		/// Default:
		/// Nullable:False
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Desc:编码
		/// Default:
		/// Nullable:False
		/// </summary>
		public string Code { get; set; }

		/// <summary>
		/// Desc:名称
		/// Default:
		/// Nullable:False
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Desc:路径
		/// Default:
		/// Nullable:False
		/// </summary>
		public string Path { get; set; }

		/// <summary>
		/// Desc:长度
		/// Default:
		/// Nullable:False
		/// </summary>
		public long Length { get; set; }

		/// <summary>
		/// Desc:后缀名
		/// Default:
		/// Nullable:False
		/// </summary>
		public string Extension { get; set; }

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
