namespace WebApi.Agent.Models
{
	/// <summary>
	/// 分页查询
	/// </summary>
	public class PageModel
	{
		/// <summary>
		/// 每页条数，默认20
		/// </summary>
		public int? PageSize { get; set; }
		/// <summary>
		/// 页码（从0开始），默认0
		/// </summary>
		public int? PageNumber { get; set; }
		/// <summary>
		/// 排序字段
		/// </summary>
		public string SortName { get; set; }
		/// <summary>
		/// 排序方式，默认0，0：降序，1：升序
		/// </summary>
		public int SortType { get; set; }
	}
}