using Adai.Standard;
using SqlSugar;

namespace Basic.Model
{
	/// <summary>
	/// 用户资金流水
	/// </summary>
	public partial class AccountFund
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
		public string TypeNote => TreeConfigIntHelper<Config.Fund.Type>.GetValue(Type);
	}
}
