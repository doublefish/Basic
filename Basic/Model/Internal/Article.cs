using Adai.Standard;
using SqlSugar;
using System.Collections.Generic;

namespace Basic.Model
{
	/// <summary>
	/// 文章
	/// </summary>
	public partial class Article
	{
		/// <summary>
		/// 扩展.版块Id
		/// </summary>
		[SugarColumn(IsIgnore = true)]
		public ICollection<int> SectionIds { get; set; }

		/// <summary>
		/// 扩展.版块名称
		/// </summary>
		[SugarColumn(IsIgnore = true)]
		public ICollection<string> SectionNames { get; set; }

		/// <summary>
		/// 扩展.状态说明
		/// </summary>
		[SugarColumn(IsIgnore = true)]
		public string StatusNote => ConfigIntHelper<Config.Article.Status>.GetValue(Status);

		/// <summary>
		/// 扩展.状态是否已启用
		/// </summary>
		[SugarColumn(IsIgnore = true)]
		public bool IsEnabled => Status == Config.Status.Enabled;

		/// <summary>
		/// 扩展.状态是否已发布
		/// </summary>
		[SugarColumn(IsIgnore = true)]
		public bool IsReleased => Status == Config.Article.Status.Released;
	}
}
