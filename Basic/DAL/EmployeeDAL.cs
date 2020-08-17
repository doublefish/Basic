using Basic.Model;
using Basic.Model.PageArg;
using SqlSugar;
using System.Linq;

namespace Basic.DAL
{
	/// <summary>
	/// EmployeeDAL
	/// </summary>
	internal class EmployeeDAL : DAL<Employee>
	{
		/// <summary>
		/// 分页查询条件
		/// </summary>
		/// <param name="arg"></param>
		/// <param name="query"></param>
		/// <returns></returns>
		public override ISugarQueryable<Employee> Query(BaseArg<Employee> arg, ISugarQueryable<Employee> query)
		{
			//姓名
			if (!string.IsNullOrEmpty(arg.FullName))
			{
				query = query.WhereConcatLike(new string[] { "LastName", "FirstName" }, arg.FullName);
			}
			//手机
			if (!string.IsNullOrEmpty(arg.Mobile))
			{
				query = query.Where(o => o.Mobile == arg.Mobile);
			}
			//入职时间.开始时间-需要修改字段
			if (arg.Start.HasValue)
			{
				var start = arg.Start.Value.Date;
				query = query.Where(o => o.EntryTime >= start);
			}
			//入职时间.结束时间-需要修改字段
			if (arg.End.HasValue)
			{
				var end = arg.End.Value.Date.AddDays(1);
				query = query.Where(o => o.EntryTime < end);
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
		public override ISugarQueryable<Employee> Sort(ISugarQueryable<Employee> query, string orderByField, OrderByType orderByType)
		{
			return orderByField switch
			{
				"Name" => query.OrderBy(o => o.FirstName, orderByType),
				"EntryTime" => query.OrderBy(o => o.EntryTime, orderByType),
				"IdNumber" => query.OrderBy(o => o.IdNumber, orderByType),
				"Status" => query.OrderBy(o => o.Status, orderByType),
				"CreateTime" => query.OrderBy(o => o.CreateTime, orderByType),
				"UpdateTime" => query.OrderBy(o => o.UpdateTime, orderByType),
				_ => query.OrderBy(o => o.Id, orderByType)
			};
		}
	}
}
