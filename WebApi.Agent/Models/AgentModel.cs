using System.ComponentModel.DataAnnotations;

namespace WebApi.Agent.Models
{
	/// <summary>
	/// 子级
	/// </summary>
	public class AgentModel
	{
		/// <summary>
		/// 编码
		/// </summary>
		public string Code { get; set; }
		/// <summary>
		/// 名称
		/// </summary>
		[Required]
		public string Name { get; set; }
		/// <summary>
		/// 状态
		/// </summary>
		[Required]
		public int Status { get; set; }
		/// <summary>
		/// 说明
		/// </summary>
		public string Note { get; set; }
		/// <summary>
		/// 管理员
		/// </summary>
		public UserModel Admin { get; set; }
	}
}