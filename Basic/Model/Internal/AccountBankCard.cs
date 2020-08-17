using SqlSugar;

namespace Basic.Model
{
	/// <summary>
	/// 用户银行卡
	/// </summary>
	public partial class AccountBankCard
	{
		/// <summary>
		/// 扩展.用户
		/// </summary>
		[SugarColumn(IsIgnore = true)]
		public Account Account { get; set; }

		/// <summary>
		/// 扩展.开户行名称
		/// </summary>
		[SugarColumn(IsIgnore = true)]
		public string BankName { get; set; }
	}
}
