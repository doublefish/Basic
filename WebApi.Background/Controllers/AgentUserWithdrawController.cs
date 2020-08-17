﻿using Basic.BLL;
using Basic.Model;
using Basic.Model.PageArg;
using Microsoft.AspNetCore.Mvc;
using System;
using WebApi.Models;

namespace WebApi.Background.Controllers
{
	/// <summary>
	/// 代理商用户提现记录
	/// </summary>
	[Route("api/Agent/User/Withdraw"), ApiExplorerSettings(GroupName = "agent")]
	public class AgentUserWithdrawController : ApiController
	{
		/// <summary>
		/// 分页查询
		/// </summary>
		/// <param name="agentId">代理商Id</param>
		/// <param name="agentName">代理商名称</param>
		/// <param name="agentUserId">代理商用户Id</param>
		/// <param name="agentUsername">代理商用户名</param>
		/// <param name="agentUserMobile">代理商用户手机号码</param>
		/// <param name="number">编号</param>
		/// <param name="bankId">银行Id</param>
		/// <param name="cardNumber">卡号</param>
		/// <param name="cardholder">持卡人</param>
		/// <param name="branch">支行</param>
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
		public ReturnResult<BankCardArg<AgentUserWithdraw>> List(int? agentId = null, string agentName = null,
			int? agentUserId = null, string agentUsername = null, string agentUserMobile = null,
			string number = null, int? bankId = null, string cardNumber = null, string cardholder = null, string branch = null,
			int? status = null, DateTime? start = null, DateTime? end = null,
			int? pageNumber = null, int? pageSize = null, string sortName = null, int? sortType = null)
		{
			var arg = new BankCardArg<AgentUserWithdraw>(pageNumber, pageSize, sortName, sortType)
			{
				AgentId = agentId,
				AgentName = agentName,
				AgentUserId = agentUserId,
				AgentUsername = agentUsername,
				AgentUserMobile = agentUserMobile,
				Number = number,
				BankId = bankId,
				CardNumber = cardNumber,
				Cardholder = cardholder,
				Branch = branch,
				Status = status,
				Start = start,
				End = end
			};
			new AgentUserWithdrawBLL(LoginInfo).List(arg);
			return Json(arg);
		}
	}
}
