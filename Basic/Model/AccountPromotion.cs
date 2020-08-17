using System;

namespace Basic.Model
{
	/// <summary>
	/// 用户推广
	/// </summary>
	public partial class AccountPromotion
	{
		/// <summary>
		/// Desc:ID
		/// Default:
		/// Nullable:False
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Desc:用户Id
		/// Default:
		/// Nullable:False
		/// </summary>
		public int AccountId { get; set; }

		/// <summary>
		/// Desc:代理商Id
		/// Default:
		/// Nullable:True
		/// </summary>
		public int? AgentId { get; set; }

		/// <summary>
		/// Desc:代理商用户Id
		/// Default:
		/// Nullable:True
		/// </summary>
		public int? AgentUserId { get; set; }

		/// <summary>
		/// Desc:推广人Id
		/// Default:
		/// Nullable:True
		/// </summary>
		public int? PromoterId { get; set; }

		/// <summary>
		/// Desc:订单数量
		/// Default:
		/// Nullable:False
		/// </summary>
		public int Orders { get; set; }

		/// <summary>
		/// Desc:订单金额
		/// Default:
		/// Nullable:False
		/// </summary>
		public decimal OrderAmount { get; set; }

		/// <summary>
		/// Desc:状态
		/// Default:
		/// Nullable:False
		/// </summary>
		public int Status { get; set; }

		/// <summary>
		/// Desc:创建时间
		/// Default:
		/// Nullable:False
		/// </summary>
		public DateTime CreateTime { get; set; }

		/// <summary>
		/// Desc:修改时间
		/// Default:
		/// Nullable:False
		/// </summary>
		public DateTime UpdateTime { get; set; }

		/// <summary>
		/// Desc:说明
		/// Default:
		/// Nullable:True
		/// </summary>
		public string Note { get; set; }
	}
}
