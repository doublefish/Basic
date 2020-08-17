using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Background.Models
{
	/// <summary>
	/// 产品计划
	/// </summary>
	public class ProductDiscountModel
	{
		/// <summary>
		/// 名称
		/// </summary>
		[Required]
		public string Name { get; set; }
		/// <summary>
		/// 比例
		/// </summary>
		public decimal Rate { get; set; }
		/// <summary>
		/// 金额
		/// </summary>
		public decimal Amount { get; set; }
		/// <summary>
		/// 总数
		/// </summary>
		public int Total { get; set; }
		/// <summary>
		/// 开始时间
		/// </summary>
		public DateTime StartTime { get; set; }
		/// <summary>
		/// 结束时间
		/// </summary>
		public DateTime EndTime { get; set; }
		/// <summary>
		/// 状态
		/// </summary>
		[Required]
		public int Status { get; set; }
		/// <summary>
		/// 说明
		/// </summary>
		public string Note { get; set; }
	}
}