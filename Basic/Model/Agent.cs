namespace Basic.Model
{
	/// <summary>
	/// 代理商
	/// </summary>
	public partial class Agent
	{
		/// <summary>
		/// Desc:等级
		/// Default:
		/// Nullable:False
		/// </summary>
		public int Level { get; set; }

		/// <summary>
		/// Desc:账户余额
		/// Default:
		/// Nullable:False
		/// </summary>
		public decimal Balance { get; set; }

		/// <summary>
		/// Desc:冻结金额
		/// Default:
		/// Nullable:False
		/// </summary>
		public decimal Freeze { get; set; }
	}
}
