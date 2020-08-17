namespace Basic.Model.Config
{
	/// <summary>
	/// 付款状态
	/// </summary>
	public class StatusOfPayment : Adai.Standard.Models.Config
	{
		/// <summary>
		/// 未支付
		/// </summary>
		public const int Unpaid = 0;
		/// <summary>
		/// 已支付
		/// </summary>
		public const int Paid = 1;

		/// <summary>
		/// 构造函数
		/// </summary>
		public StatusOfPayment()
		{
			Add(Unpaid, "未支付");
			Add(Paid, "已支付");
		}
	}
}
