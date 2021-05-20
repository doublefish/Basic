using Adai.Base;
using SqlSugar;

namespace Basic.Model
{
	/// <summary>
	/// 用户佣金
	/// </summary>
	public partial class AccountCommission
	{
		/// <summary>
		/// 扩展.用户
		/// </summary>
		[SugarColumn(IsIgnore = true)]
		public Account Account { get; set; }

		/// <summary>
		/// 扩展.订单编号
		/// </summary>
		[SugarColumn(IsIgnore = true)]
		public string OrderNumber { get; set; }

		/// <summary>
		/// 扩展.订单金额
		/// </summary>
		[SugarColumn(IsIgnore = true)]
		public decimal OrderAmount { get; set; }

		/// <summary>
		/// 扩展.状态说明
		/// </summary>
		[SugarColumn(IsIgnore = true)]
		public string StatusNote => ConfigIntHelper<Config.Commission.Status>.GetValue(Status);

		/// <summary>
		/// 扩展.状态是否已支付
		/// </summary>
		[SugarColumn(IsIgnore = true)]
		public bool IsPaid => Status == Config.Commission.Status.Paid;
	}
}
