using Adai.Standard;
using Adai.Standard.Ext;
using Basic.BLL;
using Basic.Model;
using Basic.Model.PageArg;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Background.Models;
using WebApi.Models;

namespace WebApi.Background.Controllers
{
	/// <summary>
	/// 佣金规则
	/// </summary>
	[Route("api/CommissionRule"), ApiExplorerSettings(GroupName = "business")]
	public class CommissionRuleController : ApiController
	{
		/// <summary>
		/// 新增
		/// </summary>
		/// <param name="productIds">产品Id</param>
		/// <param name="model">新增内容</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true, VerifyRight = true)]
		[HttpPost("Add/{productIds}")]
		public ReturnResult<int[]> Add(string productIds, [FromBody] CommissionRuleModel model)
		{
			var datas = new List<CommissionRule>();
			var _productIds = productIds.Split(',');
			foreach (var productId in _productIds)
			{
				datas.Add(new CommissionRule()
				{
					ProductId = productId.ToInt32(),
					Year = model.Year,
					Month = model.Month,
					AgentRate = model.AgentRate,
					AgentAmount = model.AgentAmount,
					PersonalRate = model.PersonalRate,
					PersonalAmount = model.PersonalAmount,
					Note = model.Note
				});
			}
			new CommissionRuleBLL(LoginInfo).Add(datas);
			return Json(datas.Select(o => o.Id).ToArray());
		}

		/// <summary>
		/// 修改
		/// </summary>
		/// <param name="id">Id</param>
		/// <param name="model">修改内容</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true, VerifyRight = true)]
		[HttpPut("Update/{id}")]
		public ReturnResult<string> Update(int id, [FromBody] CommissionRuleModel model)
		{
			var data = new CommissionRule()
			{
				Id = id,
				Year = model.Year,
				Month = model.Month,
				AgentRate = model.AgentRate,
				AgentAmount = model.AgentAmount,
				PersonalRate = model.PersonalRate,
				PersonalAmount = model.PersonalAmount,
				Note = model.Note
			};
			new CommissionRuleBLL(LoginInfo).Update(data);
			return Ok();
		}

		/// <summary>
		/// 启用（不可逆）
		/// </summary>
		/// <param name="id">Id</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true, VerifyRight = true)]
		[HttpPut("Enable/{id}")]
		public ReturnResult<string> Enable(int id)
		{
			new CommissionRuleBLL(LoginInfo).Enable(id);
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
			new CommissionRuleBLL(LoginInfo).Delete(id);
			return Ok();
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="id">Id</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true, VerifyRight = true)]
		[HttpGet("Get/{id}")]
		public ReturnResult<CommissionRule> Get(int id)
		{
			var result = new CommissionRuleBLL(LoginInfo).Get(id);
			return Json(result);
		}

		/// <summary>
		/// 查询最新
		/// </summary>
		/// <param name="productId">productId</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true, VerifyRight = true)]
		[HttpGet("GetLastest/{productId}")]
		public ReturnResult<CommissionRule> GetLastest(int productId)
		{
			var result = new CommissionRuleBLL(LoginInfo).GetLastest(productId);
			return Json(result);
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="productId">productId</param>
		/// <param name="year">year</param>
		/// <param name="month">month</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true, VerifyRight = true)]
		[HttpGet("GetByProductId/{productId}")]
		public ReturnResult<CommissionRule> GetByProductId(int productId, int year, int month)
		{
			var result = new CommissionRuleBLL(LoginInfo).Get(productId, year, month);
			return Json(result);
		}

		/// <summary>
		/// 分页查询
		/// </summary>
		/// <param name="productId">产品Id</param>
		/// <param name="year">年份</param>
		/// <param name="month">月份</param>
		/// <param name="status">状态</param>
		/// <param name="start">创建时间.开始时间</param>
		/// <param name="end">创建时间.结束时间</param>
		/// <param name="pageNumber">页码（从0开始），默认0</param>
		/// <param name="pageSize">每页条数，默认20</param>
		/// <param name="sortName">排序字段</param>
		/// <param name="sortType">排序方式，默认0，0：降序，1：升序</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true, VerifyRight = true)]
		[HttpGet("List/{productId}")]
		public ReturnResult<CommissionRuleArg<CommissionRule>> List(int productId,
			int? year = null, int? month = null, int? status = null, DateTime? start = null, DateTime? end = null,
			int? pageNumber = null, int? pageSize = null, string sortName = null, int? sortType = null)
		{
			var arg = new CommissionRuleArg<CommissionRule>(pageNumber, pageSize, sortName, sortType)
			{
				ProductId = productId,
				Year = year,
				Month = month,
				Status = status,
				Start = start,
				End = end
			};
			new CommissionRuleBLL(LoginInfo).List(arg);
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
