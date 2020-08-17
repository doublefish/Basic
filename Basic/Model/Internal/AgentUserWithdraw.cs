using Adai.Standard;
using SqlSugar;

namespace Basic.Model
{
	/// <summary>
	/// 代理商用户提现
	/// </summary>
	public partial class AgentUserWithdraw
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
		/// 扩展.开户行名称
		/// </summary>
		[SugarColumn(IsIgnore = true)]
		public string BankName { get; set; }

		/// <summary>
		/// 扩展.状态说明
		/// </summary>
		[SugarColumn(IsIgnore = true)]
		public string StatusNote => ConfigIntHelper<Config.StatusOfProcess>.GetValue(Status);

		/// <summary>
		/// 扩展.状态是否已申请
		/// </summary>
		[SugarColumn(IsIgnore = true)]
		public bool IsApplied => Status == Config.StatusOfProcess.Applied;

		/// <summary>
		/// 扩展.状态是否已通过
		/// </summary>
		[SugarColumn(IsIgnore = true)]
		public bool IsPassed => Status == Config.StatusOfProcess.Passed;
	}
}
