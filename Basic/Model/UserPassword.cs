using System;

namespace Basic.Model
{
	/// <summary>
	/// 用户密码
	/// </summary>
	public partial class UserPassword
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
		public int UserId { get; set; }

		/// <summary>
		/// Desc:类别
		/// Default:
		/// Nullable:False
		/// </summary>
		public int Type { get; set; }

		/// <summary>
		/// Desc:密码
		/// Default:
		/// Nullable:False
		/// </summary>
		public string Password { get; set; }

		/// <summary>
		/// Desc:创建时间
		/// Default:
		/// Nullable:False
		/// </summary>
		public DateTime CreateTime { get; set; }
	}
}
