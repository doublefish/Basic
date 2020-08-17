using Adai.Standard;
using SqlSugar;

namespace Basic.Model
{
	/// <summary>
	/// 代理商用户
	/// </summary>
	public partial class AgentUser
	{
		/// <summary>
		/// 扩展.代理商
		/// </summary>
		[SugarColumn(IsIgnore = true)]
		public Agent Agent { get; set; }

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
		/// 可用余额
		/// </summary>
		[SugarColumn(IsIgnore = true)]
		public decimal Available => Balance - Freeze;

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
