using SqlSugar;

namespace Basic.Model
{
	/// <summary>
	/// 代理商银行卡
	/// </summary>
	public partial class AgentBankCard
	{
		/// <summary>
		/// 扩展.代理商
		/// </summary>
		[SugarColumn(IsIgnore = true)]
		public Agent Agent { get; set; }

		/// <summary>
		/// 扩展.开户行名称
		/// </summary>
		[SugarColumn(IsIgnore = true)]
		public string BankName { get; set; }
	}
}
