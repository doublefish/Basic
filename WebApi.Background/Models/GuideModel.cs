using System.ComponentModel.DataAnnotations;

namespace WebApi.Background.Models
{
	/// <summary>
	/// 导游
	/// </summary>
	public class GuideModel
	{
		/// <summary>
		/// 名字
		/// </summary>
		[Required]
		public string FirstName { get; set; }
		/// <summary>
		/// 姓氏
		/// </summary>
		[Required]
		public string LastName { get; set; }
		/// <summary>
		/// 性别
		/// </summary>
		[Required]
		public int Sex { get; set; }
		/// <summary>
		/// 特长
		/// </summary>
		[Required]
		public string Specialty { get; set; }
		/// <summary>
		/// 从业年限
		/// </summary>
		public string WorkingYears { get; set; }
		/// <summary>
		/// 兴趣爱好
		/// </summary>
		public string Hobby { get; set; }
		/// <summary>
		/// 照片地址
		/// </summary>
		public string Photo { get; set; }
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