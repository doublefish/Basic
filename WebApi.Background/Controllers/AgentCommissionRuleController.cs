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
	/// 代理商佣金规则
	/// </summary>
	[Route("api/Agent/CommissionRule"), ApiExplorerSettings(GroupName = "agent")]
	public class AgentCommissionRuleController : ApiController
	{
		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="id">Id</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true, VerifyRight = true)]
		[HttpGet("Get/{id}")]
		public ReturnResult<AgentCommissionRule> Get(int id)
		{
			var result = new AgentCommissionRuleBLL(LoginInfo).Get(id);
			return Json(result);
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="agentId">agentId</param>
		/// <param name="productId">productId</param>
		/// <param name="year">year</param>
		/// <param name="month">month</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true, VerifyRight = true)]
		[HttpGet("GetByProductId/{agentId}/{productId}")]
		public ReturnResult<AgentCommissionRule> GetByProductId(int agentId, int productId, int year, int month)
		{
			var result = new AgentCommissionRuleBLL(LoginInfo).Get(agentId, productId, year, month);
			return Json(result);
		}

		/// <summary>
		/// 分页查询
		/// </summary>
		/// <param name="agentId">代理商Id</param>
		/// <param name="agentName">代理商名称</param>
		/// <param name="productId">产品Id</param>
		/// <param name="year">年份</param>
		/// <param name="month">月份</param>
		/// <param name="status">状态</param>
		/// <param name="start">创建时间.开始时间</param>
		/// <param name="end">创建时间.结束时间</param>
		/// <param name="pageNumber">页码（从0开始），默认0</param>
		/// <param name="pageSize">每页条数，默认20</param>
		/// <param name="sortName">排序字段</param>
		/// <param name="sortType">排序方式，默认0，0：降序，1：升序</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true, VerifyRight = true)]
		[HttpGet("List/{agentId?}/{productId?}")]
		public ReturnResult<CommissionRuleArg<AgentCommissionRule>> List(int? agentId = null, string agentName = null,
			int? productId = null, int? year = null, int? month = null, int? status = null, DateTime? start = null, DateTime? end = null,
			int? pageNumber = null, int? pageSize = null, string sortName = null, int? sortType = null)
		{
			var arg = new CommissionRuleArg<AgentCommissionRule>(pageNumber, pageSize, sortName, sortType)
			{
				AgentId = agentId,
				AgentName = agentName,
				ProductId = productId,
				Year = year,
				Month = month,
				Status = status,
				Start = start,
				End = end
			};
			new AgentCommissionRuleBLL(LoginInfo).List(arg);
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
