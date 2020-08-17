using System.ComponentModel.DataAnnotations;

namespace WebApi.Foreground.Models
{
	/// <summary>
	/// 密码
	/// </summary>
	public class ChangePasswordModel
	{
		/// <summary>
		/// 新密码
		/// </summary>
		[Required]
		public string NewPassword { get; set; }
		/// <summary>
		/// 短信验证码
		/// </summary>
		[Required]
		public string SmsCode { get; set; }
	}
}