using System.ComponentModel.DataAnnotations;

namespace WebApi.Background.Models
{
	/// <summary>
	/// 地域
	/// </summary>
	public class RegionModel
	{
		/// <summary>
		/// 父节点Id
		/// </summary>
		[Required]
		public int ParentId { get; set; }
		/// <summary>
		/// 编码
		/// </summary>
		public string Code { get; set; }
		/// <summary>
		/// 名称
		/// </summary>
		[Required]
		public string Name { get; set; }
		/// <summary>
		/// 全称
		/// </summary>
		public string FullName { get; set; }
		/// <summary>
		/// 英文名称
		/// </summary>
		public string EnName { get; set; }
		/// <summary>
		/// 拼音
		/// </summary>
		public string Pinyin { get; set; }
		/// <summary>
		/// 地区代码
		/// </summary>
		public string AreaCode { get; set; }
		/// <summary>
		/// 邮政编码
		/// </summary>
		public string ZipCode { get; set; }
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