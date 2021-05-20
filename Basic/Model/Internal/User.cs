using Adai.Base;
using SqlSugar;
using System.Collections.Generic;

namespace Basic.Model
{
	/// <summary>
	/// 用户
	/// </summary>
	public partial class User
	{
		/// <summary>
		/// 扩展.姓名
		/// </summary>
		[SugarColumn(IsIgnore = true)]
		public string FullName => string.Format("{0}{1}", LastName, FirstName);

		/// <summary>
		/// 扩展.角色Id
		/// </summary>
		[SugarColumn(IsIgnore = true)]
		public ICollection<int> RoleIds { get; set; }

		/// <summary>
		/// 扩展.角色名称
		/// </summary>
		[SugarColumn(IsIgnore = true)]
		public ICollection<string> RoleNames { get; set; }

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
