using Basic.Model;
using Basic.Model.PageArg;
using SqlSugar;
using System;
using System.Linq;

namespace Basic.DAL
{
	/// <summary>
	/// AgentUserWithdrawDAL
	/// </summary>
	internal class AgentUserWithdrawDAL : DAL<AgentUserWithdraw, BankCardArg<AgentUserWithdraw>>
	{
		/// <summary>
		/// 分页查询条件
		/// </summary>
		/// <param name="arg"></param>
		/// <param name="query"></param>
		/// <returns></returns>
		public override ISugarQueryable<AgentUserWithdraw> Query(BankCardArg<AgentUserWithdraw> arg, ISugarQueryable<AgentUserWithdraw> query)
		{
			//代理商Id
			if (arg.AgentId.HasValue)
			{
				query = query.Where(o => o.AgentId == arg.AgentId.Value);
			}
			//代理商名称
			if (!string.IsNullOrEmpty(arg.AgentName))
			{
				query = query.Where(o => SqlFunc.Subqueryable<Agent>().Where(a => a.Id == o.AgentId && a.Name.Contains(arg.AgentName)).Any());
			}
			//代理商用户Id
			if (arg.AgentUserId.HasValue)
			{
				query = query.Where(o => o.AgentUserId == arg.AgentUserId.Value);
			}
			//代理商用户名
			if (!string.IsNullOrEmpty(arg.Username))
			{
				query = query.Where(o => SqlFunc.Subqueryable<AgentUser>().Where(au => au.Id == o.AgentUserId && au.Username == arg.Username).Any());
			}
			//代理商用户手机号码
			if (!string.IsNullOrEmpty(arg.Mobile))
			{
				query = query.Where(o => SqlFunc.Subqueryable<AgentUser>().Where(au => au.Id == o.AgentUserId && au.Mobile == arg.Mobile).Any());
			}
			//编号
			if (!string.IsNullOrEmpty(arg.Number))
			{
				query = query.Where(o => o.Number == arg.Number);
			}
			//银行Id
			if (arg.BankId.HasValue)
			{
				query = query.Where(o => o.BankId == arg.BankId.Value);
			}
			//卡号
			if (!string.IsNullOrEmpty(arg.CardNumber))
			{
				query = query.Where(o => o.CardNumber == arg.CardNumber);
			}
			//持卡人
			if (!string.IsNullOrEmpty(arg.Cardholder))
			{
				query = query.Where(o => o.Cardholder == arg.Cardholder);
			}
			//支行
			if (!string.IsNullOrEmpty(arg.Branch))
			{
				query = query.Where(o => o.Branch.Contains(arg.Branch));
			}
			//状态
			if (arg.Status.HasValue)
			{
				query = query.Where(o => o.Status == arg.Status.Value);
			}
			else if (arg.Statuses != null && arg.Statuses.Length > 0)
			{
				query = query.Where(o => arg.Statuses.Contains(o.Status));
			}
			//开始时间
			if (arg.Start.HasValue)
			{
				var start = arg.Start.Value.Date;
				query = query.Where(o => o.CreateTime >= start);
			}
			//结束时间
			if (arg.End.HasValue)
			{
				var end = arg.End.Value.Date.AddDays(1);
				query = query.Where(o => o.CreateTime < end);
			}
			return base.Query(arg, query);
		}

		/// <summary>
		/// 分页查询排序
		/// </summary>
		/// <param name="query"></param>
		/// <param name="orderByField"></param>
		/// <param name="orderByType"></param>
		/// <returns></returns>
		public override ISugarQueryable<AgentUserWithdraw> Sort(ISugarQueryable<AgentUserWithdraw> query, string orderByField, OrderByType orderByType)
		{
			return orderByField switch
			{
				"AgentId" => query.OrderBy(o => o.AgentId, orderByType),
				"AgentUserId" => query.OrderBy(o => o.AgentUserId, orderByType),
				"Number" => query.OrderBy(o => o.Number, orderByType),
				"BankId" => query.OrderBy(o => o.BankId, orderByType),
				"CardNumber" => query.OrderBy(o => o.CardNumber, orderByType),
				"Cardholder" => query.OrderBy(o => o.Cardholder, orderByType),
				"Branch" => query.OrderBy(o => o.Branch, orderByType),
				"Status" => query.OrderBy(o => o.Status, orderByType),
				"CreateTime" => query.OrderBy(o => o.CreateTime, orderByType),
				"UpdateTime" => query.OrderBy(o => o.UpdateTime, orderByType),
				_ => query.OrderBy(o => o.Id, orderByType)
			};
		}
	}
}
