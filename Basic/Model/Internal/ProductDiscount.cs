using Adai.Standard;
using SqlSugar;
using System;

namespace Basic.Model
{
	/// <summary>
	/// 产品折扣
	/// </summary>
	public partial class ProductDiscount
	{
		/// <summary>
		/// 扩展.剩余数量
		/// </summary>
		[SugarColumn(IsIgnore = true)]
		public int Remainings => Total - Used;

		/// <summary>
		/// 扩展.剩余天数
		/// </summary>
		[SugarColumn(IsIgnore = true)]
		public int RemainingDays => EndTime > DateTime.Now ? EndTime.Subtract(DateTime.Now).Days : 0;

		/// <summary>
		/// 扩展.状态说明
		/// </summary>
		[SugarColumn(IsIgnore = true)]
		public string StatusNote => ConfigIntHelper<Config.Status>.GetValue(Status);

		/// <summary>
		/// 扩展.状态是否已启用
		/// </summary>
		[SugarColumn(IsIgnore = true)]
		public bool IsEnabled => Status == Config.Status.Enabled;

		/// <summary>
		/// 扩展.是否可用
		/// </summary>
		[SugarColumn(IsIgnore = true)]
		public bool IsAvailable => IsEnabled && Remainings > 0 && RemainingDays > 0;
	}
}
