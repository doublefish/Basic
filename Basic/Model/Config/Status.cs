namespace Basic.Model.Config
{
	/// <summary>
	/// 状态
	/// </summary>
	public class Status : Adai.Base.Model.Config
	{
		/// <summary>
		/// 禁用/审核中/产品未上架
		/// </summary>
		public const int Disabled = 0;
		/// <summary>
		/// 启用/已发布/产品已上架
		/// </summary>
		public const int Enabled = 1;

		/// <summary>
		/// 构造函数
		/// </summary>
		public Status()
		{
			Add(Disabled, "禁用");
			Add(Enabled, "启用");
		}
	}
}
