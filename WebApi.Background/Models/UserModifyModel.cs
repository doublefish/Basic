using System.ComponentModel.DataAnnotations;

namespace WebApi.Background.Models
{
	/// <summary>
	/// 用户修改
	/// </summary>
	public class UserModifyModel
	{
		/// <summary>
		/// 昵称
		/// </summary>
		[Required]
		public string Nickname { get; set; }
	}
}