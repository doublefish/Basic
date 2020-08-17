using System.ComponentModel.DataAnnotations;

namespace WebApi.Background.Models
{
	/// <summary>
	/// 功能菜单
	/// </summary>
	public class MenuModel
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
		/// 类型
		/// </summary>
		[Required]
		public int Type { get; set; }
		/// <summary>
		/// 页面地址
		/// </summary>
		public string PageUrl { get; set; }
		/// <summary>
		/// 接口地址
		/// </summary>
		public string ApiUrl { get; set; }
		/// <summary>
		/// 图标
		/// </summary>
		public string Icon { get; set; }
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