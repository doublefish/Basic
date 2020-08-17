using SqlSugar;

namespace Basic.Model
{
	/// <summary>
	/// 代理商
	/// </summary>
	public partial class Agent : TreeModel<Agent>
	{
		/// <summary>
		/// 可用余额
		/// </summary>
		[SugarColumn(IsIgnore = true)]
		public decimal Available => Balance - Freeze;
	}
}
