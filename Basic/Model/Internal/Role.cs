using Adai.Standard;
using SqlSugar;
using System.Collections.Generic;

namespace Basic.Model
{
	/// <summary>
	/// 角色
	/// </summary>
	public partial class Role
	{
		/// <summary>
		/// 扩展.菜单Id
		/// </summary>
		[SugarColumn(IsIgnore = true)]
		public ICollection<int> MenuIds { get; set; }

		/// <summary>
		/// 扩展.状态说明
		/// </summary>
		[SugarColumn(IsIgnore = true)]
		public string StatusNote => ConfigIntHelper<Config.Status>.GetValue(Status);

		/// <summary>
		/// 扩展.状态是否已启用
		/// </summary>
		[SugarColumn(IsIgnore = true)]
		public bool IsEnabled => Status == Config.Status.Enabled;
	}
}
