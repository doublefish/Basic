using System.ComponentModel.DataAnnotations;

namespace WebApi.Background.Models
{
	/// <summary>
	/// 目的地
	/// </summary>
	public class DestinationModel
	{
		/// <summary>
		/// 地域Id
		/// </summary>
		[Required]
		public int RegionId { get; set; }
		/// <summary>
		/// 封面
		/// </summary>
		public string Cover { get; set; }
		/// <summary>
		/// 热门指数
		/// </summary>
		[Required]
		public int PopularIndex { get; set; }
		/// <summary>
		/// 序号
		/// </summary>
		[Required]
		public int Sequence { get; set; }
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