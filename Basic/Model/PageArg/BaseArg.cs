using Adai.Standard.Model;

namespace Basic.Model.PageArg
{
	/// <summary>
	/// 查询条件
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class BaseArg<T> : PageArg<T> where T : class, new()
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="pageNumber"></param>
		/// <param name="pageSize"></param>
		/// <param name="sortName"></param>
		/// <param name="sortType"></param>
		public BaseArg(int? pageNumber = null, int? pageSize = null, string sortName = null, int? sortType = null)
			: base(pageNumber, pageSize, sortName, (SortType)(sortType ?? 0))
		{
		}

		/// <summary>
		/// 用户Id
		/// </summary>
		public int? AccountId { get; set; }
		/// <summary>
		/// 证件类型
		/// </summary>
		public int? IdType { get; set; }
		/// <summary>
		/// 证件号码
		/// </summary>
		public string IdNumber { get; set; }
		/// <summary>
		/// 电子邮箱
		/// </summary>
		public string Email { get; set; }
		/// <summary>
		/// 手机号码
		/// </summary>
		public string Mobile { get; set; }
		/// <summary>
		/// 产品Id
		/// </summary>
		public int? ProductId { get; set; }
		/// <summary>
		/// 省份Id
		/// </summary>
		public int? ProvinceId { get; set; }
		/// <summary>
		/// 城市Id
		/// </summary>
		public int? CityId { get; set; }
		/// <summary>
		/// 县/区Id
		/// </summary>
		public int? DistrictId { get; set; }
		/// <summary>
		/// 订单Id
		/// </summary>
		public int? OrderId { get; set; }
		/// <summary>
		/// 代理商Id
		/// </summary>
		public int? AgentId { get; set; }
		/// <summary>
		/// 代理商名称
		/// </summary>
		public string AgentName { get; set; }
		/// <summary>
		/// 代理商用户Id
		/// </summary>
		public int? AgentUserId { get; set; }
		/// <summary>
		/// 代理商用户名
		/// </summary>
		public string AgentUsername { get; set; }
		/// <summary>
		/// 代理商用户手机号码
		/// </summary>
		public string AgentUserMobile { get; set; }
	}
}
