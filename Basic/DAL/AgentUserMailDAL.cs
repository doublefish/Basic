using Basic.Model;
using Basic.Model.PageArg;
using SqlSugar;
using System;
using System.Linq;

namespace Basic.DAL
{
	/// <summary>
	/// AgentUserMailDAL
	/// </summary>
	internal class AgentUserMailDAL : DAL<AgentUserMail>
	{
		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="email"></param>
		/// <param name="type"></param>
		/// <returns></returns>
		public AgentUserMail Get(string email, int type)
		{
			return Db.Queryable<AgentUserMail>().Where(o => o.Email == email && o.Type == type).First();
		}

		/// <summary>
		/// 分页查询条件
		/// </summary>
		/// <param name="arg"></param>
		/// <param name="query"></param>
		/// <returns></returns>
		public override ISugarQueryable<AgentUserMail> Query(BaseArg<AgentUserMail> arg, ISugarQueryable<AgentUserMail> query)
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
			if (!string.IsNullOrEmpty(arg.AgentUsername))
			{
				query = query.Where(o => SqlFunc.Subqueryable<AgentUser>().Where(au => au.Id == o.AgentUserId && au.Username == arg.AgentUsername).Any());
			}
			//代理商用户手机号码
			if (!string.IsNullOrEmpty(arg.AgentUserMobile))
			{
				query = query.Where(o => SqlFunc.Subqueryable<AgentUser>().Where(a => a.Id == o.AgentUserId && a.Mobile == arg.AgentUserMobile).Any());
			}
			//电子邮箱
			if (!string.IsNullOrEmpty(arg.Email))
			{
				query = query.Where(o => o.Email == arg.Email);
			}
			//类型
			if (arg.Type.HasValue)
			{
				query = query.Where(o => o.Type == arg.Type.Value);
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
		public override ISugarQueryable<AgentUserMail> Sort(ISugarQueryable<AgentUserMail> query, string orderByField, OrderByType orderByType)
		{
			return orderByField switch
			{
				"AgentId" => query.OrderBy(o => o.AgentId, orderByType),
				"AgentUserId" => query.OrderBy(o => o.AgentUserId, orderByType),
				"Email" => query.OrderBy(o => o.Email, orderByType),
				"Type" => query.OrderBy(o => o.Type, orderByType),
				"Status" => query.OrderBy(o => o.Status, orderByType),
				"CreateTime" => query.OrderBy(o => o.CreateTime, orderByType),
				"UpdateTime" => query.OrderBy(o => o.UpdateTime, orderByType),
				_ => query.OrderBy(o => o.Id, orderByType)
			};
		}
	}
}
