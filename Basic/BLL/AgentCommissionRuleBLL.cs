using Adai.Standard.Models;
using Basic.DAL;
using Basic.Model;
using Basic.Model.PageArg;
using System;
using System.Collections.Generic;

namespace Basic.BLL
{
	/// <summary>
	/// AgentCommissionRuleBLL
	/// </summary>
	public class AgentCommissionRuleBLL : BLL<AgentCommissionRule, CommissionRuleArg<AgentCommissionRule>>
	{
		/// <summary>
		/// Dal
		/// </summary>
		readonly AgentCommissionRuleDAL Dal;

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="loginInfo"></param>
		public AgentCommissionRuleBLL(Token<TokenData> loginInfo = null) : base(loginInfo)
		{
			IDal = Dal = new AgentCommissionRuleDAL();
		}

		/// <summary>
		/// 验证
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public override string Validate(AgentCommissionRule data)
		{
			if (data.Id == 0)
			{
				if (data.ProductId < 1)
				{
					return "产品Id无效。";
				}
				data.AgentId = LoginInfo.Data.AgentId;
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
				return "佣金比例不能小于零且不能大于一。";
			}
			if (data.Amount < decimal.Zero)
			{
				return "佣金金额不能小于零。";
			}
			var parent = new CommissionRuleBLL().Get(data.ProductId, data.Year, data.Month);
			if (parent == null || !parent.IsEnabled)
			{
				return string.Format("管理员还未设置{0}年{1}月的佣金规则。", data.Year, data.Month);
			}
			if (data.Rate > parent.AgentRate)
			{
				return string.Format("佣金比例不能大于管理员设置的{0}%。", (parent.AgentRate * 100).ToString("0.##"));
			}
			if (data.Amount > parent.AgentAmount)
			{
				return string.Format("佣金金额不能大于管理员设置的￥{0}。", parent.AgentAmount.ToString("N"));
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
		public bool Exist(AgentCommissionRule data)
		{
			return Dal.Exist(data);
		}

		/// <summary>
		/// 启用（不可逆）
		/// </summary>
		/// <param name="id"></param>
		public void Enable(int id)
		{
			var data = new AgentCommissionRule()
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
		public AgentCommissionRule GetLastest(int productId)
		{
			return Dal.Get(LoginInfo.Data.AgentId, productId, null, null);
		}

		/// <summary>
		/// 查询最新
		/// </summary>
		/// <param name="agentId"></param>
		/// <param name="productId"></param>
		/// <returns></returns>
		public AgentCommissionRule GetLastest(int agentId, int productId)
		{
			return Dal.Get(agentId, productId, null, null);
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="productId"></param>
		/// <param name="year"></param>
		/// <param name="month"></param>
		/// <returns></returns>
		public AgentCommissionRule Get(int productId, int year, int month)
		{
			return Dal.Get(LoginInfo.Data.AgentId, productId, year, month);
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="agentId"></param>
		/// <param name="productId"></param>
		/// <param name="year"></param>
		/// <param name="month"></param>
		/// <returns></returns>
		public AgentCommissionRule Get(int agentId, int productId, int year, int month)
		{
			return Dal.Get(agentId, productId, year, month);
		}

		/// <summary>
		/// 查询已启用的
		/// </summary>
		/// <param name="productId"></param>
		/// <returns></returns>
		public ICollection<AgentCommissionRule> ListEnabled(int productId)
		{
			return Dal.List(LoginInfo.Data.AgentId, productId, Model.Config.Status.Enabled);
		}

		/// <summary>
		/// 查询已启用的
		/// </summary>
		/// <param name="productIds"></param>
		/// <returns></returns>
		public ICollection<AgentCommissionRule> ListEnabled(ICollection<int> productIds)
		{
			return Dal.List(LoginInfo.Data.AgentId, productIds, Model.Config.Status.Enabled);
		}

		/// <summary>
		/// 查询已启用的
		/// </summary>
		/// <param name="agentId"></param>
		/// <param name="productId"></param>
		/// <returns></returns>
		public ICollection<AgentCommissionRule> ListEnabled(int agentId, int productId)
		{
			return Dal.List(agentId, productId, Model.Config.Status.Enabled);
		}

		/// <summary>
		/// 查询已启用的
		/// </summary>
		/// <param name="agentId"></param>
		/// <param name="productIds"></param>
		/// <returns></returns>
		public ICollection<AgentCommissionRule> ListEnabled(int agentId, ICollection<int> productIds)
		{
			return Dal.List(agentId, productIds, Model.Config.Status.Enabled);
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="productId"></param>
		/// <param name="status"></param>
		/// <returns></returns>
		public ICollection<AgentCommissionRule> List(int productId, int? status = null)
		{
			return Dal.List(LoginInfo.Data.AgentId, productId, status);
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="productIds"></param>
		/// <param name="status"></param>
		/// <returns></returns>
		public ICollection<AgentCommissionRule> List(ICollection<int> productIds, int? status = null)
		{
			return Dal.List(LoginInfo.Data.AgentId, productIds, status);
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="agentId"></param>
		/// <param name="productId"></param>
		/// <param name="status"></param>
		/// <returns></returns>
		public ICollection<AgentCommissionRule> List(int agentId, int productId, int? status = null)
		{
			return Dal.List(agentId, productId, status);
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="agentId"></param>
		/// <param name="productIds"></param>
		/// <param name="status"></param>
		/// <returns></returns>
		public ICollection<AgentCommissionRule> List(int agentId, ICollection<int> productIds, int? status = null)
		{
			return Dal.List(agentId, productIds, status);
		}

		#endregion
	}
}
