using Adai.Core;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Common.Controllers
{
	/// <summary>
	/// 图片验证码
	/// </summary>
	[Route("api/VerifyCode"), ApiExplorerSettings(GroupName = "public")]
	public class VerifyCodeController : ApiController
	{
		/// <summary>
		/// 生成图片验证码
		/// </summary>
		/// <param name="guid"></param>
		/// <returns></returns>
		[HttpGet("Generate/{guid}")]
		public void Generate(string guid)
		{
			Response.GenerateImageCode(guid);
		}

		/// <summary>
		/// 验证图片验证码
		/// </summary>
		/// <returns></returns>
		[ApiAuthorize(VerifyCode = true)]
		[HttpPost("Verify")]
		public ReturnResult<string> Verify()
		{
			Request.VerifyImageCode();
			return Ok();
		}
	}
}