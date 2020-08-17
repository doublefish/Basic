namespace Basic.Model.PageArg
{
	/// <summary>
	/// 查询条件.文章
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class ArticleArg<T> : BaseArg<T> where T : class, new()
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="pageNumber">页码（从0开始），默认0</param>
		/// <param name="pageSize">每页条数，默认20</param>
		/// <param name="sortName">排序字段</param>
		/// <param name="sortType">排序方式，0：降序，1：升序</param>
		public ArticleArg(int? pageNumber = null, int? pageSize = null, string sortName = null, int? sortType = null)
			: base(pageNumber, pageSize, sortName, sortType)
		{
		}

		/// <summary>
		/// 标题
		/// </summary>
		public string Title { get; set; }
		/// <summary>
		/// 版块
		/// </summary>
		public int? Section { get; set; }
		/// <summary>
		/// 版块
		/// </summary>
		public int[] Sections { get; set; }
		/// <summary>
		/// 作者
		/// </summary>
		public string Author { get; set; }
	}
}
