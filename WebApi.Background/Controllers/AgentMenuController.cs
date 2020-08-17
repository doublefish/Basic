using Adai.Standard;
using Basic.BLL;
using Basic.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApi.Background.Models;
using WebApi.Models;

namespace WebApi.Background.Controllers
{
	/// <summary>
	/// 代理商功能菜单
	/// </summary>
	[Route("api/Agent/Menu"), ApiExplorerSettings(GroupName = "agent")]
	public class AgentMenuController : ApiController
	{
		/// <summary>
		/// 新增
		/// </summary>
		/// <param name="model">新增内容</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true, VerifyRight = true)]
		[HttpPost("Add")]
		public ReturnResult<int> Add([FromBody] AgentMenuModel model)
		{
			var data = new AgentMenu()
			{
				ParentId = model.ParentId,
				Code = model.Code,
				Name = model.Name,
				Type = model.Type,
				PageUrl = model.PageUrl,
				ApiUrl = model.ApiUrl,
				Icon = model.Icon,
				Level = model.Level,
				IsAdmin = model.IsAdmin,
				Sequence = model.Sequence,
				Status = model.Status,
				Note = model.Note
			};
			new AgentMenuBLL(LoginInfo).Add(data);
			return Json(data.Id);
		}

		/// <summary>
		/// 修改
		/// </summary>
		/// <param name="id">Id</param>
		/// <param name="model">内容</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true, VerifyRight = true)]
		[HttpPut("Update/{id}")]
		public ReturnResult<string> Update(int id, [FromBody] AgentMenuModel model)
		{
			var data = new AgentMenu()
			{
				Id = id,
				ParentId = model.ParentId,
				Code = model.Code,
				Name = model.Name,
				Type = model.Type,
				PageUrl = model.PageUrl,
				ApiUrl = model.ApiUrl,
				Icon = model.Icon,
				Level = model.Level,
				IsAdmin = model.IsAdmin,
				Sequence = model.Sequence,
				Status = model.Status,
				Note = model.Note
			};
			new AgentMenuBLL(LoginInfo).Update(data);
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
			new AgentMenuBLL(LoginInfo).UpdateStatus(id, status);
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
			new AgentMenuBLL(LoginInfo).Delete(id);
			return Ok();
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="id">Id</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true, VerifyRight = true)]
		[HttpGet("Get/{id}")]
		public ReturnResult<AgentMenu> Get(int id)
		{
			var result = new AgentMenuBLL(LoginInfo).Get(id);
			return Json(result);
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="parentId">父节点Id</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true, VerifyRight = true)]
		[HttpGet("List/{parentId}")]
		public ReturnResult<ICollection<AgentMenu>> List(int parentId = 0)
		{
			var results = new AgentMenuBLL(LoginInfo).ListByParentId(parentId);
			return Json(results);
		}

		/// <summary>
		/// 查询所有类别
		/// </summary>
		/// <returns></returns>
		[ApiAuthorize]
		[HttpGet("ListType")]
		public ReturnResult<IDictionary<int, string>> ListType()
		{
			var results = ConfigIntHelper<Basic.Model.Config.Menu.Type>.KeyValuePairs;
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
			var results = ConfigIntHelper<Basic.Model.Config.Status>.KeyValuePairs;
			return Json(results);
		}

		/// <summary>
		/// 刷新缓存
		/// </summary>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true, VerifyRight = true)]
		[HttpPut("RefreshCache")]
		public ReturnResult<string> RefreshCache()
		{
			new AgentMenuBLL(LoginInfo).RefreshCache();
			return Ok();
		}
	}
}
