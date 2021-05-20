using Adai.Base;
using Adai.Standard.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Basic.BLL
{
	/// <summary>
	/// BLL
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public abstract class BLL<T> : BLL<T, Model.PageArg.BaseArg<T>>, IBLL<T>
		where T : class, new()
	{
		DAL.IDAL<T> dal;

		/// <summary>
		/// IDal
		/// </summary>
		protected new DAL.IDAL<T> IDal
		{
			get
			{
				return dal;
			}
			set
			{
				base.IDal = dal = value;
			}
		}

		/// <summary>
		/// 构造函数
		/// </summary>
		public BLL() : this(null)
		{
		}

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="loginInfo"></param>
		public BLL(Token<TokenData> loginInfo) : base(loginInfo)
		{
		}
	}

	/// <summary>
	/// BLL
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <typeparam name="A"></typeparam>
	public abstract class BLL<T, A> : BLL<T, int, A>, IBLL<T, A>
		where T : class, new()
		where A : Model.PageArg.BaseArg<T>
	{
		DAL.IDAL<T, A> dal;

		/// <summary>
		/// IDal
		/// </summary>
		protected new DAL.IDAL<T, A> IDal
		{
			get
			{
				return dal;
			}
			set
			{
				base.IDal = dal = value;
			}
		}

		/// <summary>
		/// 构造函数
		/// </summary>
		public BLL() : this(null)
		{
		}

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="loginInfo"></param>
		public BLL(Token<TokenData> loginInfo) : base(loginInfo)
		{
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="id"></param>
		/// <param name="useCache"></param>
		/// <returns></returns>
		public override T Get(int id, bool useCache = false)
		{
			return base.Get(id, useCache);
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="id"></param>
		public override void Delete(int id)
		{
			base.Delete(id);
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="ids"></param>
		public override void Delete(ICollection<int> ids)
		{
			base.Delete(ids);
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="ids"></param>
		/// <param name="useCache"></param>
		/// <returns></returns>
		public override ICollection<T> ListByPks(ICollection<int> ids, bool useCache = false)
		{
			return base.ListByPks(ids, useCache);
		}
	}

	/// <summary>
	/// BLL
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <typeparam name="P"></typeparam>
	/// <typeparam name="A"></typeparam>
	public abstract class BLL<T, P, A> : IBLL<T, P, A>
		where T : class, new()
		where A : Model.PageArg.BaseArg<T>
	{
		Type type;

		/// <summary>
		/// IDal
		/// </summary>
		protected DAL.IDAL<T, P, A> IDal { get; set; }

		/// <summary>
		/// 类型
		/// </summary>
		public Type Type
		{
			get
			{
				if (type == null)
				{
					type = GetType();
				}
				return type;
			}
		}

		/// <summary>
		/// 语言
		/// </summary>
		public string Language => CultureInfo.CurrentCulture.ToString();

		/// <summary>
		/// 登录信息
		/// </summary>
		protected Token<TokenData> LoginInfo { get; set; }

		/// <summary>
		/// 构造函数
		/// </summary>
		public BLL() : this(null)
		{
		}

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="loginInfo"></param>
		public BLL(Token<TokenData> loginInfo)
		{
			LoginInfo = loginInfo;
		}

		/// <summary>
		/// 验证状态
		/// </summary>
		/// <param name="status"></param>
		/// <returns></returns>
		public virtual bool ValidateStatus(int status)
		{
			return ConfigIntHelper<Model.Config.Status>.ContainsKey(status);
		}

		/// <summary>
		/// 验证
		/// </summary>
		/// <param name="data"></param>
		public virtual string Validate(T data)
		{
			return null;
		}

		/// <summary>
		/// 新增
		/// </summary>
		/// <param name="data"></param>
		public virtual void Add(T data)
		{
			var error = Validate(data);
			if (!string.IsNullOrEmpty(error))
			{
				throw new CustomException(error);
			}
			IDal.Add(data);
		}

		/// <summary>
		/// 新增（批量）
		/// </summary>
		/// <param name="datas"></param>
		public virtual void Add(ICollection<T> datas)
		{
			var errors = new List<string>();
			foreach (var data in datas)
			{
				var error = Validate(data);
				errors.Add(error);
			}
			if (errors.Any(o => o != null))
			{
				throw new CustomException(string.Join("\r\n", errors));
			}
			IDal.Add(datas);
		}

		/// <summary>
		/// 修改
		/// </summary>
		/// <param name="data"></param>
		public virtual void Update(T data)
		{
			var error = Validate(data);
			if (!string.IsNullOrEmpty(error))
			{
				throw new CustomException(error);
			}
			IDal.Update(data);
		}

		/// <summary>
		/// 修改（批量）
		/// </summary>
		/// <param name="datas"></param>
		/// <param name="properties"></param>
		public virtual void Update(ICollection<T> datas)
		{
			IDal.Update(datas);
		}

		/// <summary>
		/// 修改状态
		/// </summary>
		/// <param name="id"></param>
		/// <param name="status"></param>
		public virtual void UpdateStatus(int id, int status)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="pkValue"></param>
		public virtual void Delete(P pkValue)
		{
			IDal.Delete(pkValue);
		}

		/// <summary>
		/// 删除（批量）
		/// </summary>
		/// <param name="pkValue"></param>
		public virtual void Delete(ICollection<P> pkValues)
		{
			IDal.Delete(pkValues);
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="pkValue"></param>
		/// <param name="useCache"></param>
		/// <returns></returns>
		public virtual T Get(P pkValue, bool useCache = false)
		{
			var result = IDal.Get(pkValue, useCache);
			if (result == null)
			{
				throw new CustomException("数据不存在。");
			}
			if (LoginInfo != null && LoginInfo.Data.Type != "User")
			{
				if (!VerifyRight(result))
				{
					throw new CustomException("没有查询该数据的权限。");
				}
			}
			List(new List<T>() { result });
			return result;
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="pkValues"></param>
		/// <param name="useCache"></param>
		/// <returns></returns>
		public virtual ICollection<T> ListByPks(ICollection<P> pkValues, bool useCache = false)
		{
			var results = IDal.ListByPks(pkValues, useCache);
			if (results.Count > 0)
			{
				List(results);
			}
			return results;
		}

		/// <summary>
		/// 查询所有
		/// </summary>
		/// <param name="useCache"></param>
		/// <returns></returns>
		public virtual ICollection<T> ListAll(bool useCache = false)
		{
			var results = IDal.ListAll(useCache);
			if (results.Count > 0)
			{
				List(results);
			}
			return results;
		}

		/// <summary>
		/// 分页查询
		/// </summary>
		/// <param name="arg"></param>
		public virtual A List(A arg)
		{
			if (LoginInfo != null && LoginInfo.Data.Type != "User")
			{
				VerifyRight(arg);
			}
			arg = IDal.List(arg);
			if (arg.Results.Count > 0)
			{
				List(arg.Results);
			}
			return arg;
		}

		/// <summary>
		/// 刷新缓存
		/// </summary>
		public virtual void RefreshCache()
		{
			IDal.RefreshCache();
		}

		/// <summary>
		/// 关联数据
		/// </summary>
		/// <param name="list"></param>
		/// <returns></returns>
		public virtual void List(ICollection<T> list)
		{
		}

		/// <summary>
		/// 验证权限
		/// </summary>
		/// <param name="data"></param>
		public virtual bool VerifyRight(T data)
		{
			return true;
		}

		/// <summary>
		/// 验证权限
		/// </summary>
		/// <param name="datas"></param>
		public virtual bool VerifyRight(ICollection<T> datas)
		{
			return true;
		}

		/// <summary>
		/// 验证权限
		/// </summary>
		/// <param name="arg"></param>
		public virtual void VerifyRight(A arg)
		{
			switch (LoginInfo.Data.Type)
			{
				case "Account":
					arg.AccountId = LoginInfo.Id;
					break;
				case "AgentUser":
					arg.AgentId = LoginInfo.Data.AgentId;
					if (!LoginInfo.Data.IsAdminOfAgent)
					{
						arg.AgentUserId = LoginInfo.Id;
					}
					break;
				case "User": break;
				default: break;
			}
		}
	}
}
