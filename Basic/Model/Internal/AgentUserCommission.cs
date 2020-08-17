using Adai.Standard;
using SqlSugar;

namespace Basic.Model
{
	/// <summary>
	/// 代理商佣金
	/// </summary>
	public partial class AgentUserCommission
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
		/// <summary>
		/// 扩展.状态是否已分配
		/// </summary>
		[SugarColumn(IsIgnore = true)]
		public bool IsDistributed => Status == Config.Commission.Status.Distributed;
	}
}
