using System.ComponentModel.DataAnnotations;

namespace WebApi.Background.Models
{
	/// <summary>
	/// 找回密码
	/// </summary>
	public class FindPasswordModel
	{
		/// <summary>
		/// 绑定手机
		/// </summary>
		[Required]
		public string Mobile { get; set; }
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