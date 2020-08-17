namespace WebApi.Background
{
	/// <summary>
	/// ApiAuthorizeAttribute
	/// </summary>
	public class ApiAuthorizeAttribute : WebApi.ApiAuthorizeAttribute
	{
		/// <summary>
		/// 验证权限
		/// </summary>
		/// <param name="userId"></param>
		/// <param name="path"></param>
		/// <returns></returns>
		protected override bool VerifyRequestRight(int userId, string path)
		{
			return new Basic.BLL.UserBLL().VerifyRight(userId, path);
		}
	}
}