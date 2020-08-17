using Basic.Model;
using Basic.Model.PageArg;
using SqlSugar;
using System.Linq;

namespace Basic.DAL
{
	/// <summary>
	/// MemberSmsDAL
	/// </summary>
	internal class SmsDAL : DAL<Sms>
	{
		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="mobile"></param>
		/// <param name="type"></param>
		/// <returns></returns>
		public Sms Get(string mobile, int type)
		{
			return Db.Queryable<Sms>().Where(o => o.Mobile == mobile && o.Type == type).OrderBy(o => o.Id, OrderByType.Desc).First();
		}

		/// <summary>
		/// 分页查询条件
		/// </summary>
		/// <param name="arg"></param>
		/// <param name="query"></param>
		/// <returns></returns>
		public override ISugarQueryable<Sms> Query(BaseArg<Sms> arg, ISugarQueryable<Sms> query)
		{
			//手机号码
			if (!string.IsNullOrEmpty(arg.Mobile))
			{
				query = query.Where(o => o.Mobile == arg.Mobile);
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
		public override ISugarQueryable<Sms> Sort(ISugarQueryable<Sms> query, string orderByField, OrderByType orderByType)
		{
			return orderByField switch
			{
				"Mobile" => query.OrderBy(o => o.Mobile, orderByType),
				"Type" => query.OrderBy(o => o.Type, orderByType),
				"Status" => query.OrderBy(o => o.Status, orderByType),
				"CreateTime" => query.OrderBy(o => o.CreateTime, orderByType),
				"UpdateTime" => query.OrderBy(o => o.UpdateTime, orderByType),
				_ => query.OrderBy(o => o.Id, orderByType)
			};
		}
	}
}
