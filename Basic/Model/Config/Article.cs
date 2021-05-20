namespace Basic.Model.Config
{
	/// <summary>
	/// 文章
	/// </summary>
	public class Article
	{
		/// <summary>
		/// 版块
		/// </summary>
		public class Section
		{
			/// <summary>
			/// 根节点
			/// </summary>
			public const string Root = "Article_Section";
			/// <summary>
			/// 关于我们
			/// </summary>
			public const string AboutUs = "AboutUs";
			/// <summary>
			/// 联系我们
			/// </summary>
			public const string ContactUs = "ContactUs";
			/// <summary>
			/// 常见问题
			/// </summary>
			public const string FAQ = "FAQ";
			/// <summary>
			/// 注册协议
			/// </summary>
			public const string Signup = "Signup";
		}

		/// <summary>
		/// 状态
		/// </summary>
		public class Status : Adai.Base.Model.Config
		{
			/// <summary>
			/// 未发布
			/// </summary>
			public const int Unreleased = 0;
			/// <summary>
			/// 已发布
			/// </summary>
			public const int Released = 1;

			/// <summary>
			/// 构造函数
			/// </summary>
			public Status()
			{
				Add(Unreleased, "未发布");
				Add(Released, "已发布");
			}
		}
	}
}
