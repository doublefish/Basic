namespace Basic.Model.Config
{
	/// <summary>
	/// 平台
	/// </summary>
	public class Platform : Adai.Standard.Models.Config
	{
		/// <summary>
		/// Web
		/// </summary>
		public const int Web = 1;
		/// <summary>
		/// Wap
		/// </summary>
		public const int Wap = 2;
		/// <summary>
		/// App
		/// </summary>
		public const int App = 3;

		/// <summary>
		/// 构造函数
		/// </summary>
		public Platform()
		{
			Add(Web, "Web");
			Add(Wap, "Wap");
			Add(App, "App");
		}
	}
}
