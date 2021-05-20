using Adai.Base;
using Basic.BLL;
using Basic.Model;
using Basic.Model.PageArg;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WebApi.Models;

namespace WebApi.Foreground.Controllers
{
	/// <summary>
	/// 用户资金记录
	/// </summary>
	[Route("api/Account/Fund"), ApiExplorerSettings(GroupName = "account")]
	public class AccountFundController : ApiController
	{
		/// <summary>
		/// 分页查询
		/// </summary>
		/// <param name="accountId">用户Id</param>
		/// <param name="username">用户名</param>
		/// <param name="mobile">用户手机号码</param>
		/// <param name="number">编号</param>
		/// <param name="type">类型</param>
		/// <param name="start">创建时间.开始时间</param>
		/// <param name="end">创建时间.结束时间</param>
		/// <param name="pageNumber">页码（从0开始），默认0</param>
		/// <param name="pageSize">每页条数，默认20</param>
		/// <param name="sortName">排序字段</param>
		/// <param name="sortType">排序方式，默认0，0：降序，1：升序</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true)]
		[HttpGet("List/{accountId?}")]
		public ReturnResult<BaseArg<AccountFund>> List(int? accountId = null, string username = null, string mobile = null,
			string number = null, int? type = null, DateTime? start = null, DateTime? end = null,
			int? pageNumber = null, int? pageSize = null, string sortName = null, int? sortType = null)
		{
			var arg = new BaseArg<AccountFund>(pageNumber, pageSize, sortName, sortType)
			{
				AccountId = accountId,
				Username = username,
				Mobile = mobile,
				Number = number,
				Type = type,
				Start = start,
				End = end
			};
			new AccountFundBLL(LoginInfo).List(arg);
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
