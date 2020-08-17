namespace Basic.Model.Config
{
	/// <summary>
	/// 佣金
	/// </summary>
	public class Commission
	{
		/// <summary>
		/// 状态
		/// </summary>
		public class Status : Adai.Standard.Models.Config
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
			/// 已分配
			/// </summary>
			public const int Distributed = 2;

			/// <summary>
			/// 构造函数
			/// </summary>
			public Status()
			{
				Add(Unpaid, "未支付");
				Add(Paid, "已支付");
				Add(Distributed, "已分配");
			}
		}
	}
}
