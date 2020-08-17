using System;

namespace Basic.Model
{
	/// <summary>
	/// 代理商用户密码
	/// </summary>
	public partial class AgentUserPassword
	{
		/// <summary>
		/// Desc:ID
		/// Default:
		/// Nullable:False
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Desc:代理商用户Id
		/// Default:
		/// Nullable:False
		/// </summary>
		public int AgentUserId { get; set; }

		/// <summary>
		/// Desc:类型：1-登录密码
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
