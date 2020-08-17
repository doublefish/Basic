using System;

namespace Basic.Model
{
	/// <summary>
	/// 意见反馈
	/// </summary>
	public partial class Feedback
	{
		/// <summary>
		/// Desc:ID
		/// Default:
		/// Nullable:False
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Desc:评分
		/// Default:
		/// Nullable:False
		/// </summary>
		public float Score { get; set; }

		/// <summary>
		/// Desc:内容
		/// Default:
		/// Nullable:True
		/// </summary>
		public string Content { get; set; }

		/// <summary>
		/// Desc:电子邮箱
		/// Default:
		/// Nullable:True
		/// </summary>
		public string Email { get; set; }

		/// <summary>
		/// Desc:手机号码
		/// Default:
		/// Nullable:True
		/// </summary>
		public string Mobile { get; set; }

		/// <summary>
		/// Desc:微信账号
		/// Default:
		/// Nullable:True
		/// </summary>
		public string WeChat { get; set; }

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
