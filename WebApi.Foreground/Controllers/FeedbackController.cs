using Basic.BLL;
using Basic.Model;
using Microsoft.AspNetCore.Mvc;
using WebApi.Foreground.Models;
using WebApi.Models;

namespace WebApi.Foreground.Controllers
{
	/// <summary>
	/// 意见反馈
	/// </summary>
	[Route("api/Feedback"), ApiExplorerSettings(GroupName = "business")]
	public class FeedbackController : ApiController
	{
		/// <summary>
		/// 新增
		/// </summary>
		/// <param name="model">新增内容</param>
		/// <returns></returns>
		[ApiAuthorize]
		[HttpPost("Add")]
		public ReturnResult<int> Add([FromBody] FeedbackModel model)
		{
			var data = new Feedback()
			{
				Score = model.Score,
				Content = model.Content,
				Email = model.Email,
				Mobile = model.Mobile,
				WeChat = model.WeChat
			};
			new FeedbackBLL().Add(data);
			return Json(data.Id);
		}
	}
}
