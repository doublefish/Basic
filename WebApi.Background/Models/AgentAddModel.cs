using System.ComponentModel.DataAnnotations;

namespace WebApi.Background.Models
{
	/// <summary>
	/// 添加代理商
	/// </summary>
	public class AgentAddModel : AgentModel
	{
		/// <summary>
		/// 编码
		/// </summary>
		[Required]
		public string Username { get; set; }
	}
}