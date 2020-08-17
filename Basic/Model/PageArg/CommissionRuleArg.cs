namespace Basic.Model.PageArg
{
	/// <summary>
	/// 查询条件.佣金规则
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class CommissionRuleArg<T> : BaseArg<T> where T : class, new()
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="pageNumber">页码（从0开始），默认0</param>
		/// <param name="pageSize">每页条数，默认20</param>
		/// <param name="sortName">排序字段</param>
		/// <param name="sortType">排序方式，0：降序，1：升序</param>
		public CommissionRuleArg(int? pageNumber = null, int? pageSize = null, string sortName = null, int? sortType = null)
			: base(pageNumber, pageSize, sortName, sortType)
		{
		}

		/// <summary>
		/// 年份
		/// </summary>
		public int? Year { get; set; }
		/// <summary>
		/// 月份
		/// </summary>
		public int? Month { get; set; }
	}
}
