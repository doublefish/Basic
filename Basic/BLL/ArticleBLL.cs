using Adai.Base;
using Adai.Standard.Model;
using Basic.DAL;
using Basic.Model;
using Basic.Model.PageArg;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Basic.BLL
{
	/// <summary>
	/// ArticleBLL
	/// </summary>
	public class ArticleBLL : BLL<Article, ArticleArg<Article>>
	{
		/// <summary>
		/// Dal
		/// </summary>
		readonly ArticleDAL Dal;

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="loginInfo"></param>
		public ArticleBLL(Token<TokenData> loginInfo = null) : base(loginInfo)
		{
			IDal = Dal = new ArticleDAL();
		}

		/// <summary>
		/// 验证状态
		/// </summary>
		/// <param name="status"></param>
		/// <returns></returns>
		public override bool ValidateStatus(int status)
		{
			return ConfigIntHelper<Model.Config.Article.Status>.ContainsKey(status);
		}

		/// <summary>
		/// 验证
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public override string Validate(Article data)
		{
			if (string.IsNullOrEmpty(data.Title))
			{
				return "标题不能为空。";
			}
			if (data.SectionIds == null || data.SectionIds.Count == 0)
			{
				return "板块不能为空。";
			}
			if (!ValidateStatus(data.Status))
			{
				return "状态标识无效。";
			}
			data.Sections = CommonHelper.IdsToString(data.SectionIds);
			data.UpdateTime = DateTime.Now;
			if (data.Id == 0)
			{
				data.Clicks = 0;
				data.Favorites = 0;
				data.Shares = 0;
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
			var data = new Article()
			{
				Id = id,
				Status = status
			};
			Dal.Update(data, new string[] { "Status" });
		}

		/// <summary>
		/// 关联数据
		/// </summary>
		/// <param name="list"></param>
		public override void List(ICollection<Article> list)
		{
			var dictIds = new List<int>();
			foreach (var data in list)
			{
				data.SectionIds = CommonHelper.StringToIds(data.Sections);
				dictIds.AddRange(data.SectionIds);
			}
			var dicts = new DictBLL().ListByPks(dictIds.Distinct().ToArray(), true);
			foreach (var data in list)
			{
				data.SectionNames = dicts.Where(o => data.SectionIds.Contains(o.Id)).Select(o => o.Name).ToArray();
			}
		}

		#region 扩展

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="id"></param>
		/// <param name="useCache"></param>
		/// <returns></returns>
		public Article GetDetail(int id, bool useCache = false)
		{
			var result = Get(id, useCache);
			if (result != null)
			{
				result.SectionIds = CommonHelper.StringToIds(result.Sections);
				var dicts = new DictBLL().ListByPks(result.SectionIds.Distinct().ToArray(), true);
				result.SectionNames = dicts.Select(o => o.Name).ToArray();
			}
			return result;
		}

		#endregion

		#region 业务

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="sectionCode"></param>
		/// <param name="useCache"></param>
		/// <returns></returns>
		public Article GetBySectionCode(string sectionCode, bool useCache = false)
		{
			var topic = new DictBLL().GetByCode(sectionCode, Model.Config.Article.Section.Root, true);
			return Dal.GetBySection(topic.Id, useCache);
		}

		/// <summary>
		/// 查询.关于我们
		/// </summary>
		/// <param name="useCache"></param>
		/// <returns></returns>
		public string GetAboutUs(bool useCache = false)
		{
			var result = GetBySectionCode(Model.Config.Article.Section.AboutUs, useCache);
			if (result == null)
			{
				throw new CustomException("文章不存在。");
			}
			return result.Content;
		}

		/// <summary>
		/// 查询.联系我们
		/// </summary>
		/// <param name="useCache"></param>
		/// <returns></returns>
		public string GetContactUs(bool useCache = false)
		{
			var result = GetBySectionCode(Model.Config.Article.Section.ContactUs, useCache);
			if (result == null)
			{
				throw new CustomException("文章不存在。");
			}
			return result.Content;
		}

		/// <summary>
		/// 查询.常见问题
		/// </summary>
		/// <param name="useCache"></param>
		/// <returns></returns>
		public string GetFAQ(bool useCache = false)
		{
			var result = GetBySectionCode(Model.Config.Article.Section.FAQ, useCache);
			if (result == null)
			{
				throw new CustomException("文章不存在。");
			}
			return result.Content;
		}

		/// <summary>
		/// 查询.注册协议
		/// </summary>
		/// <param name="useCache"></param>
		/// <returns></returns>
		public string GetSignup(bool useCache = false)
		{
			var result = GetBySectionCode(Model.Config.Article.Section.Signup, useCache);
			if (result == null)
			{
				throw new CustomException("文章不存在。");
			}
			return result.Content;
		}

		#endregion
	}
}
