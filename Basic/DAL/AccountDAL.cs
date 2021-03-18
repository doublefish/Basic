using Adai.Standard.Ext;
using Basic.Model;
using Basic.Model.PageArg;
using SqlSugar;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Basic.DAL
{
	/// <summary>
	/// AccountDAL
	/// </summary>
	internal class AccountDAL : DAL<Account>
	{
		/// <summary>
		/// 创建查询表达式
		/// </summary>
		/// <returns></returns>
		public override Expression<Func<Account, Account>> CreateSelector()
		{
			return o => new Account()
			{
				Id = o.Id,
				Username = o.Username,
				Nickname = o.Nickname,
				Avatar = o.Avatar,
				FirstName = o.FirstName,
				LastName = o.LastName,
				Sex = o.Sex,
				Birthday = o.Birthday,
				IdType = o.IdType,
				IdNumber = o.IdNumber,
				Email = o.Email,
				Mobile = o.Mobile,
				Tel = o.Tel,
				Balance = o.Balance,
				Freeze = o.Freeze,
				//SecretKey = o.SecretKey,
				Status = o.Status,
				CreateTime = o.CreateTime,
				UpdateTime = o.UpdateTime,
				Note = o.Note
			};
		}

		/// <summary>
		/// 增加
		/// </summary>
		/// <param name="data"></param>
		/// <param name="password"></param>
		/// <param name="promotion"></param>
		public void Add(Account data, string password, AccountPromotion promotion = null)
		{
			try
			{
				//重载指定事务的级别
				Db.Ado.BeginTran(System.Data.IsolationLevel.ReadCommitted);

				//新增
				Db.Insertable(data).ExecuteCommandIdentityIntoEntity();
				//新增密码
				Db.Insertable(new AccountPassword()
				{
					AccountId = data.Id,
					Type = Model.Config.Password.Type.Login,
					Password = CommonHelper.GeneratePassword(data.SecretKey, password),
					CreateTime = DateTime.Now
				}).ExecuteCommand();
				//新增推广关系
				if (promotion != null)
				{
					promotion.AccountId = data.Id;
					Db.Insertable(promotion).ExecuteCommand();
				}

				Db.Ado.CommitTran();
			}
			catch
			{
				Db.Ado.RollbackTran();
				throw;
			}
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="username"></param>
		/// <param name="useCache"></param>
		/// <returns></returns>
		public Account GetByUsername(string username, bool useCache = false)
		{
			if (useCache == true && IsCacheModel == true)
			{
				var key = string.Format("{0}-Username", CacheKey);
				var hashField = username;
				var pkValue = CacheDb.HashGet(key, hashField).ToInt32();
				if (pkValue <= 0)
				{
					var result = GetByUsername(username, false);
					if (result == null)
					{
						return null;
					}
					pkValue = result.Id;
					CacheDb.HashSet(key, hashField, pkValue);
				}
				return Get(pkValue, true);
			}
			return Db.Queryable<Account>().Where(o => o.Username == username).First();
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="email"></param>
		/// <param name="useCache"></param>
		/// <returns></returns>
		public Account GetByEmail(string email, bool useCache = false)
		{
			if (useCache == true && IsCacheModel == true)
			{
				var key = string.Format("{0}-Email", CacheKey);
				var hashField = email;
				var pkValue = CacheDb.HashGet(key, hashField).ToInt32();
				if (pkValue <= 0)
				{
					var result = GetByEmail(email, false);
					if (result == null)
					{
						return null;
					}
					pkValue = result.Id;
					CacheDb.HashSet(key, hashField, pkValue);
				}
				return Get(pkValue, true);
			}
			return Db.Queryable<Account>().Where(o => o.Email == email).First();
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="mobile"></param>
		/// <param name="useCache"></param>
		/// <returns></returns>
		public Account GetByMobile(string mobile, bool useCache = false)
		{
			if (useCache == true && IsCacheModel == true)
			{
				var key = string.Format("{0}-Mobile", CacheKey);
				var hashField = mobile;
				var pkValue = CacheDb.HashGet(key, hashField).ToInt32();
				if (pkValue <= 0)
				{
					var result = GetByMobile(mobile, false);
					if (result == null)
					{
						return null;
					}
					pkValue = result.Id;
					CacheDb.HashSet(key, hashField, pkValue);
				}
				return Get(pkValue, true);
			}
			return Db.Queryable<Account>().Where(o => o.Mobile == mobile).First();
		}

		/// <summary>
		/// 分页查询条件
		/// </summary>
		/// <param name="arg"></param>
		/// <param name="query"></param>
		/// <returns></returns>
		public override ISugarQueryable<Account> Query(BaseArg<Account> arg, ISugarQueryable<Account> query)
		{
			//用户名
			if (!string.IsNullOrEmpty(arg.Username))
			{
				query = query.Where(o => o.Username == arg.Username);
			}
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
		public override ISugarQueryable<Account> Sort(ISugarQueryable<Account> query, string orderByField, OrderByType orderByType)
		{
			return orderByField switch
			{
				"Username" => query.OrderBy(o => o.Username, orderByType),
				"Nickname" => query.OrderBy(o => o.Nickname, orderByType),
				"FirstName" => query.OrderBy(o => o.FirstName, orderByType),
				"LastName" => query.OrderBy(o => o.LastName, orderByType),
				"FullName" => query.OrderBy(o => o.LastName, orderByType).OrderBy(o => o.FirstName, orderByType),
				"Sex" => query.OrderBy(o => o.Sex, orderByType),
				"Birthday" => query.OrderBy(o => o.Birthday, orderByType),
				"IdType" => query.OrderBy(o => o.IdType, orderByType),
				"IdNumber" => query.OrderBy(o => o.IdNumber, orderByType),
				"Email" => query.OrderBy(o => o.Email, orderByType),
				"Mobile" => query.OrderBy(o => o.Mobile, orderByType),
				"Tel" => query.OrderBy(o => o.Tel, orderByType),
				"Balance" => query.OrderBy(o => o.Balance, orderByType),
				"Freeze" => query.OrderBy(o => o.Freeze, orderByType),
				"Status" => query.OrderBy(o => o.Status, orderByType),
				"CreateTime" => query.OrderBy(o => o.CreateTime, orderByType),
				"UpdateTime" => query.OrderBy(o => o.UpdateTime, orderByType),
				_ => query.OrderBy(o => o.Id, orderByType)
			};
		}
	}
}
