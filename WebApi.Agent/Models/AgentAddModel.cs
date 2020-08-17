using System.ComponentModel.DataAnnotations;

namespace WebApi.Agent.Models
{
	/// <summary>
	/// 添加代理商
	/// </summary>
	public class AgentAddModel : AgentModel
	{
		/// <summary>
		/// 用户名
		/// </summary>
		[Required]
		public string Username { get; set; }
	}
}