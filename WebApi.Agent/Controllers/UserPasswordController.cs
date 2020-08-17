using Basic.BLL;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Agent.Controllers
{
	/// <summary>
	/// 用户密码
	/// </summary>
	[Route("api/User/Password"), ApiExplorerSettings(GroupName = "user")]
	public class UserPasswordController : ApiController
	{
		/// <summary>
		/// 重置登录密码
		/// </summary>
		/// <param name="userId">用户Id</param>
		/// <param name="newPassword">新密码</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true, VerifyRight = true)]
		[HttpPut("Reset/{userId}")]
		public ReturnResult<string> Reset(int userId, [FromBody] string newPassword)
		{
			new AgentUserPasswordBLL(LoginInfo).ResetLogin(userId, newPassword);
			return Ok();
		}
	}
}
