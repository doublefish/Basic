using Adai.Standard;
using SqlSugar;

namespace Basic.Model
{
	/// <summary>
	/// 用户推广
	/// </summary>
	public partial class AccountPromotion
	{
		/// <summary>
		/// 扩展.用户
		/// </summary>
		[SugarColumn(IsIgnore = true)]
		public Account Account { get; set; }

		/// <summary>
		/// 扩展.推广人
		/// </summary>
		[SugarColumn(IsIgnore = true)]
		public Account Promoter { get; set; }

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
