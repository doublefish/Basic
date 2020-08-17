using System.ComponentModel.DataAnnotations;

namespace WebApi.Foreground.Models
{
	/// <summary>
	/// 注册
	/// </summary>
	public class SignUpModel
	{
		/// <summary>
		/// 用户名
		/// </summary>
		//[Required]
		public string Username { get; set; }
		/// <summary>
		/// 昵称
		/// </summary>
		public string Nickname { get; set; }
		/// <summary>
		/// 手机号码
		/// </summary>
		[Required]
		public string Mobile { get; set; }
		/// <summary>
		/// 密码
		/// </summary>
		[Required]
		public string Password { get; set; }
		/// <summary>
		/// 短信验证码
		/// </summary>
		[Required]
		public string SmsCode { get; set; }
		/// <summary>
		/// 推广代码
		/// </summary>
		public string PromoCode { get; set; }
		/// <summary>
		/// 推广人Id
		/// </summary>
		public int? PromoterId { get; set; }
	}
}
