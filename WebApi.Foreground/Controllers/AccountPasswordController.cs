using Basic.BLL;
using Microsoft.AspNetCore.Mvc;
using WebApi.Foreground.Models;
using WebApi.Models;

namespace WebApi.Foreground.Controllers
{
	/// <summary>
	/// 个人密码
	/// </summary>
	[Route("api/Account/Password"), ApiExplorerSettings(GroupName = "account")]
	public class AccountPasswordController : ApiController
	{
		/// <summary>
		/// 修改登录密码
		/// </summary>
		/// <param name="model">请求数据</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true)]
		[HttpPut("Change")]
		public ReturnResult<string> Change([FromBody] ChangePasswordModel model)
		{
			new AccountPasswordBLL(LoginInfo).ChangeLogin(model.NewPassword, model.SmsCode);
			return Ok();
		}

		/// <summary>
		/// 找回登录密码
		/// </summary>
		/// <param name="model">请求数据</param>
		/// <returns></returns>
		[ApiAuthorize]
		[HttpPut("Find")]
		public ReturnResult<string> Find([FromBody] FindPasswordModel model)
		{
			new AccountPasswordBLL().FindLogin(model.Mobile, model.NewPassword, model.SmsCode);
			return Ok();
		}
	}
}