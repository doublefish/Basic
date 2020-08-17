using Basic.Model;

namespace Basic.BLL
{
	/// <summary>
	/// AppSettingBLL
	/// </summary>
	public class AppSettingBLL
	{
		private static AppSetting appSetting;

		/// <summary>
		/// AppSetting
		/// </summary>
		public static AppSetting AppSetting
		{
			get
			{
				if (appSetting == null)
				{
					appSetting = new AppSetting();

					var setting = new DictBLL().Get(Model.Config.AppSetting.AppName);

					appSetting.AppName = setting?.Value;
				}
				return appSetting;
			}
		}
	}
}
