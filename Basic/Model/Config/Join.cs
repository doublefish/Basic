namespace Basic.Model.Config
{
	/// <summary>
	/// 加盟
	/// </summary>
	public class Join
	{
		/// <summary>
		/// 类型
		/// </summary>
		public class Type : Adai.Base.Model.Config
		{
			/// <summary>
			/// 机构
			/// </summary>
			public const int Org = 1;
			/// <summary>
			/// 个人
			/// </summary>
			public const int Individual = 2;

			/// <summary>
			/// 构造函数
			/// </summary>
			public Type()
			{
				Add(Org, "机构");
				Add(Individual, "个人");
			}
		}
	}
}
