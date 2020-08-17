using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Background.Models
{
	/// <summary>
	/// 文章
	/// </summary>
	public class ArticleModel
	{
		/// <summary>
		/// 标题
		/// </summary>
		[Required]
		public string Title { get; set; }
		/// <summary>
		/// 版块Id
		/// </summary>
		[Required]
		public int[] SectionIds { get; set; }
		/// <summary>
		/// 摘要
		/// </summary>
		public string Summary { get; set; }
		/// <summary>
		/// 作者
		/// </summary>
		[Required]
		public string Author { get; set; }
		/// <summary>
		/// 来源
		/// </summary>
		public string Source { get; set; }
		/// <summary>
		/// 内容
		/// </summary>
		public string Content { get; set; }
		/// <summary>
		/// 封面
		/// </summary>
		public string Cover { get; set; }
		/// <summary>
		/// 发布时间
		/// </summary>
		[Required]
		public DateTime ReleaseTime { get; set; }
		/// <summary>
		/// 是否置顶
		/// </summary>
		[Required]
		public bool IsStick { get; set; }
		/// <summary>
		/// 状态
		/// </summary>
		[Required]
		public int Status { get; set; }
	}
}