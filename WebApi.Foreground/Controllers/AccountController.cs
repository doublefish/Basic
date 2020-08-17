using Basic.BLL;
using Basic.Model;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Foreground.Controllers
{
	/// <summary>
	/// 个人中心
	/// </summary>
	[Route("api/Account"), ApiExplorerSettings(GroupName = "account")]
	public class AccountController : ApiController
	{
		/// <summary>
		/// 修改昵称
		/// </summary>
		/// <param name="nickname">昵称</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true)]
		[HttpPut("UpdateNickname")]
		public ReturnResult<string> UpdateNickname([FromBody] string nickname)
		{
			new AccountBLL().UpdateNickname(LoginInfo.Id, nickname);
			return Ok();
		}

		/// <summary>
		/// 修改头像
		/// </summary>
		/// <param name="avatar">头像地址</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true)]
		[HttpPut("UpdateAvatar")]
		public ReturnResult<string> UpdateAvatar([FromBody] string avatar)
		{
			new AccountBLL().UpdateAvatar(LoginInfo.Id, avatar);
			return Ok();
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="username">用户名</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true)]
		[HttpGet("ExistByUsername/{username}")]
		public ReturnResult<bool> ExistByUsername(string username)
		{
			var result = new AccountBLL().ExistByUsername(LoginInfo.Id, username, true);
			return Json(result);
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="mobile">手机</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true)]
		[HttpGet("ExistByMobile/{mobile}")]
		public ReturnResult<bool> ExistByMobile(string mobile)
		{
			var result = new AccountBLL().ExistByMobile(LoginInfo.Id, mobile, true);
			return Json(result);
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true)]
		[HttpGet("Get")]
		public ReturnResult<Account> Get()
		{
			var result = new AccountBLL().Get(LoginInfo.Id);
			return Json(result);
		}
	}
}