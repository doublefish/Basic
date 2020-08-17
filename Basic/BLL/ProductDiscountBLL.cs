using Adai.Standard.Models;
using Basic.DAL;
using Basic.Model;
using Basic.Model.PageArg;
using System;
using System.Collections.Generic;

namespace Basic.BLL
{
	/// <summary>
	/// ProductDiscountBLL
	/// </summary>
	public class ProductDiscountBLL : BLL<ProductDiscount, ProductArg<ProductDiscount>>
	{
		/// <summary>
		/// Dal
		/// </summary>
		readonly ProductDiscountDAL Dal;

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="loginInfo"></param>
		public ProductDiscountBLL(Token<TokenData> loginInfo = null) : base(loginInfo)
		{
			IDal = Dal = new ProductDiscountDAL();
		}

		/// <summary>
		/// 验证
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public override string Validate(ProductDiscount data)
		{
			if (data.Id == 0)
			{
				if (data.ProductId < 1)
				{
					return "产品Id无效。";
				}
			}
			if (!ValidateStatus(data.Status))
			{
				return "状态标识无效。";
			}
			data.UpdateTime = DateTime.Now;
			if (data.Id == 0)
			{
				data.CreateTime = data.UpdateTime;
			}
			return base.Validate(data);
		}

		/// <summary>
		/// 修改状态
		/// </summary>
		/// <param name="id"></param>
		/// <param name="status"></param>
		public override void UpdateStatus(int id, int status)
		{
			if (!ValidateStatus(status))
			{
				throw new CustomException("状态标识无效。");
			}
			var data = new ProductDiscount()
			{
				Id = id,
				Status = status
			};
			Dal.Update(data, new string[] { "Status" });
		}

		#region 扩展

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="productId"></param>
		/// <param name="date"></param>
		/// <returns></returns>
		public ProductDiscount Get(int productId, DateTime date)
		{
			return Dal.Get(productId, date);
		}

		/// <summary>
		/// 查询已启用的
		/// </summary>
		/// <param name="productId"></param>
		/// <param name="date"></param>
		/// <returns></returns>
		public ICollection<ProductDiscount> ListEnabled(int productId, DateTime? date = null)
		{
			return Dal.List(productId, date, Model.Config.Status.Enabled);
		}

		/// <summary>
		/// 查询已启用的
		/// </summary>
		/// <param name="productIds"></param>
		/// <param name="date"></param>
		/// <returns></returns>
		public ICollection<ProductDiscount> ListEnabled(ICollection<int> productIds, DateTime? date = null)
		{
			return Dal.List(productIds, date, Model.Config.Status.Enabled);
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="productId"></param>
		/// <param name="date"></param>
		/// <param name="status"></param>
		/// <returns></returns>
		public ICollection<ProductDiscount> List(int productId, DateTime? date = null, int? status = null)
		{
			return Dal.List(productId, date, status);
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="productIds"></param>
		/// <param name="date"></param>
		/// <param name="status"></param>
		/// <returns></returns>
		public ICollection<ProductDiscount> List(ICollection<int> productIds, DateTime? date = null, int? status = null)
		{
			return Dal.List(productIds, date, status);
		}

		#endregion
	}
}
