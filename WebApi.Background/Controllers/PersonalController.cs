using Basic.BLL;
using Basic.Model;
using Microsoft.AspNetCore.Mvc;
using WebApi.Background.Models;
using WebApi.Models;

namespace WebApi.Background.Controllers
{
	/// <summary>
	/// 个人中心
	/// </summary>
	[Route("api/Personal"), ApiExplorerSettings(GroupName = "personal")]
	public class PersonalController : ApiController
	{
		/// <summary>
		/// 修改
		/// </summary>
		/// <param name="model">修改内容</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true)]
		[HttpPut("Modify")]
		public ReturnResult<string> Modify([FromBody] UserModifyModel model)
		{
			var data = new User()
			{
				Nickname = model.Nickname
			};
			new UserBLL(LoginInfo).Modify(LoginInfo.Id, data);
			return Ok();
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="username">用户名</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true)]
		[HttpGet("ExistByUsername")]
		public ReturnResult<bool> ExistByUsername(string username)
		{
			var result = new UserBLL(LoginInfo).ExistByUsername(0, username, true);
			return Json(result);
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="mobile">手机号码</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true)]
		[HttpGet("ExistByMobile")]
		public ReturnResult<bool> ExistByMobile(string mobile)
		{
			var result = new UserBLL(LoginInfo).ExistByMobile(0, mobile, true);
			return Json(result);
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true)]
		[HttpGet("Get")]
		public ReturnResult<User> Get()
		{
			var result = new UserBLL(LoginInfo).Get(LoginInfo.Id);
			return Json(result);
		}
	}
}