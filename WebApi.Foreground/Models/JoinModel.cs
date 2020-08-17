using System.ComponentModel.DataAnnotations;

namespace WebApi.Foreground.Models
{
	/// <summary>
	/// 加盟申请
	/// </summary>
	public class JoinModel
	{
		/// <summary>
		/// 类型
		/// </summary>
		[Required]
		public int Type { get; set; }
		/// <summary>
		/// 姓名
		/// </summary>
		[Required]
		public string Name { get; set; }
		/// <summary>
		/// 手机号码
		/// </summary>
		[Required]
		public string Mobile { get; set; }
	}
}