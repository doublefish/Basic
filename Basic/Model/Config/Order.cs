namespace Basic.Model.Config
{
	/// <summary>
	/// 订单
	/// </summary>
	public class Order
	{
		/// <summary>
		/// 状态
		/// </summary>
		public class Status : Adai.Base.Model.Config
		{
			/// <summary>
			/// 待提交
			/// </summary>
			public const int Pending = 0;
			/// <summary>
			/// 已提交
			/// </summary>
			public const int Submitted = 1;
			/// <summary>
			/// 已完成
			/// </summary>
			public const int Completed = 2;

			/// <summary>
			/// 构造函数
			/// </summary>
			public Status()
			{
				Add(Pending, "待提交");
				Add(Submitted, "已提交");
				Add(Completed, "已完成");
			}
		}
	}
}
