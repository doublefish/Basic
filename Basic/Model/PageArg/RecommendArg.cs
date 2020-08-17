namespace Basic.Model.PageArg
{
	/// <summary>
	/// 查询条件.推荐
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class RecommendArg<T> : BaseArg<T> where T : class, new()
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="pageNumber">页码（从0开始），默认0</param>
		/// <param name="pageSize">每页条数，默认20</param>
		/// <param name="sortName">排序字段</param>
		/// <param name="sortType">排序方式，0：降序，1：升序</param>
		public RecommendArg(int? pageNumber = null, int? pageSize = null, string sortName = null, int? sortType = null)
			: base(pageNumber, pageSize, sortName, sortType)
		{
		}

		/// <summary>
		/// 标签
		/// </summary>
		public int[] Tags { get; set; }
		/// <summary>
		/// 标签名称
		/// </summary>
		public string[] TagNames { get; set; }
	}
}
