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
	/// 产品折扣
	/// </summary>
	[Route("api/Product/Discount"), ApiExplorerSettings(GroupName = "business")]
	public class ProductDiscountController : ApiController
	{
		/// <summary>
		/// 新增
		/// </summary>
		/// <param name="productId">产品Id</param>
		/// <param name="model">新增内容</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true, VerifyRight = true)]
		[HttpPost("Add/{productId}")]
		public ReturnResult<int> Add(int productId, [FromBody] ProductDiscountModel model)
		{
			var data = new ProductDiscount()
			{
				ProductId = productId,
				Name = model.Name,
				Rate = model.Rate,
				Amount = model.Amount,
				Total = model.Total,
				StartTime = model.StartTime,
				EndTime = model.EndTime,
				Status = model.Status,
				Note = model.Note
			};
			new ProductDiscountBLL(LoginInfo).Add(data);
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
		public ReturnResult<string> Update(int id, [FromBody] ProductDiscountModel model)
		{
			var data = new ProductDiscount()
			{
				Id = id,
				Name = model.Name,
				Rate = model.Rate,
				Amount = model.Amount,
				Total = model.Total,
				StartTime = model.StartTime,
				EndTime = model.EndTime,
				Status = model.Status,
				Note = model.Note
			};
			new ProductDiscountBLL(LoginInfo).Update(data);
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
			new ProductDiscountBLL(LoginInfo).UpdateStatus(id, status);
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
			new ProductDiscountBLL(LoginInfo).Delete(id);
			return Ok();
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="id">Id</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true, VerifyRight = true)]
		[HttpGet("Get/{id}")]
		public ReturnResult<ProductDiscount> Get(int id)
		{
			var result = new ProductDiscountBLL(LoginInfo).Get(id);
			return Json(result);
		}

		/// <summary>
		/// 分页查询
		/// </summary>
		/// <param name="productId">产品Id</param>
		/// <param name="name">名称</param>
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
		public ReturnResult<ProductArg<ProductDiscount>> List(int productId,
			string name = null, int? status = null, DateTime? start = null, DateTime? end = null,
			int? pageNumber = null, int? pageSize = null, string sortName = null, int? sortType = null)
		{
			var arg = new ProductArg<ProductDiscount>(pageNumber, pageSize, sortName, sortType)
			{
				ProductId = productId,
				Name = name,
				Status = status,
				Start = start,
				End = end
			};
			new ProductDiscountBLL(LoginInfo).List(arg);
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
