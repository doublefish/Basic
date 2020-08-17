using Basic.BLL;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Background.Controllers
{
	/// <summary>
	/// 代理商用户密码
	/// </summary>
	[Route("api/Agent/User/Password"), ApiExplorerSettings(GroupName = "agent")]
	public class AgentUserPasswordController : ApiController
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
