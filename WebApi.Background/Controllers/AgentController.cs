using Adai.Standard;
using Basic.BLL;
using Basic.Model;
using Basic.Model.PageArg;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WebApi.Background.Models;
using WebApi.Models;

namespace WebApi.Background.Controllers
{
	/// <summary>
	/// 代理商
	/// </summary>
	[Route("api/Agent"), ApiExplorerSettings(GroupName = "agent")]
	public class AgentController : ApiController
	{
		/// <summary>
		/// 新增
		/// </summary>
		/// <param name="model">新增内容</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true, VerifyRight = true)]
		[HttpPost("Add")]
		public ReturnResult<int> Add([FromBody] AgentAddModel model)
		{
			var data = new Agent()
			{
				Code = model.Code,
				Name = model.Name,
				Status = model.Status,
				Note = model.Note
			};
			new AgentBLL(LoginInfo).Add(data, model.Username, Basic.Model.Config.Password.Default);
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
		public ReturnResult<string> Update(int id, [FromBody] AgentModel model)
		{
			var data = new Agent()
			{
				Id = id,
				Code = model.Code,
				Name = model.Name,
				Status = model.Status,
				Note = model.Note
			};
			new AgentBLL(LoginInfo).Update(data);
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
			new AgentBLL(LoginInfo).UpdateStatus(id, status);
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
			new AgentBLL(LoginInfo).Delete(id);
			return Ok();
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="id">Id</param>
		/// <param name="username">用户名</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true)]
		[HttpGet("ExistByName/{id}")]
		public ReturnResult<bool> ExistByName(int id, string username)
		{
			var result = new AgentBLL(LoginInfo).ExistByName(id, username);
			return Json(result);
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="id">Id</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true, VerifyRight = true)]
		[HttpGet("Get/{id}")]
		public ReturnResult<Agent> Get(int id)
		{
			var result = new AgentBLL(LoginInfo).Get(id);
			return Json(result);
		}

		/// <summary>
		/// 分页查询
		/// </summary>
		/// <param name="name">名称</param>
		/// <param name="level">等级</param>
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
		public ReturnResult<AgentArg<Agent>> List(string name = null, int? level = null,
			int? status = null, DateTime? start = null, DateTime? end = null,
			int? pageNumber = null, int? pageSize = null, string sortName = null, int? sortType = null)
		{
			var arg = new AgentArg<Agent>(pageNumber, pageSize, sortName, sortType)
			{
				Name = name,
				Level = level,
				Status = status,
				Start = start,
				End = end
			};
			new AgentBLL(LoginInfo).List(arg);
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
