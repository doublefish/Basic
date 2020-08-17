using System.ComponentModel.DataAnnotations;

namespace WebApi.Background.Models
{
	/// <summary>
	/// 版块
	/// </summary>
	public class SectionModel
	{
		/// <summary>
		/// 父节点Id
		/// </summary>
		[Required]
		public int ParentId { get; set; }
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
		/// 序号
		/// </summary>
		[Required]
		public int Sequence { get; set; }
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