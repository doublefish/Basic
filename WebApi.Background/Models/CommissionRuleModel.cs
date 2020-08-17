using System.ComponentModel.DataAnnotations;

namespace WebApi.Background.Models
{
	/// <summary>
	/// 佣金规则
	/// </summary>
	public class CommissionRuleModel
	{
		/// <summary>
		/// 年份
		/// </summary>
		[Required]
		public int Year { get; set; }
		/// <summary>
		/// 月份
		/// </summary>
		[Required]
		public int Month { get; set; }
		/// <summary>
		/// 代理商佣金比例（优先级高于佣金金额）
		/// </summary>
		[Required]
		public decimal AgentRate { get; set; }
		/// <summary>
		/// 代理商佣金金额
		/// </summary>
		[Required]
		public decimal AgentAmount { get; set; }
		/// <summary>
		/// 个人佣金比例（优先级高于佣金金额）
		/// </summary>
		[Required]
		public decimal PersonalRate { get; set; }
		/// <summary>
		/// 个人佣金金额
		/// </summary>
		[Required]
		public decimal PersonalAmount { get; set; }
		/// <summary>
		/// 说明
		/// </summary>
		public string Note { get; set; }
	}
}