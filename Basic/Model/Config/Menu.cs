namespace Basic.Model.Config
{
	/// <summary>
	/// 功能菜单
	/// </summary>
	public class Menu
	{
		/// <summary>
		/// 根节点
		/// </summary>
		public const int Root = 0;

		/// <summary>
		/// 类型
		/// </summary>
		public class Type : Adai.Standard.Models.Config
		{
			/// <summary>
			/// 系统
			/// </summary>
			public const int System = 1;
			/// <summary>
			/// 目录
			/// </summary>
			public const int Directory = 2;
			/// <summary>
			/// 页面
			/// </summary>
			public const int Page = 3;
			/// <summary>
			/// 功能
			/// </summary>
			public const int Function = 4;

			/// <summary>
			/// 构造函数
			/// </summary>
			public Type()
			{
				Add(System, "系统");
				Add(Directory, "目录");
				Add(Page, "页面");
				Add(Function, "功能");
			}
		}
	}
}
