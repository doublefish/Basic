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
	/// 代理商用户资金记录
	/// </summary>
	[Route("api/Agent/User/Fund"), ApiExplorerSettings(GroupName = "agent")]
	public class AgentUserFundController : ApiController
	{
		/// <summary>
		/// 分页查询
		/// </summary>
		/// <param name="agentId">代理商Id</param>
		/// <param name="agentName">代理商名称</param>
		/// <param name="agentUserId">代理商用户Id</param>
		/// <param name="agentUsername">代理商用户名</param>
		/// <param name="agentUserMobile">用户手机号码</param>
		/// <param name="number">编号</param>
		/// <param name="type">类型</param>
		/// <param name="start">创建时间.开始时间</param>
		/// <param name="end">创建时间.结束时间</param>
		/// <param name="pageNumber">页码（从0开始），默认0</param>
		/// <param name="pageSize">每页条数，默认20</param>
		/// <param name="sortName">排序字段</param>
		/// <param name="sortType">排序方式，默认0，0：降序，1：升序</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true, VerifyRight = true)]
		[HttpGet("List/{agentId?}")]
		public ReturnResult<BaseArg<AgentUserFund>> List(int? agentId = null, string agentName = null,
			int? agentUserId = null, string agentUsername = null, string agentUserMobile = null,
			string number = null, int? type = null, DateTime? start = null, DateTime? end = null,
			int? pageNumber = null, int? pageSize = null, string sortName = null, int? sortType = null)
		{
			var arg = new BaseArg<AgentUserFund>(pageNumber, pageSize, sortName, sortType)
			{
				AgentId = agentId,
				AgentName = agentName,
				AgentUserId = agentUserId,
				AgentUsername = agentUsername,
				AgentUserMobile = agentUserMobile,
				Number = number,
				Type = type,
				Start = start,
				End = end
			};
			new AgentUserFundBLL(LoginInfo).List(arg);
			return Json(arg);
		}

		/// <summary>
		/// 查询所有类型
		/// </summary>
		/// <returns></returns>
		[ApiAuthorize]
		[HttpGet("ListType")]
		public ReturnResult<IDictionary<string, IDictionary<int, string>>> ListType()
		{
			var results = TreeConfigIntHelper<Basic.Model.Config.Fund.Type>.KeyValuePairs;
			return Json(results);
		}
	}
}
