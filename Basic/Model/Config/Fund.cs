namespace Basic.Model.Config
{
	/// <summary>
	/// 资金
	/// </summary>
	public class Fund
	{
		/// <summary>
		/// 类型
		/// </summary>
		public class Type : Adai.Standard.Models.TreeConfig
		{
			/// <summary>
			/// 收入
			/// </summary>
			public class Revenue : Adai.Standard.Models.Config
			{
				/// <summary>
				/// 充值
				/// </summary>
				public const int Recharge = 11;
				/// <summary>
				/// 佣金
				/// </summary>
				public const int Commission = 18;

				/// <summary>
				/// 构造函数
				/// </summary>
				public Revenue()
				{
					Add(Recharge, "充值");
					Add(Commission, "佣金");
				}
			}

			/// <summary>
			/// 支出
			/// </summary>
			public class Expenditure : Adai.Standard.Models.Config
			{
				/// <summary>
				/// 提现
				/// </summary>
				public const int Withdraw = 21;

				/// <summary>
				/// 构造函数
				/// </summary>
				public Expenditure()
				{
					Add(Withdraw, "提现");
				}
			}

			/// <summary>
			/// 构造函数
			/// </summary>
			public Type()
			{
				Add("收入", Adai.Standard.ConfigIntHelper<Revenue>.KeyValuePairs);
				Add("支出", Adai.Standard.ConfigIntHelper<Expenditure>.KeyValuePairs);
			}
		}
	}
}
