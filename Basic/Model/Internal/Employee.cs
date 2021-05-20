using Adai.Base;
using SqlSugar;

namespace Basic.Model
{
	/// <summary>
	/// 员工
	/// </summary>
	public partial class Employee
	{
		/// <summary>
		/// 扩展.姓名
		/// </summary>
		[SugarColumn(IsIgnore = true)]
		public string FullName => string.Format("{0}{1}", LastName, FirstName);

		/// <summary>
		/// 扩展.性别说明
		/// </summary>
		[SugarColumn(IsIgnore = true)]
		public string SexNote => Sex.HasValue ? ConfigIntHelper<Config.Sex>.GetValue(Sex.Value) : "";

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
