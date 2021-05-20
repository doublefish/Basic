namespace Basic.Model.Config
{
	/// <summary>
	/// 流程状态
	/// </summary>
	public class StatusOfProcess : Adai.Base.Model.Config
	{
		/// <summary>
		/// 已申请
		/// </summary>
		public const int Applied = 1;
		/// <summary>
		/// 已通过
		/// </summary>
		public const int Passed = 11;
		/// <summary>
		/// 已驳回
		/// </summary>
		public const int Rejected = 10;

		/// <summary>
		/// 构造函数
		/// </summary>
		public StatusOfProcess()
		{
			Add(Applied, "已申请");
			Add(Passed, "已通过");
			Add(Rejected, "已驳回");
		}
	}
}
