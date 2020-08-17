using Basic.Model;
using Basic.Model.PageArg;
using SqlSugar;
using System.Linq;

namespace Basic.DAL
{
	/// <summary>
	/// CustomerDAL
	/// </summary>
	internal class CustomerDAL : DAL<Customer>
	{
		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="mobile"></param>
		/// <returns></returns>
		public Customer GetByMobile(string mobile)
		{
			return Db.Queryable<Customer>().Where(o => o.Mobile == mobile).OrderBy(o => o.Id, OrderByType.Desc).First();
		}

		/// <summary>
		/// 分页查询条件
		/// </summary>
		/// <param name="arg"></param>
		/// <param name="query"></param>
		/// <returns></returns>
		public override ISugarQueryable<Customer> Query(BaseArg<Customer> arg, ISugarQueryable<Customer> query)
		{
			//姓名
			if (!string.IsNullOrEmpty(arg.FullName))
			{
				query = query.WhereConcatLike(new string[] { "LastName", "FirstName" }, arg.FullName);
			}
			//证件类型
			if (arg.IdType.HasValue)
			{
				query = query.Where(o => o.IdType == arg.IdType.Value);
			}
			//证件号码
			if (!string.IsNullOrEmpty(arg.IdNumber))
			{
				query = query.Where(o => o.IdNumber == arg.IdNumber);
			}
			//手机号码
			if (!string.IsNullOrEmpty(arg.Mobile))
			{
				query = query.Where(o => o.Mobile == arg.Mobile);
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
		public override ISugarQueryable<Customer> Sort(ISugarQueryable<Customer> query, string orderByField, OrderByType orderByType)
		{
			return orderByField switch
			{
				"FirstName" => query.OrderBy(o => o.FirstName, orderByType),
				"LastName" => query.OrderBy(o => o.LastName, orderByType),
				"Sex" => query.OrderBy(o => o.Sex, orderByType),
				"Birthday" => query.OrderBy(o => o.Birthday, orderByType),
				"IdType" => query.OrderBy(o => o.IdType, orderByType),
				"IdNumber" => query.OrderBy(o => o.IdNumber, orderByType),
				"Email" => query.OrderBy(o => o.Email, orderByType),
				"Mobile" => query.OrderBy(o => o.Mobile, orderByType),
				"Status" => query.OrderBy(o => o.Status, orderByType),
				"CreateTime" => query.OrderBy(o => o.CreateTime, orderByType),
				"UpdateTime" => query.OrderBy(o => o.UpdateTime, orderByType),
				_ => query.OrderBy(o => o.Id, orderByType)
			};
		}
	}
}
