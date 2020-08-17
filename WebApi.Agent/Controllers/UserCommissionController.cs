using Adai.Standard;
using Basic.BLL;
using Basic.Model;
using Basic.Model.PageArg;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WebApi.Models;

namespace WebApi.Agent.Controllers
{
	/// <summary>
	/// 用户佣金
	/// </summary>
	[Route("api/User/Commission"), ApiExplorerSettings(GroupName = "user")]
	public class UserCommissionController : ApiController
	{
		/// <summary>
		/// 分配
		/// </summary>
		/// <param name="id">Id</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true, VerifyRight = true)]
		[HttpPut("Distribute/{id}")]
		public ReturnResult<string> Distribute(int id)
		{
			new AgentUserCommissionBLL(LoginInfo).Distribute(id);
			return Ok();
		}

		/// <summary>
		/// 分配预览
		/// </summary>
		/// <param name="id">Id</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true, VerifyRight = true)]
		[HttpPut("Distribute/Preview/{id}")]
		public ReturnResult<AgentUserCommission> DistributePreview(int id)
		{
			var result = new AgentUserCommissionBLL(LoginInfo).DistributePreview(id);
			return Json(result);
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="id">Id</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true, VerifyRight = true)]
		[HttpGet("Get/{id}")]
		public ReturnResult<AgentUserCommission> Get(int id)
		{
			var result = new AgentUserCommissionBLL(LoginInfo).Get(id);
			return Json(result);
		}

		/// <summary>
		/// 分页查询
		/// </summary>
		/// <param name="userId">用户Id</param>
		/// <param name="username">用户名</param>
		/// <param name="mobile">用户手机号码</param>
		/// <param name="orderNumber">订单编号</param>
		/// <param name="orderUsername">订单用户名</param>
		/// <param name="status">状态</param>
		/// <param name="start">创建时间.开始时间</param>
		/// <param name="end">创建时间.结束时间</param>
		/// <param name="pageNumber">页码（从0开始），默认0</param>
		/// <param name="pageSize">每页条数，默认20</param>
		/// <param name="sortName">排序字段</param>
		/// <param name="sortType">排序方式，默认0，0：降序，1：升序</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true, VerifyRight = true)]
		[HttpGet("List/{userId?}")]
		public ReturnResult<CommissionArg<AgentUserCommission>> List(int? userId = null, string username = null, string mobile = null,
			string orderNumber = null, string orderUsername = null,
			int? status = null, DateTime? start = null, DateTime? end = null,
			int? pageNumber = null, int? pageSize = null, string sortName = null, int? sortType = null)
		{
			var arg = new CommissionArg<AgentUserCommission>(pageNumber, pageSize, sortName, sortType)
			{
				AgentUserId = userId,
				AgentUsername = username,
				AgentUserMobile = mobile,
				OrderNumber = orderNumber,
				OrderUsername = orderUsername,
				Status = status,
				Start = start,
				End = end
			};
			new AgentUserCommissionBLL(LoginInfo).List(arg);
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
			var results = ConfigIntHelper<Basic.Model.Config.Commission.Status>.KeyValuePairs;
			return Json(results);
		}
	}
}
