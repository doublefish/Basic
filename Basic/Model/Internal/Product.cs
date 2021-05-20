using Adai.Base;
using SqlSugar;
using System.Collections.Generic;

namespace Basic.Model
{
	/// <summary>
	/// 产品
	/// </summary>
	public partial class Product
	{
		/// <summary>
		/// 扩展.类型说明
		/// </summary>
		[SugarColumn(IsIgnore = true)]
		public string TypeNote => ConfigIntHelper<Config.Product.Type>.GetValue(Type);

		/// <summary>
		/// 扩展.标签Id
		/// </summary>
		[SugarColumn(IsIgnore = true)]
		public ICollection<int> TagIds { get; set; }

		/// <summary>
		/// 扩展.标签名称
		/// </summary>
		[SugarColumn(IsIgnore = true)]
		public ICollection<string> TagNames { get; set; }

		/// <summary>
		/// 扩展.主题Id
		/// </summary>
		[SugarColumn(IsIgnore = true)]
		public ICollection<int> ThemeIds { get; set; }

		/// <summary>
		/// 扩展.主题名称
		/// </summary>
		[SugarColumn(IsIgnore = true)]
		public ICollection<string> ThemeNames { get; set; }

		/// <summary>
		/// 扩展.图片
		/// </summary>
		[SugarColumn(IsIgnore = true)]
		public ICollection<string> Images { get; set; }

		/// <summary>
		/// 扩展.折扣
		/// </summary>
		[SugarColumn(IsIgnore = true)]
		public ProductDiscount Discount { get; set; }

		/// <summary>
		/// 扩展.是否有折扣
		/// </summary>
		[SugarColumn(IsIgnore = true)]
		public bool HasDiscount => Discount != null;

		/// <summary>
		/// 扩展.折扣价格
		/// </summary>
		[SugarColumn(IsIgnore = true)]
		public decimal DiscountPrice => Discount != null ? (Discount.Rate > decimal.Zero ? Price * Discount.Rate : Price - Discount.Amount) : decimal.Zero;

		/// <summary>
		/// 扩展.推荐比例
		/// </summary>
		[SugarColumn(IsIgnore = true)]
		public decimal RecommendRate => Recommends > 1000 ? decimal.One : Recommends / 1000;

		/// <summary>
		/// 扩展.状态说明
		/// </summary>
		[SugarColumn(IsIgnore = true)]
		public string StatusNote => ConfigIntHelper<Config.Status>.GetValue(Status);

		/// <summary>
		/// 扩展.状态是否已启用
		/// </summary>
		[SugarColumn(IsIgnore = true)]
		public bool IsEnabled => Status == Config.Status.Enabled;
	}
}
