using Basic.BLL;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Background.Controllers
{
	/// <summary>
	/// 用户密码
	/// </summary>
	[Route("api/User/Password"), ApiExplorerSettings(GroupName = "system")]
	public class UserPasswordController : ApiController
	{
		/// <summary>
		/// 重置登录密码
		/// </summary>
		/// <param name="userId">用户Id</param>
		/// <param name="newPassword">密码</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true, VerifyRight = true)]
		[HttpPut("Reset/{userId}")]
		public ReturnResult<string> Reset(int userId, [FromBody] string newPassword)
		{
			new UserPasswordBLL(LoginInfo).ResetLogin(userId, newPassword);
			return Ok();
		}
	}
}
