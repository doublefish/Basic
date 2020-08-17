using Adai.Standard;
using SqlSugar;

namespace Basic.Model
{
	/// <summary>
	/// 目的地
	/// </summary>
	public partial class Destination
	{
		/// <summary>
		/// 扩展.地区
		/// </summary>
		[SugarColumn(IsIgnore = true)]
		public Region Region { get; set; }

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
