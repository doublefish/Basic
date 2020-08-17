namespace Basic.Model.Config
{
	/// <summary>
	/// 密码
	/// </summary>
	public class Password
	{
		/// <summary>
		/// 默认密码
		/// </summary>
		public const string Default = "123456";

		/// <summary>
		/// 类型
		/// </summary>
		public class Type : Adai.Standard.Models.Config
		{
			/// <summary>
			/// 登录密码
			/// </summary>
			public const int Login = 1;
			/// <summary>
			/// 支付密码
			/// </summary>
			public const int Pay = 2;

			/// <summary>
			/// 构造函数
			/// </summary>
			public Type()
			{
				Add(Login, "登录");
				Add(Pay, "支付");
			}
		}
	}
}
