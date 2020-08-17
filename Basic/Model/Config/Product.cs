namespace Basic.Model.Config
{
	/// <summary>
	/// 产品
	/// </summary>
	public class Product
	{
		/// <summary>
		/// 类型
		/// </summary>
		public class Type : Adai.Standard.Models.Config
		{
			/// <summary>
			/// 跟团游
			/// </summary>
			public const int Group = 1;
			/// <summary>
			/// 自由行
			/// </summary>
			public const int Freedom = 2;
			/// <summary>
			/// 其他
			/// </summary>
			public const int Other = 9;

			/// <summary>
			/// 构造函数
			/// </summary>
			public Type()
			{
				Add(Group, "跟团游");
				Add(Freedom, "自由行");
				Add(Other, "其他");
			}
		}

		/// <summary>
		/// 标签
		/// </summary>
		public class Tag
		{
			/// <summary>
			/// 根节点
			/// </summary>
			public const string Root = "Product_Tag";
		}

		/// <summary>
		/// 线路
		/// </summary>
		public class Route
		{
			/// <summary>
			/// 类型
			/// </summary>
			public class Type : Adai.Standard.Models.Config
			{
				/// <summary>
				/// 交通
				/// </summary>
				public const int Traffic = 1;
				/// <summary>
				/// 三餐
				/// </summary>
				public const int Meal = 2;
				/// <summary>
				/// 活动
				/// </summary>
				public const int Activity = 3;
				/// <summary>
				/// 住宿
				/// </summary>
				public const int Stay = 4;
				/// <summary>
				/// 提示
				/// </summary>
				public const int Tip = 9;

				/// <summary>
				/// 构造函数
				/// </summary>
				public Type()
				{
					Add(Traffic, "交通");
					Add(Meal, "三餐");
					Add(Activity, "活动");
					Add(Stay, "住宿");
					Add(Tip, "提示");
				}
			}
		}
	}
}
