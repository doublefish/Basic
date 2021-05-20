using Adai.Base;
using SqlSugar;

namespace Basic.Model
{
	/// <summary>
	/// 订单
	/// </summary>
	public partial class Order
	{
		/// <summary>
		/// 扩展.产品
		/// </summary>
		[SugarColumn(IsIgnore = true)]
		public Product Product { get; set; }

		/// <summary>
		/// 扩展.用户
		/// </summary>
		[SugarColumn(IsIgnore = true)]
		public Account Account { get; set; }

		/// <summary>
		/// 扩展.状态说明
		/// </summary>
		[SugarColumn(IsIgnore = true)]
		public string StatusNote => ConfigIntHelper<Config.Order.Status>.GetValue(Status);

		/// <summary>
		/// 扩展.状态是否已提交
		/// </summary>
		[SugarColumn(IsIgnore = true)]
		public bool IsSubmitted => Status == Config.Order.Status.Submitted;

		/// <summary>
		/// 扩展.状态是否已完成
		/// </summary>
		[SugarColumn(IsIgnore = true)]
		public bool IsCompleted => Status == Config.Order.Status.Completed;
	}
}
