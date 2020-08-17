using System;

namespace Basic.Model
{
	/// <summary>
	/// 邮件
	/// </summary>
	public partial class Mail
	{
		/// <summary>
		/// Desc:ID
		/// Default:
		/// Nullable:False
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Desc:电子邮箱
		/// Default:
		/// Nullable:False
		/// </summary>
		public string Email { get; set; }

		/// <summary>
		/// Desc:类型
		/// Default:
		/// Nullable:False
		/// </summary>
		public int Type { get; set; }

		/// <summary>
		/// Desc:内容
		/// Default:
		/// Nullable:False
		/// </summary>
		public string Content { get; set; }

		/// <summary>
		/// Desc:验证码
		/// Default:
		/// Nullable:False
		/// </summary>
		public string CheckCode { get; set; }

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
