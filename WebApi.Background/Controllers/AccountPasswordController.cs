using Basic.BLL;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Background.Controllers
{
	/// <summary>
	/// 用户密码
	/// </summary>
	[Route("api/Account/Password"), ApiExplorerSettings(GroupName = "account")]
	public class AccountPasswordController : ApiController
	{
		/// <summary>
		/// 重置登录密码
		/// </summary>
		/// <param name="accountId">用户Id</param>
		/// <param name="newPassword">新密码</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true, VerifyRight = true)]
		[HttpPut("Reset/{accountId}")]
		public ReturnResult<string> Reset(int accountId, [FromBody] string newPassword)
		{
			new AccountPasswordBLL(LoginInfo).ResetLogin(accountId, newPassword);
			return Ok();
		}
	}
}
