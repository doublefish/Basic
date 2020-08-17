using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Background.Models
{
	/// <summary>
	/// 代理商用户
	/// </summary>
	public class AgentUserModel
	{
		/// <summary>
		/// 用户名
		/// </summary>
		[Required]
		public string Username { get; set; }
		/// <summary>
		/// 昵称
		/// </summary>
		public string Nickname { get; set; }
		/// <summary>
		/// 头像地址
		/// </summary>
		public string Avatar { get; set; }
		/// <summary>
		/// 名字
		/// </summary>
		public string FirstName { get; set; }
		/// <summary>
		/// 姓氏
		/// </summary>
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
		/// 推广码
		/// </summary>
		public string PromoCode { get; set; }
		/// <summary>
		/// 是否管理员
		/// </summary>
		[Required]
		public bool IsAdmin { get; set; }
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