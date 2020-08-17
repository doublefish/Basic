using System;

namespace Basic.Model
{
	/// <summary>
	/// 用户
	/// </summary>
	public partial class Account
	{
		/// <summary>
		/// Desc:ID
		/// Default:
		/// Nullable:False
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Desc:用户名
		/// Default:
		/// Nullable:False
		/// </summary>
		public string Username { get; set; }

		/// <summary>
		/// Desc:昵称
		/// Default:
		/// Nullable:True
		/// </summary>
		public string Nickname { get; set; }

		/// <summary>
		/// Desc:头像地址
		/// Default:
		/// Nullable:True
		/// </summary>
		public string Avatar { get; set; }

		/// <summary>
		/// Desc:名字
		/// Default:
		/// Nullable:True
		/// </summary>
		public string FirstName { get; set; }

		/// <summary>
		/// Desc:姓氏
		/// Default:
		/// Nullable:True
		/// </summary>
		public string LastName { get; set; }

		/// <summary>
		/// Desc:性别
		/// Default:
		/// Nullable:True
		/// </summary>
		public int? Sex { get; set; }

		/// <summary>
		/// Desc:出生日期
		/// Default:
		/// Nullable:True
		/// </summary>
		public DateTime? Birthday { get; set; }

		/// <summary>
		/// Desc:证件类型
		/// Default:
		/// Nullable:True
		/// </summary>
		public int? IdType { get; set; }

		/// <summary>
		/// Desc:证件号码
		/// Default:
		/// Nullable:True
		/// </summary>
		public string IdNumber { get; set; }

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
		/// Desc:电话号码
		/// Default:
		/// Nullable:True
		/// </summary>
		public string Tel { get; set; }

		/// <summary>
		/// Desc:账户余额
		/// Default:
		/// Nullable:True
		/// </summary>
		public decimal Balance { get; set; }

		/// <summary>
		/// Desc:冻结金额
		/// Default:
		/// Nullable:True
		/// </summary>
		public decimal Freeze { get; set; }

		/// <summary>
		/// Desc:密钥
		/// Default:
		/// Nullable:False
		/// </summary>
		[Newtonsoft.Json.JsonIgnore]
		public string SecretKey { get; set; }

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
