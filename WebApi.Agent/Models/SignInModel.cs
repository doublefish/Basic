using System.ComponentModel.DataAnnotations;

namespace WebApi.Agent.Models
{
	/// <summary>
	/// 登录
	/// </summary>
	public class SignInModel
	{
		/// <summary>
		/// 用户名
		/// </summary>
		[Required, MaxLength(20), MinLength(6)]
		public string Username { get; set; }
		/// <summary>
		/// 密码
		/// </summary>
		[Required, MaxLength(20), MinLength(6)]
		public string Password { get; set; }
	}
}
