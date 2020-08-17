namespace Basic.Model.PageArg
{
	/// <summary>
	/// 查询条件.银行卡
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class BankCardArg<T> : BaseArg<T> where T : class, new()
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="pageNumber">页码（从0开始），默认0</param>
		/// <param name="pageSize">每页条数，默认20</param>
		/// <param name="sortName">排序字段</param>
		/// <param name="sortType">排序方式，0：降序，1：升序</param>
		public BankCardArg(int? pageNumber = null, int? pageSize = null, string sortName = null, int? sortType = null)
			: base(pageNumber, pageSize, sortName, sortType)
		{
		}

		/// <summary>
		/// 开户行Id
		/// </summary>
		public int? BankId { get; set; }
		/// <summary>
		/// 卡号
		/// </summary>
		public string CardNumber { get; set; }
		/// <summary>
		/// 持卡人
		/// </summary>
		public string Cardholder { get; set; }
		/// <summary>
		/// 开户行支行
		/// </summary>
		public string Branch { get; set; }
	}
}
