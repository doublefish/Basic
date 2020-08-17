using System.ComponentModel.DataAnnotations;

namespace WebApi.Agent.Models
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
		/// 旧密码
		/// </summary>
		[Required]
		public string OldPassword { get; set; }
	}
}