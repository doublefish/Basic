using Adai.Standard;
using Basic.BLL;
using Basic.Model;
using Basic.Model.PageArg;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WebApi.Background.Models;
using WebApi.Models;

namespace WebApi.Background.Controllers
{
	/// <summary>
	/// 订单
	/// </summary>
	[Route("api/Order"), ApiExplorerSettings(GroupName = "business")]
	public class OrderController : ApiController
	{
		/// <summary>
		/// 修改价格
		/// </summary>
		/// <param name="id">Id</param>
		/// <param name="model">修改内容</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true, VerifyRight = true)]
		[HttpPut("UpdatePrice/{id}")]
		public ReturnResult<string> UpdatePrice(int id, [FromBody] OrderPriceModel model)
		{
			new OrderBLL(LoginInfo).UpdatePrice(id, model.AdultPrice, model.ChildPrice, model.Note);
			return Ok();
		}

		/// <summary>
		/// 完成
		/// </summary>
		/// <param name="id">Id</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true, VerifyRight = true)]
		[HttpPut("Complete/{id}")]
		public ReturnResult<string> Complete(int id)
		{
			new OrderBLL(LoginInfo).Complete(id);
			return Ok();
		}

		/// <summary>
		/// 完成预览
		/// </summary>
		/// <param name="id">Id</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true, VerifyRight = true)]
		[HttpPut("Complete/Preview/{id}")]
		public ReturnResult<OrderCommission> CompletePreview(int id)
		{
			var result = new OrderBLL(LoginInfo).CompletePreview(id);
			return Json(result);
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="id">Id</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true, VerifyRight = true)]
		[HttpDelete("Delete/{id}")]
		public ReturnResult<string> Delete(int id)
		{
			new OrderBLL(LoginInfo).Delete(id);
			return Ok();
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="id">Id</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true, VerifyRight = true)]
		[HttpGet("Get/{id}")]
		public ReturnResult<Order> Get(int id)
		{
			var result = new OrderBLL(LoginInfo).Get(id);
			return Json(result);
		}

		/// <summary>
		/// 分页查询
		/// </summary>
		/// <param name="number">编号</param>
		/// <param name="mobile">手机号码</param>
		/// <param name="dateStart">出行日期.开始时间</param>
		/// <param name="dateEnd">出行日期.结束时间</param>
		/// <param name="accountId">用户Id</param>
		/// <param name="username">用户名</param>
		/// <param name="status">状态</param>
		/// <param name="pageNumber">页码（从0开始），默认0</param>
		/// <param name="pageSize">每页条数，默认20</param>
		/// <param name="sortName">排序字段</param>
		/// <param name="sortType">排序方式，默认0，0：降序，1：升序</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true, VerifyRight = true)]
		[HttpGet("List")]
		public ReturnResult<OrderArg<Order>> List(string number, string mobile = null,
			DateTime? dateStart = null, DateTime? dateEnd = null,
			int? accountId = null, string username = null, int? status = null,
			int? pageNumber = null, int? pageSize = null, string sortName = null, int? sortType = null)
		{
			var arg = new OrderArg<Order>(pageNumber, pageSize, sortName, sortType)
			{
				Number = number,
				Mobile = mobile,
				DateStart = dateStart,
				DateEnd = dateEnd,
				AccountId = accountId,
				Username = username,
				Status = status
			};
			new OrderBLL(LoginInfo).List(arg);
			return Json(arg);
		}

		/// <summary>
		/// 查询所有状态
		/// </summary>
		/// <returns></returns>
		[ApiAuthorize]
		[HttpGet("ListStatus")]
		public ReturnResult<IDictionary<int, string>> ListStatus()
		{
			var results = ConfigIntHelper<Basic.Model.Config.Order.Status>.KeyValuePairs;
			return Json(results);
		}
	}
}
