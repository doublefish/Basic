namespace Basic.Model
{
	/// <summary>
	/// 功能菜单
	/// </summary>
	public partial class Menu
	{
		/// <summary>
		/// Desc:类型
		/// Default:
		/// Nullable:False
		/// </summary>
		public int Type { get; set; }

		/// <summary>
		/// Desc:页面地址
		/// Default:
		/// Nullable:False
		/// </summary>
		public string PageUrl { get; set; }

		/// <summary>
		/// Desc:接口地址
		/// Default:
		/// Nullable:False
		/// </summary>
		public string ApiUrl { get; set; }

		/// <summary>
		/// Desc:图标
		/// Default:
		/// Nullable:False
		/// </summary>
		public string Icon { get; set; }
	}
}
