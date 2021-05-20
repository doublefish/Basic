using Adai.Base;
using SqlSugar;

namespace Basic.Model
{
	/// <summary>
	/// 用户提现
	/// </summary>
	public partial class AccountWithdraw
	{
		/// <summary>
		/// 扩展.用户
		/// </summary>
		[SugarColumn(IsIgnore = true)]
		public Account Account { get; set; }

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
