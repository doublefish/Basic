using System;

namespace Basic.Model
{
	/// <summary>
	/// 客户
	/// </summary>
	public partial class Customer
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
		/// Desc:名字
		/// Default:
		/// Nullable:False
		/// </summary>
		public string FirstName { get; set; }

		/// <summary>
		/// Desc:姓氏
		/// Default:
		/// Nullable:False
		/// </summary>
		public string LastName { get; set; }

		/// <summary>
		/// Desc:性别
		/// Default:
		/// Nullable:True
		/// </summary>
		public int? Sex { get; set; }

		/// <summary>
		/// Desc:生日
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
		/// Desc:民族
		/// Default:
		/// Nullable:True
		/// </summary>
		public int? Nation { get; set; }

		/// <summary>
		/// Desc:籍贯
		/// Default:
		/// Nullable:True
		/// </summary>
		public int? NativePlace { get; set; }

		/// <summary>
		/// Desc:政治面貌
		/// Default:
		/// Nullable:True
		/// </summary>
		public int? PoliticalStatus { get; set; }

		/// <summary>
		/// Desc:婚姻状况
		/// Default:
		/// Nullable:True
		/// </summary>
		public int? MaritalStatus { get; set; }

		/// <summary>
		/// Desc:健康状况
		/// Default:
		/// Nullable:True
		/// </summary>
		public string HealthStatus { get; set; }

		/// <summary>
		/// Desc:宗教信仰
		/// Default:
		/// Nullable:True
		/// </summary>
		public int? Faith { get; set; }

		/// <summary>
		/// Desc:教育程度
		/// Default:
		/// Nullable:True
		/// </summary>
		public int? Education { get; set; }

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
