namespace Basic.Model.PageArg
{
	/// <summary>
	/// 查询条件.用户推广
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class PromotionArg<T> : BaseArg<T> where T : class, new()
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="pageNumber">页码（从0开始），默认0</param>
		/// <param name="pageSize">每页条数，默认20</param>
		/// <param name="sortName">排序字段</param>
		/// <param name="sortType">排序方式，0：降序，1：升序</param>
		public PromotionArg(int? pageNumber = null, int? pageSize = null, string sortName = null, int? sortType = null)
			: base(pageNumber, pageSize, sortName, sortType)
		{
		}

		/// <summary>
		/// 被代理商推广的
		/// </summary>
		public bool ByAgent { get; set; }
		/// <summary>
		/// 被个人用户推广的
		/// </summary>
		public bool ByPersonal { get; set; }
		/// <summary>
		/// 推广人Id
		/// </summary>
		public int? PromoterId { get; set; }
		/// <summary>
		/// 推广人用户名
		/// </summary>
		public string PromoterUsername { get; set; }
		/// <summary>
		/// 推广人手机号码
		/// </summary>
		public string PromoterMobile { get; set; }
	}
}
