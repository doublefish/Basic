namespace Basic.Model.Config
{
	/// <summary>
	/// 性别
	/// </summary>
	public class Sex : Adai.Standard.Models.Config
	{
		/// <summary>
		/// 男
		/// </summary>
		public const int Male = 1;
		/// <summary>
		/// 女
		/// </summary>
		public const int Female = 2;

		/// <summary>
		/// 构造函数
		/// </summary>
		public Sex()
		{
			Add(Male, "男");
			Add(Female, "女");
		}
	}
}
