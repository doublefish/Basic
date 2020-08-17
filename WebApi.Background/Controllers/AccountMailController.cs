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
	/// 用户邮件
	/// </summary>
	[Route("api/Account/Mail"), ApiExplorerSettings(GroupName = "account")]
	public class AccountMailController : ApiController
	{
		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="id">Id</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true, VerifyRight = true)]
		[HttpGet("Get/{id}")]
		public ReturnResult<AccountMail> Get(int id)
		{
			var result = new AccountMailBLL(LoginInfo).Get(id);
			return Json(result);
		}

		/// <summary>
		/// 分页查询
		/// </summary>
		/// <param name="accountId">用户Id</param>
		/// <param name="username">用户名</param>
		/// <param name="email">电子邮箱</param>
		/// <param name="type">类型</param>
		/// <param name="status">状态</param>
		/// <param name="start">创建时间.开始时间</param>
		/// <param name="end">创建时间.结束时间</param>
		/// <param name="pageNumber">页码（从0开始），默认0</param>
		/// <param name="pageSize">每页条数，默认20</param>
		/// <param name="sortName">排序字段</param>
		/// <param name="sortType">排序方式，默认0，0：降序，1：升序</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true, VerifyRight = true)]
		[HttpGet("List/{accountId?}")]
		public ReturnResult<BaseArg<AccountMail>> List(int? accountId = null, string username = null,
			string email = null, int? type = null, int? status = null, DateTime? start = null, DateTime? end = null,
			int? pageNumber = null, int? pageSize = null, string sortName = null, int? sortType = null)
		{
			var arg = new BaseArg<AccountMail>(pageNumber, pageSize, sortName, sortType)
			{
				AccountId = accountId,
				Username = username,
				Email = email,
				Type = type,
				Status = status,
				Start = start,
				End = end
			};
			new AccountMailBLL(LoginInfo).List(arg);
			return Json(arg);
		}

		/// <summary>
		/// 查询所有类型
		/// </summary>
		/// <returns></returns>
		[ApiAuthorize]
		[HttpGet("ListType")]
		public ReturnResult<IDictionary<int, string>> ListType()
		{
			var results = ConfigIntHelper<Basic.Model.Config.Mail.Type>.KeyValuePairs;
			return Json(results);
		}

		/// <summary>
		/// 查询所有状态
		/// </summary>
		/// <returns></returns>
		[ApiAuthorize]
		[HttpGet("ListStatus")]
		public ReturnResult<IDictionary<int, string>> ListStatus()
		{
			var results = ConfigIntHelper<Basic.Model.Config.Mail.Status>.KeyValuePairs;
			return Json(results);
		}
	}
}
