using Adai.Base;
using Adai.Base.Ext;
using Basic.BLL;
using Basic.Model;
using Basic.Model.PageArg;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApi.Background.Models;
using WebApi.Models;

namespace WebApi.Background.Controllers
{
	/// <summary>
	/// 产品
	/// </summary>
	[Route("api/Product"), ApiExplorerSettings(GroupName = "business")]
	public class ProductController : ApiController
	{
		/// <summary>
		/// 新增
		/// </summary>
		/// <param name="model">新增内容</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true, VerifyRight = true)]
		[HttpPost("Add")]
		public ReturnResult<int> Add([FromBody] ProductModel model)
		{
			var data = new Product()
			{
				Name = model.Name,
				Type = model.Type,
				TagIds = model.TagIds,
				ThemeIds = model.ThemeIds,
				Price = model.Price,
				Overview = model.Overview,
				Recommends = model.Recommends,
				Sequence = model.Sequence,
				Status = model.Status,
				Note = model.Note
			};
			new ProductBLL(LoginInfo).Add(data);
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
		public ReturnResult<string> Update(int id, [FromBody] ProductModel model)
		{
			var data = new Product()
			{
				Id = id,
				Name = model.Name,
				Type = model.Type,
				TagIds = model.TagIds,
				ThemeIds = model.ThemeIds,
				Price = model.Price,
				Overview = model.Overview,
				Recommends = model.Recommends,
				Sequence = model.Sequence,
				Status = model.Status,
				Note = model.Note
			};
			new ProductBLL(LoginInfo).Update(data);
			return Ok();
		}

		/// <summary>
		/// 修改内容
		/// </summary>
		/// <param name="id">Id</param>
		/// <param name="property">修改属性：Feature/Cost/Notice/Visa/Book</param>
		/// <param name="content">修改内容</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true, VerifyRight = true)]
		[HttpPut("Update/{id}/{property}")]
		public ReturnResult<string> Update(int id, string property, [FromBody] string content)
		{
			new ProductBLL(LoginInfo).Update(id, property, content);
			return Ok();
		}

		/// <summary>
		/// 修改首页展示
		/// </summary>
		/// <param name="id">Id</param>
		/// <param name="index">修改内容</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true, VerifyRight = true)]
		[HttpPut("UpdateDisplay/{id}")]
		public ReturnResult<string> UpdateDisplay(int id, [FromBody] int index)
		{
			new ProductBLL(LoginInfo).UpdateDisplay(id, index);
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
			new ProductBLL(LoginInfo).UpdateCover(id, cover);
			return Ok();
		}

		/// <summary>
		/// 修改封面
		/// </summary>
		/// <param name="id">Id</param>
		/// <param name="cover">修改内容</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true, VerifyRight = true)]
		[HttpPut("UpdateCover1/{id}")]
		public ReturnResult<string> UpdateCover1(int id, [FromBody] string cover)
		{
			new ProductBLL(LoginInfo).UpdateCover(id, cover, 1);
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
			new ProductBLL(LoginInfo).UpdateStatus(id, status);
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
			new ProductBLL(LoginInfo).Delete(id);
			return Ok();
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="id">Id</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true, VerifyRight = true)]
		[HttpGet("Get/{id}")]
		public ReturnResult<Product> Get(int id)
		{
			var result = new ProductBLL(LoginInfo).Get(id);
			result.Feature = null;
			result.Notice = null;
			result.Book = null;
			result.Cost = null;
			return Json(result);
		}

		/// <summary>
		/// 查询详细内容
		/// </summary>
		/// <param name="id">Id</param>
		/// <param name="name">属性：Feature/Cost/Notice/Visa/Book</param>
		/// <returns></returns>
		[ApiAuthorize(Frequency = 5D, VerifyToken = true, VerifyRight = true)]
		[HttpGet("GetDetail/{id}/{name}")]
		public ReturnResult<string> GetDetail(int id, string name)
		{
			var result = new ProductBLL(LoginInfo).Get(id);
			var content = "";
			switch (name)
			{
				case "Feature": content = result.Feature; break;
				case "Notice": content = result.Notice; break;
				case "Book": content = result.Book; break;
				case "Cost": content = result.Cost; break;
				default: break;
			}
			return Json(content);
		}

		/// <summary>
		/// 分页查询
		/// </summary>
		/// <param name="name">名称</param>
		/// <param name="type">类型</param>
		/// <param name="typeName">类型名称</param>
		/// <param name="tags">标签（英文逗号拼接）</param>
		/// <param name="tagNames">标签名称（英文逗号拼接)</param>
		/// <param name="themes">主题（英文逗号拼接）</param>
		/// <param name="status">状态</param>
		/// <param name="pageNumber">页码（从0开始），默认0</param>
		/// <param name="pageSize">每页条数，默认20</param>
		/// <param name="sortName">排序字段</param>
		/// <param name="sortType">排序方式，默认0，0：降序，1：升序</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true, VerifyRight = true)]
		[HttpGet("List")]
		public ReturnResult<ProductArg<Product>> List(string name = null, int? type = null, string typeName = null,
			string tags = null, string tagNames = null, string themes = null, int? status = null,
			int? pageNumber = null, int? pageSize = null, string sortName = null, int? sortType = null)
		{
			var arg = new ProductArg<Product>(pageNumber, pageSize, sortName, sortType)
			{
				Name = name,
				Type = type,
				TypeName = typeName,
				Status = status
			};
			if (!string.IsNullOrEmpty(tags))
			{
				arg.Tags = tags.Split(',').ToInt32();
			}
			if (!string.IsNullOrEmpty(tagNames))
			{
				arg.TagNames = tagNames.Split(',');
			}
			if (!string.IsNullOrEmpty(themes))
			{
				arg.Themes = themes.Split(',').ToInt32();
			}
			new ProductBLL(LoginInfo).List(arg);
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
			var results = ConfigIntHelper<Basic.Model.Config.Product.Type>.KeyValuePairs;
			return Json(results);
		}

		/// <summary>
		/// 查询所有标签
		/// </summary>
		/// <returns></returns>
		[ApiAuthorize]
		[HttpGet("ListTag")]
		public ReturnResult<ICollection<Dict>> ListTag()
		{
			var results = new DictBLL(LoginInfo).ListByParentCode(Basic.Model.Config.Product.Tag.Root, true);
			return Json(results);
		}

		/// <summary>
		/// 查询所有主题
		/// </summary>
		/// <returns></returns>
		[ApiAuthorize]
		[HttpGet("ListTheme")]
		public ReturnResult<ICollection<Dict>> ListTheme()
		{
			var results = new DictBLL(LoginInfo).ListByParentCode(Basic.Model.Config.Dict.Business.Theme, true);
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
	}
}
