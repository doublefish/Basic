using Adai.Base;
using SqlSugar;

namespace Basic.Model
{
	/// <summary>
	/// 佣金规则
	/// </summary>
	public partial class CommissionRule
	{
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
