using Adai.Base;
using Basic.BLL;
using Basic.Model;
using Basic.Model.PageArg;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using WebApi.Background.Models;
using WebApi.Models;

namespace WebApi.Background.Controllers
{
	/// <summary>
	/// 用户
	/// </summary>
	[Route("api/User"), ApiExplorerSettings(GroupName = "system")]
	public class UserController : ApiController
	{
		/// <summary>
		/// 新增
		/// </summary>
		/// <param name="model">新增内容</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true, VerifyRight = true)]
		[HttpPost("Add")]
		public ReturnResult<int> Add([FromBody] UserModel model)
		{
			var data = new User()
			{
				Username = model.Username,
				Nickname = model.Nickname,
				Avatar = model.Avatar,
				FirstName = model.FirstName,
				LastName = model.LastName,
				Email = model.Email,
				Mobile = model.Mobile,
				Tel = model.Tel,
				RoleIds = model.RoleIds,
				Status = model.Status,
				Note = model.Note
			};
			new UserBLL(LoginInfo).Add(data, Basic.Model.Config.Password.Default);
			return Json(data.Id);
		}

		/// <summary>
		/// 修改
		/// </summary>
		/// <param name="id">Id</param>
		/// <param name="model">修改内容</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true, VerifyRight = true)]
		[HttpPut("Update/{id}")]
		public ReturnResult<string> Update(int id, [FromBody] UserModel model)
		{
			var data = new User()
			{
				Id = id,
				//Username = model.Username,
				Nickname = model.Nickname,
				Avatar = model.Avatar,
				FirstName = model.FirstName,
				LastName = model.LastName,
				Mobile = model.Mobile,
				Email = model.Email,
				Tel = model.Tel,
				RoleIds = model.RoleIds,
				Status = model.Status,
				Note = model.Note
			};
			new UserBLL(LoginInfo).Update(data);
			return Ok();
		}

		/// <summary>
		/// 修改状态
		/// </summary>
		/// <param name="id">Id</param>
		/// <param name="status">状态</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true, VerifyRight = true)]
		[HttpPut("UpdateStatus/{id}")]
		public ReturnResult<string> UpdateStatus(int id, [FromBody] int status)
		{
			new UserBLL(LoginInfo).UpdateStatus(id, status);
			return Ok();
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
			new UserBLL(LoginInfo).Delete(id);
			return Ok();
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="id">Id</param>
		/// <param name="username">用户名</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true)]
		[HttpGet("ExistByUsername/{id}/{username}")]
		public ReturnResult<bool> ExistByUsername(int id, string username)
		{
			var result = new UserBLL(LoginInfo).ExistByUsername(id, username);
			return Json(result);
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="id">Id</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true, VerifyRight = true)]
		[HttpGet("Get/{id}")]
		public ReturnResult<User> Get(int id)
		{
			var result = new UserBLL(LoginInfo).Get(id);
			return Json(result);
		}

		/// <summary>
		/// 分页查询
		/// </summary>
		/// <param name="username">用户名</param>
		/// <param name="fullName">姓名</param>
		/// <param name="mobile">手机号码</param>
		/// <param name="status">状态</param>
		/// <param name="start">创建时间.开始时间</param>
		/// <param name="end">创建时间.结束时间</param>
		/// <param name="pageNumber">页码（从0开始），默认0</param>
		/// <param name="pageSize">每页条数，默认20</param>
		/// <param name="sortName">排序字段</param>
		/// <param name="sortType">排序方式，默认0，0：降序，1：升序</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true, VerifyRight = true)]
		[HttpGet("List")]
		public ReturnResult<BaseArg<User>> List(string username = null, string fullName = null,
			string mobile = null, int? status = null, DateTime? start = null, DateTime? end = null,
			int? pageNumber = null, int? pageSize = null, string sortName = null, int? sortType = null)
		{
			var arg = new BaseArg<User>(pageNumber, pageSize, sortName, sortType)
			{
				Username = username,
				FullName = fullName,
				Mobile = mobile,
				Status = status,
				Start = start,
				End = end
			};
			new UserBLL(LoginInfo).List(arg);
			return Json(arg);
		}

		/// <summary>
		/// 导出Excel
		/// </summary>
		/// <param name="username">用户名</param>
		/// <param name="fullName">姓名</param>
		/// <param name="mobile">手机号码</param>
		/// <param name="status">状态</param>
		/// <param name="start">创建时间.开始时间</param>
		/// <param name="end">创建时间.结束时间</param>
		/// <param name="pageNumber">页码（从0开始），默认0</param>
		/// <param name="pageSize">每页条数，默认20</param>
		/// <param name="sortName">排序字段</param>
		/// <param name="sortType">排序方式，默认0，0：降序，1：升序</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true, VerifyRight = true, VerifyExcelExport = true)]
		[HttpGet("Excel")]
		public HttpResponseMessage Excel(string username = null, string fullName = null,
			string mobile = null, int? status = null, DateTime? start = null, DateTime? end = null,
			int? pageNumber = null, int? pageSize = null, string sortName = null, int? sortType = null)
		{
			var arg = new BaseArg<User>(pageNumber, pageSize, sortName, sortType)
			{
				Username = username,
				FullName = fullName,
				Mobile = mobile,
				Status = status,
				Start = start,
				End = end
			};
			new UserBLL(LoginInfo).List(arg);
			//return Request.OutputExcel(arg.Results);
			return default;
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
