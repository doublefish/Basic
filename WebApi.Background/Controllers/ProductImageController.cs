using Basic.BLL;
using Basic.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebApi.Models;

namespace WebApi.Background.Controllers
{
	/// <summary>
	/// 产品图片
	/// </summary>
	[Route("api/Product/Image"), ApiExplorerSettings(GroupName = "business")]
	public class ProductImageController : ApiController
	{
		/// <summary>
		/// 新增（批量）
		/// </summary>
		/// <param name="productId">产品Id</param>
		/// <param name="imageUrls">图片地址</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true, VerifyRight = true)]
		[HttpPost("Add/{productId}")]
		public ReturnResult<int[]> Add(int productId, [FromBody] string[] imageUrls)
		{
			var datas = new List<ProductImage>();
			foreach (var imageUrl in imageUrls)
			{
				datas.Add(new ProductImage()
				{
					ProductId = productId,
					ImageUrl = imageUrl
				});
			}
			new ProductImageBLL(LoginInfo).Add(datas);
			return Json(datas.Select(o => o.Id).ToArray());
		}

		/// <summary>
		/// 排序
		/// </summary>
		/// <param name="ids">Ids</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true, VerifyRight = true)]
		[HttpPut("Sort")]
		public ReturnResult<string> Sort([FromBody] int[] ids)
		{
			new ProductImageBLL(LoginInfo).Sort(ids);
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
			new ProductImageBLL(LoginInfo).Delete(id);
			return Ok();
		}

		/// <summary>
		/// 删除（批量）
		/// </summary>
		/// <param name="ids">Ids</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true, VerifyRight = true)]
		[HttpDelete("Delete")]
		public ReturnResult<string> Delete([FromBody] int[] ids)
		{
			new ProductImageBLL(LoginInfo).Delete(ids);
			return Ok();
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="productId">产品Id</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true, VerifyRight = true)]
		[HttpGet("List/{productId}")]
		public ReturnResult<ICollection<ProductImage>> List(int productId)
		{
			var result = new ProductImageBLL(LoginInfo).List(productId);
			return Json(result);
		}
	}
}
