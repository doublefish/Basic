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
	/// 文章
	/// </summary>
	[Route("api/Article"), ApiExplorerSettings(GroupName = "public")]
	public class ArticleController : ApiController
	{
		/// <summary>
		/// 新增
		/// </summary>
		/// <param name="model">新增内容</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true, VerifyRight = true)]
		[HttpPost("Add")]
		public ReturnResult<int> Add([FromBody] ArticleModel model)
		{
			var data = new Article()
			{
				Title = model.Title,
				SectionIds = model.SectionIds,
				Summary = model.Summary,
				Author = model.Author,
				Source = model.Source,
				Content = model.Content,
				ReleaseTime = model.ReleaseTime,
				Cover = model.Cover,
				IsStick = model.IsStick,
				UserId = LoginInfo.Id,
				Status = model.Status
			};
			new ArticleBLL(LoginInfo).Add(data);
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
		public ReturnResult<string> Update(int id, [FromBody] ArticleModel model)
		{
			var data = new Article()
			{
				Id = id,
				Title = model.Title,
				SectionIds = model.SectionIds,
				Summary = model.Summary,
				Author = model.Author,
				Source = model.Source,
				Content = model.Content,
				ReleaseTime = model.ReleaseTime,
				Cover = model.Cover,
				IsStick = model.IsStick,
				UserId = LoginInfo.Id,
				Status = model.Status
			};
			new ArticleBLL(LoginInfo).Update(data);
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
			new ArticleBLL(LoginInfo).UpdateStatus(id, status);
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
			new ArticleBLL(LoginInfo).Delete(id);
			return Ok();
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="id">Id</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true, VerifyRight = true)]
		[HttpGet("Get/{id}")]
		public ReturnResult<Article> Get(int id)
		{
			var result = new ArticleBLL(LoginInfo).GetDetail(id);
			return Json(result);
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="section">版块</param>
		/// <param name="title">标题</param>
		/// <param name="author">作者</param>
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
		public ReturnResult<ArticleArg<Article>> List(int? section = null, string title = null, string author = null,
			int? status = null, DateTime? start = null, DateTime? end = null,
			int? pageNumber = null, int? pageSize = null, string sortName = null, int? sortType = null)
		{
			var arg = new ArticleArg<Article>(pageNumber, pageSize, sortName, sortType)
			{
				Section = section,
				Title = title,
				Author = author,
				Status = status,
				Start = start,
				End = end
			};
			new ArticleBLL(LoginInfo).List(arg);
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
			var results = ConfigIntHelper<Basic.Model.Config.Article.Status>.KeyValuePairs;
			return Json(results);
		}

		/// <summary>
		/// 查询所有版块
		/// </summary>
		/// <returns></returns>
		[ApiAuthorize]
		[HttpGet("All")]
		public ReturnResult<ICollection<Dict>> All()
		{
			var results = new DictBLL(LoginInfo).ListByParentCode(Basic.Model.Config.Article.Section.Root, true);
			return Json(results);
		}
	}
}
