using Adai.Standard;
using SqlSugar;

namespace Basic.Model
{
	/// <summary>
	/// 用户短信
	/// </summary>
	public partial class AccountSms
	{
		/// <summary>
		/// 扩展.用户
		/// </summary>
		[SugarColumn(IsIgnore = true)]
		public Account Account { get; set; }

		/// <summary>
		/// 扩展.类型说明
		/// </summary>
		[SugarColumn(IsIgnore = true)]
		public string TypeNote => ConfigIntHelper<Config.Sms.Type>.GetValue(Type);

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
