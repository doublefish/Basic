using Basic.Model;
using SqlSugar;
using System.Collections.Generic;

namespace Basic.DAL
{
	/// <summary>
	/// ProductImageDAL
	/// </summary>
	internal class ProductImageDAL : DAL<ProductImage>
	{
		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="productId"></param>
		/// <returns></returns>
		public ICollection<ProductImage> List(int productId)
		{
			var query = Db.Queryable<ProductImage>().Where(o => o.ProductId == productId);
			return Sort(query).ToList();
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="productIds"></param>
		/// <returns></returns>
		public ICollection<ProductImage> List(ICollection<int> productIds)
		{
			var query = Db.Queryable<ProductImage>().Where(o => productIds.Contains(o.ProductId));
			return Sort(query).ToList();
		}

		/// <summary>
		/// 默认排序
		/// </summary>
		/// <param name="query"></param>
		/// <returns></returns>
		private ISugarQueryable<ProductImage> Sort(ISugarQueryable<ProductImage> query)
		{
			return query.OrderBy(o => o.Sequence);
		}
	}
}
