using Basic.Model;
using Basic.Model.PageArg;
using SqlSugar;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Basic.DAL
{
	/// <summary>
	/// AgentUserDAL
	/// </summary>
	internal class AgentUserDAL : DAL<AgentUser>
	{
		/// <summary>
		/// 创建查询表达式
		/// </summary>
		/// <returns></returns>
		public override Expression<Func<AgentUser, AgentUser>> CreateSelector()
		{
			return o => new AgentUser()
			{
				Id = o.Id,
				AgentId = o.AgentId,
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
				PromoCode = o.PromoCode,
				IsAdmin = o.IsAdmin,
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
		public void Add(AgentUser data, string password)
		{
			try
			{
				//重载指定事务的级别
				Db.Ado.BeginTran(System.Data.IsolationLevel.ReadCommitted);

				//新增
				Db.Insertable(data).ExecuteCommandIdentityIntoEntity();
				//新增密码
				Db.Insertable(new AgentUserPassword()
				{
					AgentUserId = data.Id,
					Type = Model.Config.Password.Type.Login,
					Password = CommonHelper.GeneratePassword(data.SecretKey, password),
					CreateTime = DateTime.Now
				}).ExecuteCommand();

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
		public AgentUser GetByUsername(string username, bool useCache = false)
		{
			if (useCache == true && IsCacheModel == true)
			{
				var key = string.Format("{0}-Username", CacheKey);
				var hashField = username;
				var pkValue = CacheDb.HashGet(key, hashField).ObjToInt();
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
			return Db.Queryable<AgentUser>().Where(o => o.Username == username).First();
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="email"></param>
		/// <param name="useCache"></param>
		/// <returns></returns>
		public AgentUser GetByEmail(string email, bool useCache = false)
		{
			if (useCache == true && IsCacheModel == true)
			{
				var key = string.Format("{0}-Email", CacheKey);
				var hashField = email;
				var pkValue = CacheDb.HashGet(key, hashField).ObjToInt();
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
			return Db.Queryable<AgentUser>().Where(o => o.Email == email).First();
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="mobile"></param>
		/// <param name="useCache"></param>
		/// <returns></returns>
		public AgentUser GetByMobile(string mobile, bool useCache = false)
		{
			if (useCache == true && IsCacheModel == true)
			{
				var key = string.Format("{0}-Mobile", CacheKey);
				var hashField = mobile;
				var pkValue = CacheDb.HashGet(key, hashField).ObjToInt();
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
			return Db.Queryable<AgentUser>().Where(o => o.Mobile == mobile).First();
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="promoCode"></param>
		/// <param name="useCache"></param>
		/// <returns></returns>
		public AgentUser GetByPromoCode(string promoCode, bool useCache = false)
		{
			if (useCache == true && IsCacheModel == true)
			{
				var key = string.Format("{0}-PromoCode", CacheKey);
				var hashField = promoCode;
				var pkValue = CacheDb.HashGet(key, hashField).ObjToInt();
				if (pkValue <= 0)
				{
					var result = GetByPromoCode(promoCode, false);
					if (result == null)
					{
						return null;
					}
					pkValue = result.Id;
					CacheDb.HashSet(key, hashField, pkValue);
				}
				return Get(pkValue, true);
			}
			return Db.Queryable<AgentUser>().Where(o => o.PromoCode == promoCode).First(Selector);
		}

		/// <summary>
		/// 分页查询条件
		/// </summary>
		/// <param name="arg"></param>
		/// <param name="query"></param>
		/// <returns></returns>
		public override ISugarQueryable<AgentUser> Query(BaseArg<AgentUser> arg, ISugarQueryable<AgentUser> query)
		{
			//代理商Id
			if (arg.AgentId.HasValue)
			{
				query = query.Where(o => o.AgentId == arg.AgentId.Value);
			}
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
		public override ISugarQueryable<AgentUser> Sort(ISugarQueryable<AgentUser> query, string orderByField, OrderByType orderByType)
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
				"PromoCode" => query.OrderBy(o => o.PromoCode, orderByType),
				"Status" => query.OrderBy(o => o.Status, orderByType),
				"CreateTime" => query.OrderBy(o => o.CreateTime, orderByType),
				"UpdateTime" => query.OrderBy(o => o.UpdateTime, orderByType),
				_ => query.OrderBy(o => o.Id, orderByType)
			};
		}
	}
}
