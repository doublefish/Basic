using Basic.BLL;
using Microsoft.AspNetCore.Mvc;
using WebApi.Background.Models;
using WebApi.Models;

namespace WebApi.Background.Controllers
{
	/// <summary>
	/// 个人密码
	/// </summary>
	[Route("api/Personal/Password"), ApiExplorerSettings(GroupName = "personal")]
	public class PersonalPasswordController : ApiController
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
			new UserPasswordBLL(LoginInfo).ChangeLogin(model.NewPassword, model.OldPassword);
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
			new UserPasswordBLL(LoginInfo).FindLogin(model.Mobile, model.NewPassword, model.SmsCode);
			return Ok();
		}
	}
}