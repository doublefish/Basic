using Adai.Standard.Model;
using Basic.DAL;
using Basic.Model;
using System.Collections.Generic;

namespace Basic.BLL
{
	/// <summary>
	/// ProductImageBLL
	/// </summary>
	public class ProductImageBLL : BLL<ProductImage>
	{
		/// <summary>
		/// Dal
		/// </summary>
		readonly ProductImageDAL Dal;

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="loginInfo"></param>
		public ProductImageBLL(Token<TokenData> loginInfo = null) : base(loginInfo)
		{
			IDal = Dal = new ProductImageDAL();
		}

		/// <summary>
		/// 验证
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public override string Validate(ProductImage data)
		{
			data.ImageUrl = CommonHelper.ReplaceFilePath(data.ImageUrl);
			return base.Validate(data);
		}

		/// <summary>
		/// 关联数据
		/// </summary>
		/// <param name="list"></param>
		public override void List(ICollection<ProductImage> list)
		{
			foreach (var data in list)
			{
				data.ImageUrl = CommonHelper.RestoreFilePath(data.ImageUrl);
			}
		}

		#region 扩展

		/// <summary>
		/// 排序
		/// </summary>
		/// <param name="id"></param>
		/// <param name="sequence"></param>
		public void Sort(ICollection<int> ids)
		{
			var datas = new List<ProductImage>();
			var i = 0;
			foreach (var id in ids)
			{
				datas.Add(new ProductImage()
				{
					Id = id,
					Sequence = i
				});
				i++;
			}
			Dal.Update(datas, new string[] { "Sequence" });
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="productId"></param>
		/// <returns></returns>
		public ICollection<ProductImage> List(int productId)
		{
			var results = Dal.List(productId);
			if (results.Count > 0)
			{
				List(results);
			}
			return results;
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="productIds"></param>
		/// <returns></returns>
		public ICollection<ProductImage> List(ICollection<int> productIds)
		{
			var results = Dal.List(productIds);
			if (results.Count > 0)
			{
				List(results);
			}
			return results;
		}

		#endregion
	}
}
