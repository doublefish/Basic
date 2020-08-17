using Adai.Standard;
using SqlSugar;

namespace Basic.Model
{
	/// <summary>
	/// 代理商佣金规则
	/// </summary>
	public partial class AgentCommissionRule
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
