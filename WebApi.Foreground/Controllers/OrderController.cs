using Basic.BLL;
using Basic.Model;
using Microsoft.AspNetCore.Mvc;
using WebApi.Foreground.Models;
using WebApi.Models;

namespace WebApi.Foreground.Controllers
{
	/// <summary>
	/// 订单
	/// </summary>
	[Route("api/Order"), ApiExplorerSettings(GroupName = "business")]
	public class OrderController : ApiController
	{
		/// <summary>
		/// 新增
		/// </summary>
		/// <param name="model">新增内容</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true)]
		[HttpPost("Add")]
		public ReturnResult<int> Add([FromBody] OrderModel model)
		{
			var data = new Order()
			{
				ProductId = model.ProductId,
				Mobile = model.Mobile,
				Date = model.Date,
				Adults = model.Adults,
				Children = model.Children
			};
			new OrderBLL(LoginInfo).Add(data);
			return Json(data.Id);
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="id">Id</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true)]
		[HttpGet("Get/{id}")]
		public ReturnResult<Order> Get(int id)
		{
			var result = new OrderBLL(LoginInfo).Get(id, true);
			return Json(result);
		}
	}
}
