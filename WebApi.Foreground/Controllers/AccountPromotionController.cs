using Adai.Standard;
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
	/// 个人推广记录
	/// </summary>
	[Route("api/Account/Promotion"), ApiExplorerSettings(GroupName = "account")]
	public class AccountPromotionController : ApiController
	{
		/// <summary>
		/// 分页查询
		/// </summary>
		/// <param name="username">用户名</param>
		/// <param name="mobile">手机号码</param>
		/// <param name="status">状态</param>
		/// <param name="start">创建时间.开始时间</param>
		/// <param name="end">创建时间.结束时间</param>
		/// <param name="pageNumber">页码（从0开始），默认0</param>
		/// <param name="pageSize">每页条数，默认20</param>
		/// <param name="sortName">排序字段</param>
		/// <param name="sortType">排序方式，默认0，0：降序，1：升序</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true)]
		[HttpGet("List")]
		public ReturnResult<PromotionArg<AccountPromotion>> List(string username = null, string mobile = null,
			int? status = null, DateTime? start = null, DateTime? end = null,
			int? pageNumber = null, int? pageSize = null, string sortName = null, int? sortType = null)
		{
			var arg = new PromotionArg<AccountPromotion>(pageNumber, pageSize, sortName, sortType)
			{
				Username = username,
				Mobile = mobile,
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
