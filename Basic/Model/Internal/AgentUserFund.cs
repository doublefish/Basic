using Adai.Standard;
using SqlSugar;

namespace Basic.Model
{
	/// <summary>
	/// 代理商用户资金流水
	/// </summary>
	public partial class AgentUserFund
	{
		/// <summary>
		/// 扩展.代理商
		/// </summary>
		[SugarColumn(IsIgnore = true)]
		public Agent Agent { get; set; }

		/// <summary>
		/// 扩展.代理商用户
		/// </summary>
		[SugarColumn(IsIgnore = true)]
		public AgentUser AgentUser { get; set; }

		/// <summary>
		/// 扩展.类型说明
		/// </summary>
		[SugarColumn(IsIgnore = true)]
		public string TypeNote => TreeConfigIntHelper<Config.Fund.Type>.GetValue(Type);
	}
}
