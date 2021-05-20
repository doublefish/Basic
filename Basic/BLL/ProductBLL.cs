using Adai.Base;
using Adai.Standard.Model;
using Basic.DAL;
using Basic.Model;
using Basic.Model.PageArg;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Basic.BLL
{
	/// <summary>
	/// ProductBLL
	/// </summary>
	public class ProductBLL : BLL<Product, ProductArg<Product>>
	{
		/// <summary>
		/// Dal
		/// </summary>
		readonly ProductDAL Dal;

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="loginInfo"></param>
		public ProductBLL(Token<TokenData> loginInfo = null) : base(loginInfo)
		{
			IDal = Dal = new ProductDAL();
		}

		/// <summary>
		/// 验证
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public override string Validate(Product data)
		{
			if (string.IsNullOrEmpty(data.Name))
			{
				return "名称不能为空。";
			}
			if (!ConfigIntHelper<Model.Config.Product.Type>.ContainsKey(data.Type))
			{
				return "类型标识无效。";
			}
			if (data.ThemeIds == null || data.ThemeIds.Count == 0)
			{
				return "主题不能为空。";
			}
			if (data.Price <= decimal.Zero)
			{
				return "价格不能小于或等于零。";
			}
			if (data.Sequence <= 0)
			{
				return "序号不能小于或等于零。";
			}
			if (!ValidateStatus(data.Status))
			{
				return "状态标识无效。";
			}
			data.Tags = CommonHelper.IdsToString(data.TagIds);
			data.Themes = CommonHelper.IdsToString(data.ThemeIds);
			data.Feature = CommonHelper.ReplaceFilePath(data.Feature);
			data.Notice = CommonHelper.ReplaceFilePath(data.Notice);
			data.Book = CommonHelper.ReplaceFilePath(data.Book);
			data.Cost = CommonHelper.ReplaceFilePath(data.Cost);
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
			var data = new Product()
			{
				Id = id,
				Status = status
			};
			Dal.Update(data, new string[] { "Status" });
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="id"></param>
		/// <param name="useCache"></param>
		/// <returns></returns>
		public override Product Get(int id, bool useCache = false)
		{
			var result = base.Get(id, useCache);
			if (result != null)
			{
				result.Feature = CommonHelper.RestoreFilePath(result.Feature);
				result.Notice = CommonHelper.RestoreFilePath(result.Notice);
				result.Book = CommonHelper.RestoreFilePath(result.Book);
				result.Cost = CommonHelper.RestoreFilePath(result.Cost);
			}
			return result;
		}

		/// <summary>
		/// 分页查询
		/// </summary>
		/// <param name="arg"></param>
		/// <returns></returns>
		public override ProductArg<Product> List(ProductArg<Product> arg)
		{
			if (!string.IsNullOrEmpty(arg.TypeName))
			{
				arg.Type = ConfigIntHelper<Model.Config.Product.Type>.GetKey(arg.TypeName);
			}
			if (arg.TagNames != null && arg.TagNames.Length > 0)
			{
				var dicts = new DictBLL(LoginInfo).ListByParentCode(Model.Config.Product.Tag.Root, true);
				arg.Tags = dicts.Where(o => arg.TagNames.Contains(o.Name)).Select(o => o.Id).ToArray();
			}
			return base.List(arg);
		}

		/// <summary>
		/// 关联数据
		/// </summary>
		/// <param name="list"></param>
		public override void List(ICollection<Product> list)
		{
			var ids = list.Select(o => o.Id).Distinct().ToArray();
			var dictIds = new List<int>();
			foreach (var data in list)
			{
				data.TagIds = CommonHelper.StringToIds(data.Tags);
				data.ThemeIds = CommonHelper.StringToIds(data.Themes);
				dictIds.AddRange(data.TagIds);
				dictIds.AddRange(data.ThemeIds);
			}
			var dicts = new DictBLL().ListByPks(dictIds.Distinct().ToArray(), true);
			var discounts = new ProductDiscountBLL().ListEnabled(ids, DateTime.Now);
			foreach (var data in list)
			{
				data.TagNames = dicts.Where(o => data.TagIds.Contains(o.Id)).Select(o => o.Name).ToArray();
				data.ThemeNames = dicts.Where(o => data.ThemeIds.Contains(o.Id)).Select(o => o.Name).ToArray();
				data.Cover = CommonHelper.RestoreFilePath(data.Cover);
				data.Cover1 = CommonHelper.RestoreFilePath(data.Cover1);
				data.Discount = discounts.Where(o => o.ProductId == data.Id).FirstOrDefault();
			}
		}

		#region 扩展

		/// <summary>
		/// 修改内容
		/// </summary>
		/// <param name="id"></param>
		/// <param name="property">属性：Feature/Cost/Notice/Visa/Book</param>
		/// <param name="content">内容</param>
		public void Update(int id, string property, string content)
		{
			var data = new Product()
			{
				Id = id,
				UpdateTime = DateTime.Now
			};
			switch (property)
			{
				case "Feature": data.Feature = content; break;
				case "Notice": data.Notice = content; break;
				case "Book": data.Book = content; break;
				case "Cost": data.Cost = content; break;
				default: throw new CustomException("属性名称无效");
			}
			Dal.Update(data, new string[] { property, "UpdateTime" });
		}

		/// <summary>
		/// 修改首页展示
		/// </summary>
		/// <param name="id"></param>
		/// <param name="index">顺序</param>
		public void UpdateDisplay(int id, int index)
		{
			var data = new Product()
			{
				Id = id,
				Sequence = index
			};
			Dal.Update(data, new string[] { "DisplayIndex" });
		}

		/// <summary>
		/// 修改封面
		/// </summary>
		/// <param name="id"></param>
		/// <param name="cover"></param>
		/// <param name="index">0：默认，1：首页</param>
		public void UpdateCover(int id, string cover, int index = 0)
		{
			cover = CommonHelper.ReplaceFilePath(cover);
			var data = new Product() { Id = id };
			string propertyName;
			if (index > 0)
			{
				data.Cover1 = cover;
				propertyName = "Cover1";
			}
			else
			{
				data.Cover = cover;
				propertyName = "Cover";
			}
			Dal.Update(data, new string[] { propertyName });
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="id"></param>
		/// <param name="useCache"></param>
		/// <returns></returns>
		public Product GetDetail(int id, bool useCache = false)
		{
			var result = Get(id, useCache);
			if (result != null)
			{
				result.Images = new ProductImageBLL().List(result.Id).Select(o => o.ImageUrl).ToArray();
			}
			return result;
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="id"></param>
		/// <param name="useCache"></param>
		/// <returns></returns>
		public Product View(int id, bool useCache = false)
		{
			var result = GetDetail(id, useCache);
			if (result != null)
			{
				//更新点击次数
				Dal.UpdateClicks(id);
			}
			return result;
		}


		/// <summary>
		/// 查询 - 根据推荐人数倒序
		/// </summary>
		/// <param name="typeName"></param>
		/// <param name="tagName"></param>
		/// <param name="take"></param>
		/// <returns></returns>
		public ICollection<Product> ListByRecommends(string typeName, string tagName, int take = 10)
		{
			return List(typeName, tagName, take, "Recommends", OrderByType.Desc);
		}

		/// <summary>
		/// 查询 - 根据推荐人数倒序
		/// </summary>
		/// <param name="typeName"></param>
		/// <param name="tagName"></param>
		/// <param name="take"></param>
		/// <returns></returns>
		public ICollection<Product> ListByRecommends(int? type, int? tag, int take = 10)
		{
			return List(type, tag, take, "Recommends", OrderByType.Desc);
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="typeName"></param>
		/// <param name="tagName"></param>
		/// <param name="take"></param>
		/// <param name="orderByField"></param>
		/// <param name="orderByType"></param>
		/// <returns></returns>
		public ICollection<Product> List(string typeName, string tagName, int take = 10, string orderByField = "Sequence", OrderByType orderByType = OrderByType.Asc)
		{
			int? type = default, tag = default;
			if (!string.IsNullOrEmpty(typeName))
			{
				type = ConfigIntHelper<Model.Config.Product.Type>.GetKey(typeName);
			}
			if (!string.IsNullOrEmpty(tagName))
			{
				var dict = new DictBLL().GetByName(tagName, Model.Config.Product.Tag.Root, true);
				if (dict != null)
				{
					tag = dict.Id;
				}
			}
			return List(type, tag, take, orderByField, orderByType);
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="type"></param>
		/// <param name="tag"></param>
		/// <param name="take"></param>
		/// <param name="orderByField"></param>
		/// <param name="orderByType"></param>
		/// <returns></returns>
		public ICollection<Product> List(int? type, int? tag, int take = 10, string orderByField = "Sequence", OrderByType orderByType = OrderByType.Asc)
		{
			var results = Dal.List(type, tag, take, orderByField, orderByType);
			if (results.Count > 0)
			{
				List(results);
			}
			return results.ToArray();
		}

		#endregion
	}
}
