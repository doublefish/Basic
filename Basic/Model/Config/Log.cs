namespace Basic.Model.Config
{
	/// <summary>
	/// 日志
	/// </summary>
	public class Log
	{
		/// <summary>
		/// 类别
		/// </summary>
		public class Category
		{
			/// <summary>
			/// 信息
			/// </summary>
			public const string Information = "Information";
			/// <summary>
			/// 异常
			/// </summary>
			public const string Exception = "Exception";
			/// <summary>
			/// 接口异常
			/// </summary>
			public const string ApiException = "ApiException";
			/// <summary>
			/// 自定义异常
			/// </summary>
			public const string CustomException = "CustomException";
		}
	}
}
