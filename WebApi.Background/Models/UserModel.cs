using System.ComponentModel.DataAnnotations;

namespace WebApi.Background.Models
{
	/// <summary>
	/// 用户
	/// </summary>
	public class UserModel
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
		/// 角色Id
		/// </summary>
		public int[] RoleIds { get; set; }
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