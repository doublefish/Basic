using Basic.Model;
using Basic.Model.PageArg;
using SqlSugar;

namespace Basic.DAL
{
	/// <summary>
	/// AgentBankCardDAL
	/// </summary>
	internal class AgentBankCardDAL : DAL<AgentBankCard, BankCardArg<AgentBankCard>>
	{
		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="agentId"></param>
		/// <returns></returns>
		public AgentBankCard GetByAgentId(int agentId)
		{
			return Db.Queryable<AgentBankCard>().Where(o => o.AgentId == agentId).OrderBy(o => o.Id, OrderByType.Desc).First();
		}

		/// <summary>
		/// 分页查询条件
		/// </summary>
		/// <param name="arg"></param>
		/// <param name="query"></param>
		/// <returns></returns>
		public override ISugarQueryable<AgentBankCard> Query(BankCardArg<AgentBankCard> arg, ISugarQueryable<AgentBankCard> query)
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
			//开户行Id
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
		public override ISugarQueryable<AgentBankCard> Sort(ISugarQueryable<AgentBankCard> query, string orderByField, OrderByType orderByType)
		{
			return orderByField switch
			{
				"AgentId" => query.OrderBy(o => o.AgentId, orderByType),
				"BankId" => query.OrderBy(o => o.BankId, orderByType),
				"CardNumber" => query.OrderBy(o => o.CardNumber, orderByType),
				"Cardholder" => query.OrderBy(o => o.Cardholder, orderByType),
				"Branch" => query.OrderBy(o => o.Branch, orderByType),
				"CreateTime" => query.OrderBy(o => o.CreateTime, orderByType),
				"UpdateTime" => query.OrderBy(o => o.UpdateTime, orderByType),
				_ => query.OrderBy(o => o.Id, orderByType)
			};
		}
	}
}
