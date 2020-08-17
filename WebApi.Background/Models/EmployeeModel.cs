using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Background.Models
{
	/// <summary>
	/// 员工
	/// </summary>
	public class EmployeeModel
	{
		/// <summary>
		/// 编码
		/// </summary>
		public string Code { get; set; }
		/// <summary>
		/// 名字
		/// </summary>
		[Required]
		public string FirstName { get; set; }
		/// <summary>
		/// 姓氏
		/// </summary>
		[Required]
		public string LastName { get; set; }
		/// <summary>
		/// 性别
		/// </summary>
		public int? Sex { get; set; }
		/// <summary>
		/// 出生日期
		/// </summary>
		public DateTime? Birthday { get; set; }
		/// <summary>
		/// 证件类型
		/// </summary>
		public int? IdType { get; set; }
		/// <summary>
		/// 证件号码
		/// </summary>
		public string IdNumber { get; set; }
		/// <summary>
		/// 民族
		/// </summary>
		public int? Nation { get; set; }
		/// <summary>
		/// 籍贯
		/// </summary>
		public int? NativePlace { get; set; }
		/// <summary>
		/// 政治面貌
		/// </summary>
		public int? PoliticalStatus { get; set; }
		/// <summary>
		/// 婚姻状况
		/// </summary>
		public int? MaritalStatus { get; set; }
		/// <summary>
		/// 健康状况
		/// </summary>
		public string HealthStatus { get; set; }
		/// <summary>
		/// 宗教信仰
		/// </summary>
		public int? Faith { get; set; }
		/// <summary>
		/// 学历
		/// </summary>
		public int? Education { get; set; }
		/// <summary>
		/// 毕业院校
		/// </summary>
		public string School { get; set; }
		/// <summary>
		/// 专业
		/// </summary>
		public string Major { get; set; }
		/// <summary>
		/// 职称
		/// </summary>
		public string JobTitle { get; set; }
		/// <summary>
		/// 语言能力
		/// </summary>
		public string LanguageSkills { get; set; }
		/// <summary>
		/// 计算机技能
		/// </summary>
		public string ComputerSkills { get; set; }
		/// <summary>
		/// 证书
		/// </summary>
		public string Certificates { get; set; }
		/// <summary>
		/// 电子邮箱
		/// </summary>
		public string Email { get; set; }
		/// <summary>
		/// 手机号码
		/// </summary>
		public string Mobile { get; set; }
		/// <summary>
		/// 电话号码
		/// </summary>
		public string Tel { get; set; }
		/// <summary>
		/// 邮政编码
		/// </summary>
		public string PostalCode { get; set; }
		/// <summary>
		/// 地址
		/// </summary>
		public string Address { get; set; }
		/// <summary>
		/// 岗位
		/// </summary>
		public int? Post { get; set; }
		/// <summary>
		/// 入职时间
		/// </summary>
		public DateTime? EntryTime { get; set; }
		/// <summary>
		/// 照片
		/// </summary>
		public string Photo { get; set; }
		/// <summary>
		/// 状态
		/// </summary>
		[Required]
		public int Status { get; set; }
		/// <summary>
		/// 说明
		/// </summary>
		public string Note { get; set; }
	}
}
