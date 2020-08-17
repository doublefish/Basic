namespace Basic.Model
{
	/// <summary>
	/// 功能菜单
	/// </summary>
	public partial class AgentMenu
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

		/// <summary>
		/// Desc:等级
		/// Default:
		/// Nullable:False
		/// </summary>
		public int Level { get; set; }

		/// <summary>
		/// Desc:是否管理员专用
		/// Default:
		/// Nullable:False
		/// </summary>
		public bool IsAdmin { get; set; }
	}
}
