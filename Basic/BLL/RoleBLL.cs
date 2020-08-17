using Adai.Standard.Models;
using Basic.DAL;
using Basic.Model;
using System;

namespace Basic.BLL
{
	/// <summary>
	/// RoleBLL
	/// </summary>
	public class RoleBLL : BLL<Role>
	{
		/// <summary>
		/// Dal
		/// </summary>
		readonly RoleDAL Dal;

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="loginInfo"></param>
		public RoleBLL(Token<TokenData> loginInfo = null) : base(loginInfo)
		{
			IDal = Dal = new RoleDAL();
		}

		/// <summary>
		/// 验证
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public override string Validate(Role data)
		{
			if (!string.IsNullOrEmpty(data.Code) && ExistByCode(data.Id, data.Code, true))
			{
				return "编码已存在。";
			}
			if (string.IsNullOrEmpty(data.Name))
			{
				return "名称不能为空。";
			}
			if (ExistByName(data.Id, data.Name, true))
			{
				return "名称已存在。";
			}
			if (!ValidateStatus(data.Status))
			{
				return "状态标识无效。";
			}
			data.UpdateTime = DateTime.Now;
			if (data.Id == 0)
			{
				data.Menus = "";
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
			var data = new Role()
			{
				Id = id,
				Status = status
			};
			Dal.Update(data, new string[] { "Status" });
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="id"></param>
		/// <param name="useCache"></param>
		/// <returns></returns>
		public override Role Get(int id, bool useCache = false)
		{
			var result = base.Get(id, useCache);
			if (result != null)
			{
				result.MenuIds = CommonHelper.StringToIds(result.Menus);
			}
			return result;
		}

		#region 扩展

		/// <summary>
		/// 修改菜单
		/// </summary>
		/// <param name="id"></param>
		/// <param name="menuIds"></param>
		public void UpdateMenus(int id, int[] menuIds)
		{
			if (menuIds == null || menuIds.Length == 0)
			{
				throw new CustomException("菜单不能为空。");
			}
			var data = new Role()
			{
				Id = id,
				Menus = CommonHelper.IdsToString(menuIds)
			};
			Dal.Update(data, new string[] { "Menus" });
		}

		/// <summary>
		/// 是否存在相同编码
		/// </summary>
		/// <param name="id"></param>
		/// <param name="code"></param>
		/// <param name="useCache"></param>
		/// <returns></returns>
		public bool ExistByCode(int id, string code, bool useCache = false)
		{
			var result = Dal.GetByCode(code, useCache);
			return result != null && result.Id != id;
		}

		/// <summary>
		/// 是否存在相同名称
		/// </summary>
		/// <param name="id"></param>
		/// <param name="name"></param>
		/// <param name="useCache"></param>
		/// <returns></returns>
		public bool ExistByName(int id, string name, bool useCache = false)
		{
			var result = Dal.GetByName(name, useCache);
			return result != null && result.Id != id;
		}

		#endregion
	}
}
