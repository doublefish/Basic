using Adai.Standard;
using Basic.BLL;
using Basic.Model;
using Basic.Model.PageArg;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WebApi.Agent.Models;
using WebApi.Models;

namespace WebApi.Agent.Controllers
{
	/// <summary>
	/// 用户
	/// </summary>
	[Route("api/User"), ApiExplorerSettings(GroupName = "user")]
	public class UserController : ApiController
	{
		/// <summary>
		/// 新增
		/// </summary>
		/// <param name="agentId">代理商Id</param>
		/// <param name="model">新增内容</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true, VerifyRight = true)]
		[HttpPost("Add")]
		public ReturnResult<int> Add(int agentId, [FromBody] UserModel model)
		{
			var data = new AgentUser()
			{
				AgentId = agentId,
				Username = model.Username,
				Nickname = model.Nickname,
				Avatar = model.Avatar,
				FirstName = model.FirstName,
				LastName = model.LastName,
				Sex = model.Sex,
				Birthday = model.Birthday,
				IdType = model.IdType,
				IdNumber = model.IdNumber,
				Email = model.Email,
				Mobile = model.Mobile,
				Tel = model.Tel,
				PromoCode = model.PromoCode,
				IsAdmin = model.IsAdmin,
				Status = model.Status,
				Note = model.Note
			};
			new AgentUserBLL(LoginInfo).Add(data, Basic.Model.Config.Password.Default);
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
			var data = new AgentUser()
			{
				Id = id,
				//Username = model.Username,
				Nickname = model.Nickname,
				Avatar = model.Avatar,
				FirstName = model.FirstName,
				LastName = model.LastName,
				Sex = model.Sex,
				Birthday = model.Birthday,
				IdType = model.IdType,
				IdNumber = model.IdNumber,
				Email = model.Email,
				Mobile = model.Mobile,
				Tel = model.Tel,
				PromoCode = model.PromoCode,
				Status = model.Status,
				Note = model.Note
			};
			new AgentUserBLL(LoginInfo).Update(data);
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
			new AgentUserBLL(LoginInfo).UpdateStatus(id, status);
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
			new AgentUserBLL(LoginInfo).Delete(id);
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
			var result = new AgentUserBLL(LoginInfo).ExistByUsername(id, username);
			return Json(result);
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="id">Id</param>
		/// <param name="mobile">手机号码</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true)]
		[HttpGet("ExistByMobile/{id}/{mobile}")]
		public ReturnResult<bool> ExistByMobile(int id, string mobile)
		{
			var result = new AgentUserBLL(LoginInfo).ExistByMobile(id, mobile);
			return Json(result);
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="id">Id</param>
		/// <param name="promoCode">推广代码</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true)]
		[HttpGet("ExistByPromoCode/{id}/{promoCode}")]
		public ReturnResult<bool> ExistByPromoCode(int id, string promoCode)
		{
			var result = new AgentUserBLL(LoginInfo).ExistByPromoCode(id, promoCode);
			return Json(result);
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="id">Id</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true, VerifyRight = true)]
		[HttpGet("Get/{id}")]
		public ReturnResult<AgentUser> Get(int id)
		{
			var result = new AgentUserBLL(LoginInfo).Get(id);
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
		public ReturnResult<BaseArg<AgentUser>> List(string username = null, string fullName = null, string mobile = null,
			int? status = null, DateTime? start = null, DateTime? end = null,
			int? pageNumber = null, int? pageSize = null, string sortName = null, int? sortType = null)
		{
			var arg = new BaseArg<AgentUser>(pageNumber, pageSize, sortName, sortType)
			{
				Username = username,
				FullName = fullName,
				Mobile = mobile,
				Status = status,
				Start = start,
				End = end
			};
			new AgentUserBLL(LoginInfo).List(arg);
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
