using Basic.BLL;
using Basic.Model;
using Basic.Model.PageArg;
using Microsoft.AspNetCore.Mvc;
using System;
using WebApi.Models;

namespace WebApi.Agent.Controllers
{
	/// <summary>
	/// 用户提现
	/// </summary>
	[Route("api/User/Withdraw"), ApiExplorerSettings(GroupName = "user")]
	public class UserWithdrawController : ApiController
	{
		/// <summary>
		/// 分页查询
		/// </summary>
		/// <param name="userId">用户Id</param>
		/// <param name="username">用户名</param>
		/// <param name="mobile">手机号码</param>
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
		[HttpGet("List/{userId?}")]
		public ReturnResult<BankCardArg<AgentUserWithdraw>> List(int? userId = null, string username = null, string mobile = null,
			string number = null, int? bankId = null, string cardNumber = null, string cardholder = null, string branch = null,
			int? status = null, DateTime? start = null, DateTime? end = null,
			int? pageNumber = null, int? pageSize = null, string sortName = null, int? sortType = null)
		{
			var arg = new BankCardArg<AgentUserWithdraw>(pageNumber, pageSize, sortName, sortType)
			{
				AgentUserId = userId,
				AgentUsername = username,
				AgentUserMobile = mobile,
				Number = number,
				BankId = bankId,
				CardNumber = cardNumber,
				Cardholder = cardholder,
				Branch = branch,
				Status = status,
				Start = start,
				End = end
			};
			new AgentUserWithdrawBLL().List(arg);
			return Json(arg);
		}
	}
}
