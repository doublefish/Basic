using Adai.Standard.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Basic.BLL
{
	/// <summary>
	/// TreeBLL
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public abstract class TreeBLL<T> : TreeBLL<T, Model.PageArg.BaseArg<T>>, ITreeBLL<T>
		where T : Model.TreeModel<T>, new()
	{
		DAL.ITreeDAL<T> dal;

		/// <summary>
		/// IGeneralDal
		/// </summary>
		protected new DAL.ITreeDAL<T> IDal
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
		public TreeBLL() : this(null)
		{
		}

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="loginInfo"></param>
		public TreeBLL(Token<TokenData> loginInfo) : base(loginInfo)
		{
		}
	}

	/// <summary>
	/// TreeBLL
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <typeparam name="A"></typeparam>
	public abstract class TreeBLL<T, A> : BLL<T, A>, ITreeBLL<T, A>
		where T : Model.TreeModel<T>, new()
		where A : Model.PageArg.BaseArg<T>
	{
		DAL.ITreeDAL<T, A> dal;

		/// <summary>
		/// IGeneralDal
		/// </summary>
		protected new DAL.ITreeDAL<T, A> IDal
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
		public TreeBLL() : this(null)
		{
		}

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="loginInfo"></param>
		public TreeBLL(Token<TokenData> loginInfo) : base(loginInfo)
		{
		}

		/// <summary>
		/// 验证
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public override string Validate(T data)
		{
			if (!string.IsNullOrEmpty(data.Code) && ExistByCode(data.Id, data.Code, data.ParentId, true))
			{
				return "编码已存在。";
			}
			if (string.IsNullOrEmpty(data.Name))
			{
				return "名称不能为空。";
			}
			if (ExistByName(data.Id, data.Name, data.ParentId, true))
			{
				return "名称已存在。";
			}
			if (data.ParentId > 0)
			{
				var parent = IDal.Get(data.ParentId, true);
				if (parent == null)
				{
					return "父节点不存在。";
				}
			}
			if (data.Sequence <= 0)
			{
				return "序号不能小于或等于零。";
			}
			if (!ValidateStatus(data.Status))
			{
				return "状态标识无效。";
			}
			data.UpdateTime = DateTime.Now;
			if (data.Id == 0)
			{
				if (data.ParentId > 0)
				{
					var parent = dal.Get(data.ParentId, true);
					data.Parents = string.Format("{0}{1},", parent.Parents, parent.Id);
				}
				else
				{
					data.Parents = ",0,";
				}
				data.CreateTime = data.UpdateTime;
			}
			return base.Validate(data);
		}

		/// <summary>
		/// 修改状态
		/// </summary>
		/// <param name="id"></param>
		/// <param name="status"></param>
		public override void UpdateStatus(int id, int status)
		{
			if (!ValidateStatus(status))
			{
				throw new CustomException("状态标识无效。");
			}
			var data = new T()
			{
				Id = id,
				Status = status
			};
			IDal.Update(data, new string[] { "Status" });
		}

		/// <summary>
		/// 是否存在相同编码（同一父节点）
		/// </summary>
		/// <param name="id"></param>
		/// <param name="code"></param>
		/// <param name="parentId"></param>
		/// <param name="useCache"></param>
		/// <returns></returns>
		public virtual bool ExistByCode(int id, string code, int parentId, bool useCache = false)
		{
			var result = IDal.GetByCode(code, parentId, useCache);
			return result != null && result.Id != id;
		}

		/// <summary>
		/// 是否存在相同名称（同一父节点）
		/// </summary>
		/// <param name="id"></param>
		/// <param name="name"></param>
		/// <param name="parentId"></param>
		/// <param name="useCache"></param>
		/// <returns></returns>
		public virtual bool ExistByName(int id, string name, int parentId, bool useCache = false)
		{
			var result = IDal.GetByName(name, parentId, useCache);
			return result != null && result.Id != id;
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="code"></param>
		/// <param name="useCache"></param>
		/// <returns></returns>
		public virtual T GetByCode(string code, bool useCache = false)
		{
			return IDal.GetByCode(code, null, useCache);
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="code"></param>
		/// <param name="parentCode"></param>
		/// <param name="useCache"></param>
		/// <returns></returns>
		public virtual T GetByCode(string code, string parentCode, bool useCache = false)
		{
			var parent = IDal.GetByCode(parentCode, null, true);
			if (parent == null)
			{
				throw new CustomException("父节点不存在。");
			}
			return GetByCode(code, parent.Id, useCache);
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="code"></param>
		/// <param name="parentId"></param>
		/// <param name="useCache"></param>
		/// <returns></returns>
		public virtual T GetByCode(string code, int? parentId, bool useCache = false)
		{
			return IDal.GetByCode(code, parentId, useCache);
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="name"></param>
		/// <param name="useCache"></param>
		/// <returns></returns>
		public virtual T GetByName(string name, bool useCache = false)
		{
			return IDal.GetByName(name, null, useCache);
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="name"></param>
		/// <param name="parentCode"></param>
		/// <param name="useCache"></param>
		/// <returns></returns>
		public virtual T GetByName(string name, string parentCode, bool useCache = false)
		{
			var parent = IDal.GetByCode(parentCode, null, true);
			if (parent == null)
			{
				throw new CustomException("父节点不存在。");
			}
			return GetByName(name, parent.Id, useCache);
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="name"></param>
		/// <param name="parentId"></param>
		/// <param name="useCache"></param>
		/// <returns></returns>
		public virtual T GetByName(string name, int? parentId, bool useCache = false)
		{
			return IDal.GetByName(name, parentId, useCache);
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="parentCode"></param>
		/// <param name="useCache"></param>
		/// <returns></returns>
		public virtual ICollection<T> ListByParentCode(string parentCode, bool useCache = false)
		{
			var parent = IDal.GetByCode(parentCode, null, true);
			if (parent == null)
			{
				throw new CustomException("父节点不存在。");
			}
			var results = ListByParentId(parent.Id, useCache);
			var ids = results.Select(o => o.Id).ToArray();
			var children = IDal.ListByParentIds(ids, useCache);
			foreach (var result in results)
			{
				result.Children = children.Where(o => o.ParentId == result.Id).OrderBy(o => o.Sequence).ThenBy(o => o.Id).ToArray();
				result.HasChild = result.Children.Count > 0;
			}
			return results;
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="parentId"></param>
		/// <param name="useCache"></param>
		/// <returns></returns>
		public virtual ICollection<T> ListByParentId(int parentId, bool useCache = false)
		{
			var results = IDal.ListByParentId(parentId, useCache);
			var ids = results.Select(o => o.Id).ToArray();
			var existChilds = IDal.ExistChild(ids, true);
			foreach (var result in results)
			{
				result.HasChild = existChilds[result.Id];
			}
			return results;
		}

		/// <summary>
		/// 查询所有子节点
		/// </summary>
		/// <param name="parentId"></param>
		/// <param name="useCache"></param>
		/// <returns></returns>
		public virtual ICollection<T> ListChildren(int parentId, bool useCache = false)
		{
			return IDal.ListChildren(parentId, useCache);
		}

		/// <summary>
		/// 查询所有
		/// </summary>
		/// <param name="useCache"></param>
		/// <returns></returns>
		public override ICollection<T> ListAll(bool useCache = false)
		{
			var results = IDal.ListAll(useCache);
			return results.OrderBy(o => o.ParentId).ThenBy(o => o.Sequence).ThenBy(o => o.Id).ToArray();
		}
	}
}
