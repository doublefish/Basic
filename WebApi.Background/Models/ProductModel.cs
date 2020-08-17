using System.ComponentModel.DataAnnotations;

namespace WebApi.Background.Models
{
	/// <summary>
	/// 产品
	/// </summary>
	public class ProductModel
	{
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
		/// 标签Id
		/// </summary>
		[Required]
		public int[] TagIds { get; set; }
		/// <summary>
		/// 主题Id
		/// </summary>
		[Required]
		public int[] ThemeIds { get; set; }
		/// <summary>
		/// 价格
		/// </summary>
		[Required]
		public decimal Price { get; set; }
		/// <summary>
		/// 住宿
		/// </summary>
		public string Stay { get; set; }
		/// <summary>
		/// 概要
		/// </summary>
		public string Overview { get; set; }
		/// <summary>
		/// 推荐人数
		/// </summary>
		public int Recommends { get; set; }
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