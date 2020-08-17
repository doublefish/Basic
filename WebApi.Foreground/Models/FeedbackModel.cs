using System.ComponentModel.DataAnnotations;

namespace WebApi.Foreground.Models
{
	/// <summary>
	/// 意见反馈
	/// </summary>
	public class FeedbackModel
	{
		/// <summary>
		/// 评分
		/// </summary>
		[Required]
		public float Score { get; set; }
		/// <summary>
		/// 内容
		/// </summary>
		[Required]
		public string Content { get; set; }
		/// <summary>
		/// 电子邮箱
		/// </summary>
		public string Email { get; set; }
		/// <summary>
		/// 手机号码
		/// </summary>
		[Required]
		public string Mobile { get; set; }
		/// <summary>
		/// 微信账号
		/// </summary>
		public string WeChat { get; set; }
	}
}