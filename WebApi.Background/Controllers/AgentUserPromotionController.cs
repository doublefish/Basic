using Adai.Standard;
using Basic.BLL;
using Basic.Model;
using Basic.Model.PageArg;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WebApi.Models;

namespace WebApi.Background.Controllers
{
	/// <summary>
	/// 代理商用户推广
	/// </summary>
	[Route("api/Agent/User/Promotion"), ApiExplorerSettings(GroupName = "agent")]
	public class AgentUserPromotionController : ApiController
	{
		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="id">Id</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true, VerifyRight = true)]
		[HttpGet("Get/{id}")]
		public ReturnResult<AccountPromotion> Get(int id)
		{
			var result = new AccountPromotionBLL(LoginInfo).Get(id);
			return Json(result);
		}

		/// <summary>
		/// 分页查询
		/// </summary>
		/// <param name="accountId">用户Id</param>
		/// <param name="username">用户名</param>
		/// <param name="mobile">用户手机号码</param>
		/// <param name="agentId">代理商Id</param>
		/// <param name="agentName">代理商名称</param>
		/// <param name="agentUserId">代理商用户Id</param>
		/// <param name="agentUsername">代理商用户名</param>
		/// <param name="agentUserMobile">代理商用户手机号码</param>
		/// <param name="promoterId">推广人用户Id</param>
		/// <param name="promoterUsername">推广人用户名</param>
		/// <param name="promoterMobile">推广人手机号码</param>
		/// <param name="status">状态</param>
		/// <param name="start">创建时间.开始时间</param>
		/// <param name="end">创建时间.结束时间</param>
		/// <param name="pageNumber">页码（从0开始），默认0</param>
		/// <param name="pageSize">每页条数，默认20</param>
		/// <param name="sortName">排序字段</param>
		/// <param name="sortType">排序方式，默认0，0：降序，1：升序</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true, VerifyRight = true)]
		[HttpGet("List/{agentId?}")]
		public ReturnResult<PromotionArg<AccountPromotion>> List(int? accountId = null, string username = null, string mobile = null,
			int? agentId = null, string agentName = null, int? agentUserId = null, string agentUsername = null, string agentUserMobile = null,
			int? promoterId = null, string promoterUsername = null, string promoterMobile = null,
			int? status = null, DateTime? start = null, DateTime? end = null,
			int? pageNumber = null, int? pageSize = null, string sortName = null, int? sortType = null)
		{
			var arg = new PromotionArg<AccountPromotion>(pageNumber, pageSize, sortName, sortType)
			{
				ByAgent = true,
				AccountId = accountId,
				Username = username,
				Mobile = mobile,
				AgentId = agentId,
				AgentName = agentName,
				AgentUserId = agentUserId,
				AgentUsername = agentUsername,
				AgentUserMobile = agentUserMobile,
				PromoterId = promoterId,
				PromoterUsername = promoterUsername,
				PromoterMobile = promoterMobile,
				Status = status,
				Start = start,
				End = end
			};
			new AccountPromotionBLL(LoginInfo).List(arg);
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
			var results = ConfigIntHelper<Basic.Model.Config.Status>.KeyValuePairs;
			return Json(results);
		}
	}
}
