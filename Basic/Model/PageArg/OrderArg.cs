using System;

namespace Basic.Model.PageArg
{
	/// <summary>
	/// 查询条件.订单
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class OrderArg<T> : BaseArg<T> where T : class, new()
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="pageNumber">页码（从0开始），默认0</param>
		/// <param name="pageSize">每页条数，默认20</param>
		/// <param name="sortName">排序字段</param>
		/// <param name="sortType">排序方式，0：降序，1：升序</param>
		public OrderArg(int? pageNumber = null, int? pageSize = null, string sortName = null, int? sortType = null)
			: base(pageNumber, pageSize, sortName, sortType)
		{
		}

		/// <summary>
		/// 目的地
		/// </summary>
		public string Destination { get; set; }
		/// <summary>
		/// 出发地
		/// </summary>
		public string Departure { get; set; }
		/// <summary>
		/// 出行日期.开始时间
		/// </summary>
		public DateTime? DateStart { get; set; }
		/// <summary>
		/// 出行日期.结束时间
		/// </summary>
		public DateTime? DateEnd { get; set; }
	}
}
