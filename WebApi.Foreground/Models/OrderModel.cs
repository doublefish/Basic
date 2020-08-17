using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Foreground.Models
{
	/// <summary>
	/// 订单
	/// </summary>
	public class OrderModel
	{
		/// <summary>
		/// 产品Id
		/// </summary>
		[Required]
		public int ProductId { get; set; }
		/// <summary>
		/// 手机号码
		/// </summary>
		[Required]
		public string Mobile { get; set; }
		/// <summary>
		/// 出发日期
		/// </summary>
		[Required]
		public DateTime Date { get; set; }
		/// <summary>
		/// 成年人数
		/// </summary>
		[Required]
		public int Adults { get; set; }
		/// <summary>
		/// 儿童人数
		/// </summary>
		[Required]
		public int Children { get; set; }
	}
}