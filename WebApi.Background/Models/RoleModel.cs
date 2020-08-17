using System.ComponentModel.DataAnnotations;

namespace WebApi.Background.Models
{
	/// <summary>
	/// 角色
	/// </summary>
	public class RoleModel
	{
		/// <summary>
		/// 编码
		/// </summary>
		[Required]
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
	}
}