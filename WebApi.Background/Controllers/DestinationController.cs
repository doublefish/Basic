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
	/// 目的地
	/// </summary>
	[Route("api/Destination"), ApiExplorerSettings(GroupName = "system")]
	public class DestinationController : ApiController
	{
		/// <summary>
		/// 新增
		/// </summary>
		/// <param name="model">新增内容</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true, VerifyRight = true)]
		[HttpPost("Add")]
		public ReturnResult<int> Add([FromBody] DestinationModel model)
		{
			var data = new Destination()
			{
				RegionId = model.RegionId,
				Cover = model.Cover,
				PopularIndex = model.PopularIndex,
				Sequence = model.Sequence,
				Status = model.Status,
				Note = model.Note
			};
			new DestinationBLL(LoginInfo).Add(data);
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
		public ReturnResult<string> Update(int id, [FromBody] DestinationModel model)
		{
			var data = new Destination()
			{
				Id = id,
				RegionId = model.RegionId,
				Cover = model.Cover,
				PopularIndex = model.PopularIndex,
				Sequence = model.Sequence,
				Status = model.Status,
				Note = model.Note
			};
			new DestinationBLL(LoginInfo).Update(data);
			return Ok();
		}

		/// <summary>
		/// 修改封面
		/// </summary>
		/// <param name="id">Id</param>
		/// <param name="cover">修改内容</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true, VerifyRight = true)]
		[HttpPut("UpdateCover/{id}")]
		public ReturnResult<string> UpdateCover(int id, [FromBody] string cover)
		{
			new DestinationBLL(LoginInfo).UpdateCover(id, cover);
			return Ok();
		}

		/// <summary>
		/// 修改状态
		/// </summary>
		/// <param name="id">Id</param>
		/// <param name="status">修改内容</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true, VerifyRight = true)]
		[HttpPut("UpdateStatus/{id}")]
		public ReturnResult<string> UpdateStatus(int id, [FromBody] int status)
		{
			new DestinationBLL(LoginInfo).UpdateStatus(id, status);
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
			new DestinationBLL(LoginInfo).Delete(id);
			return Ok();
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="id">Id</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true, VerifyRight = true)]
		[HttpGet("Get/{id}")]
		public ReturnResult<Destination> Get(int id)
		{
			var result = new DestinationBLL(LoginInfo).Get(id);
			return Json(result);
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="parentId">父节点Id</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true, VerifyRight = true)]
		[HttpGet("List/{parentId}")]
		public ReturnResult<ICollection<Destination>> List(int parentId = 0)
		{
			var results = new DestinationBLL(LoginInfo).ListByParentId(parentId);
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
			new DestinationBLL(LoginInfo).RefreshCache();
			return Ok();
		}
	}
}