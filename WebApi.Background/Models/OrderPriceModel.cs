using System.ComponentModel.DataAnnotations;

namespace WebApi.Background.Models
{
	/// <summary>
	/// 订单价格
	/// </summary>
	public class OrderPriceModel
	{
		/// <summary>
		/// 成人价格
		/// </summary>
		[Required]
		public decimal AdultPrice { get; set; }
		/// <summary>
		/// 儿童价格
		/// </summary>
		[Required]
		public decimal ChildPrice { get; set; }
		/// <summary>
		/// 说明
		/// </summary>
		public string Note { get; set; }
	}
}