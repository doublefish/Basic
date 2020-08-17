using System.ComponentModel.DataAnnotations;

namespace WebApi.Foreground.Models
{
	/// <summary>
	/// 银行卡
	/// </summary>
	public class BankCardModel
	{
		/// <summary>
		/// 银行Id
		/// </summary>
		[Required]
		public int BankId { get; set; }
		/// <summary>
		/// 卡号
		/// </summary>
		[Required]
		public string CardNumber { get; set; }
		/// <summary>
		/// 持卡人
		/// </summary>
		[Required]
		public string Cardholder { get; set; }
		/// <summary>
		/// 支行
		/// </summary>
		[Required]
		public string Branch { get; set; }
	}
}