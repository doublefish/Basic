using Adai.Base;
using SqlSugar;

namespace Basic.Model
{
	/// <summary>
	/// 邮件
	/// </summary>
	public partial class Mail
	{
		/// <summary>
		/// 扩展.类型说明
		/// </summary>
		[SugarColumn(IsIgnore = true)]
		public string TypeNote => ConfigIntHelper<Config.Mail.Type>.GetValue(Type);

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
