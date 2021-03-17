using Adai.Standard.Models;
using Basic.DAL;
using Basic.Model;
using Basic.Model.PageArg;
using System;
using System.Collections.Generic;

namespace Basic.BLL
{
	/// <summary>
	/// CommissionRuleBLL
	/// </summary>
	public class CommissionRuleBLL : BLL<CommissionRule, CommissionRuleArg<CommissionRule>>
	{
		/// <summary>
		/// Dal
		/// </summary>
		readonly CommissionRuleDAL Dal;

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="loginInfo"></param>
		public CommissionRuleBLL(Token<TokenData> loginInfo = null) : base(loginInfo)
		{
			IDal = Dal = new CommissionRuleDAL();
		}

		/// <summary>
		/// 验证
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public override string Validate(CommissionRule data)
		{
			if (data.Id == 0)
			{
				if (data.ProductId < 1)
				{
					return "产品Id无效。";
				}
			}
			if (data.Year < 2020 || data.Year > 2021)
			{
				return "年份不能小于2020或大于2021。";
			}
			if (data.Month < 1 || data.Month > 12)
			{
				return "月份不能小于1或大于12。";
			}
			if (Exist(data))
			{
				return "相同记录（产品Id、年份、月份）已存在。";
			}
			if (data.Rate < decimal.Zero || data.Rate > decimal.One)
			{
				return "个人佣金比例不能小于零且不能大于一。";
			}
			if (data.Amount < decimal.Zero)
			{
				return "个人佣金金额不能小于零。";
			}
			data.UpdateTime = DateTime.Now;
			if (data.Id == 0)
			{
				data.Status = Model.Config.Status.Disabled;
				data.CreateTime = data.UpdateTime;
			}
			return base.Validate(data);
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="id"></param>
		public override void Delete(int id)
		{
			var result = Dal.Get(id);
			if (result.IsEnabled)
			{
				throw new CustomException("已启用的规则不可删除。");
			}
			Dal.Delete(id);
		}

		#region 扩展

		/// <summary>
		/// 是否存在相同的记录
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public bool Exist(CommissionRule data)
		{
			return Dal.Exist(data);
		}

		/// <summary>
		/// 启用（不可逆）
		/// </summary>
		/// <param name="id"></param>
		public void Enable(int id)
		{
			var data = new CommissionRule()
			{
				Id = id,
				Status = Model.Config.Status.Enabled
			};
			Dal.Update(data, new string[] { "Status" });
		}

		/// <summary>
		/// 查询最新
		/// </summary>
		/// <param name="productId"></param>
		/// <returns></returns>
		public CommissionRule GetLastest(int productId)
		{
			return Dal.Get(productId, null, null);
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="productId"></param>
		/// <param name="year"></param>
		/// <param name="month"></param>
		/// <returns></returns>
		public CommissionRule Get(int productId, int year, int month)
		{
			return Dal.Get(productId, year, month);
		}

		/// <summary>
		/// 查询已启用的
		/// </summary>
		/// <param name="productId"></param>
		/// <returns></returns>
		public ICollection<CommissionRule> ListEnabled(int productId)
		{
			return Dal.List(productId, Model.Config.Status.Enabled);
		}

		/// <summary>
		/// 查询已启用的
		/// </summary>
		/// <param name="productIds"></param>
		/// <returns></returns>
		public ICollection<CommissionRule> ListEnabled(ICollection<int> productIds)
		{
			return Dal.List(productIds, Model.Config.Status.Enabled);
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="productId"></param>
		/// <param name="status"></param>
		/// <returns></returns>
		public ICollection<CommissionRule> List(int productId, int? status = null)
		{
			return Dal.List(productId, status);
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="productIds"></param>
		/// <param name="status"></param>
		/// <returns></returns>
		public ICollection<CommissionRule> List(ICollection<int> productIds, int? status = null)
		{
			return Dal.List(productIds, status);
		}

		#endregion
	}
}
