namespace Basic.Model
{
	/// <summary>
	/// 订单佣金
	/// </summary>
	public class OrderCommission
	{
		/// <summary>
		/// 推广记录
		/// </summary>
		public AccountPromotion AccountPromotion { get; set; }
		/// <summary>
		/// 推广用户佣金
		/// </summary>
		public AccountCommission AccountCommission { get; set; }
		/// <summary>
		/// 代理商用户
		/// </summary>
		public AgentUserCommission AgentUserCommission { get; set; }
	}
}