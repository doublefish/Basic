using Adai.Standard.Model;
using Basic.DAL;
using Basic.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Basic.BLL
{
	/// <summary>
	/// UserBLL
	/// </summary>
	public class UserBLL : BLL<User>
	{
		/// <summary>
		/// Dal
		/// </summary>
		readonly UserDAL Dal;

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="loginInfo"></param>
		public UserBLL(Token<TokenData> loginInfo = null) : base(loginInfo)
		{
			IDal = Dal = new UserDAL();
		}

		/// <summary>
		/// 验证
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public override string Validate(User data)
		{
			if (data.Id == 0)
			{
				if (string.IsNullOrEmpty(data.Username))
				{
					return "用户名不能为空。";
				}
				if (ExistByUsername(data.Id, data.Username))
				{
					return "用户名已存在。";
				}
			}
			if (!string.IsNullOrEmpty(data.Email) && ExistByEmail(data.Id, data.Email))
			{
				return "电子邮箱已存在。";
			}
			if (!string.IsNullOrEmpty(data.Mobile) && ExistByMobile(data.Id, data.Mobile))
			{
				return "手机号码已存在。";
			}
			if (!ValidateStatus(data.Status))
			{
				return "状态标识无效。";
			}
			data.Avatar = CommonHelper.ReplaceFilePath(data.Avatar);
			data.Roles = CommonHelper.IdsToString(data.RoleIds);
			data.UpdateTime = DateTime.Now;
			if (data.Id == 0)
			{
				data.SecretKey = Guid.NewGuid().ToString();
				data.CreateTime = data.UpdateTime;
			}
			return base.Validate(data);
		}

		/// <summary>
		/// 新增
		/// </summary>
		/// <param name="data"></param>
		/// <param name="password"></param>
		public void Add(User data, string password)
		{
			var error = Validate(data);
			if (!string.IsNullOrEmpty(error))
			{
				throw new CustomException(error);
			}
			Dal.Add(data, password);
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
			var data = new User()
			{
				Id = id,
				Status = status
			};
			Dal.Update(data, new string[] { "Status" });
		}

		/// <summary>
		/// 关联数据
		/// </summary>
		/// <param name="list"></param>
		public override void List(ICollection<User> list)
		{
			var roleIds = new List<int>();
			foreach (var data in list)
			{
				data.RoleIds = CommonHelper.StringToIds(data.Roles);
				roleIds.AddRange(data.RoleIds);
				data.Avatar = CommonHelper.RestoreFilePath(data.Avatar);
			}
			var roles = new RoleBLL().ListByPks(roleIds.Distinct().ToArray(), true);
			foreach (var data in list)
			{
				data.RoleNames = roles.Where(o => data.RoleIds.Contains(o.Id)).Select(o => o.Name).ToArray();
			}
		}

		#region 扩展

		/// <summary>
		/// 修改
		/// </summary>
		/// <param name="id"></param>
		/// <param name="data"></param>
		public void Modify(int id, User data)
		{
			data.Id = id;
			if (string.IsNullOrEmpty(data.Nickname))
			{
				throw new CustomException("昵称不能为空。");
			}
			data.UpdateTime = DateTime.Now;
			Dal.Update(data, new string[] { "Nickname", "UpdateTime" });
		}

		/// <summary>
		/// 修改昵称
		/// </summary>
		/// <param name="id"></param>
		/// <param name="nickname"></param>
		public void UpdateNickname(int id, string nickname)
		{
			if (string.IsNullOrEmpty(nickname))
			{
				throw new CustomException("昵称不能为空。");
			}
			var data = new User()
			{
				Id = id,
				Nickname = nickname,
				UpdateTime = DateTime.Now
			};
			Dal.Update(data, new string[] { "Nickname", "UpdateTime" });
		}

		/// <summary>
		/// 修改头像
		/// </summary>
		/// <param name="id"></param>
		/// <param name="avatar"></param>
		public void UpdateAvatar(int id, string avatar)
		{
			if (string.IsNullOrEmpty(avatar))
			{
				throw new CustomException("头像地址不能为空。");
			}
			var data = new User()
			{
				Id = id,
				Avatar = CommonHelper.ReplaceFilePath(avatar),
				UpdateTime = DateTime.Now
			};
			Dal.Update(data, new string[] { "Avatar", "UpdateTime" });
		}

		/// <summary>
		/// 是否存在相同用户名
		/// </summary>
		/// <param name="id"></param>
		/// <param name="username"></param>
		/// <returns></returns>
		public bool ExistByUsername(int id, string username, bool useCache = false)
		{
			var result = Dal.GetByUsername(username, useCache);
			return result != null && result.Id != id;
		}

		/// <summary>
		/// 是否存在相同邮箱
		/// </summary>
		/// <param name="id"></param>
		/// <param name="email"></param>
		/// <returns></returns>
		public bool ExistByEmail(int id, string email, bool useCache = false)
		{
			var result = Dal.GetByEmail(email, useCache);
			return result != null && result.Id != id;
		}

		/// <summary>
		/// 是否存在相同手机号码
		/// </summary>
		/// <param name="id"></param>
		/// <param name="mobile"></param>
		/// <returns></returns>
		public bool ExistByMobile(int id, string mobile, bool useCache = false)
		{
			var result = Dal.GetByMobile(mobile, useCache);
			return result != null && result.Id != id;
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="username"></param>
		/// <param name="useCache"></param>
		/// <returns></returns>
		public User GetByUsername(string username, bool useCache = false)
		{
			return Dal.GetByUsername(username, useCache);
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="mail"></param>
		/// <param name="useCache"></param>
		/// <returns></returns>
		public User GetByEmail(string mail, bool useCache = false)
		{
			return Dal.GetByEmail(mail, useCache);
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="mobile"></param>
		/// <param name="useCache"></param>
		/// <returns></returns>
		public User GetByMobile(string mobile, bool useCache = false)
		{
			return Dal.GetByMobile(mobile, useCache);
		}

		#endregion

		#region 业务

		/// <summary>
		/// 登录
		/// </summary>
		/// <param name="username"></param>
		/// <param name="password"></param>
		/// <returns></returns>
		internal TokenData SignIn(string username, string password)
		{
			var result = CommonHelper.IsMobile(username) ? GetByMobile(username, true) : GetByUsername(username, true);
			if (result == null || !new UserPasswordBLL().Verify(result.Id, Model.Config.Password.Type.Login, result.SecretKey, password))
			{
				throw new CustomException("用户名或密码错误。");
			}
			if (!result.IsEnabled)
			{
				throw new CustomException("用户状态不可用。");
			}
			return new TokenData()
			{
				Id = result.Id,
				Username = result.Username,
				Nickname = result.Nickname,
				Avatar = result.Avatar,
				FirstName = result.FirstName,
				LastName = result.LastName,
				Email = result.Email,
				Mobile = result.Mobile,
				RoleIds = CommonHelper.StringToIds(result.Roles)
			};
		}

		/// <summary>
		/// 查询权限
		/// </summary>
		/// <param name="menuIds"></param>
		/// <returns></returns>
		public ICollection<Menu> ListMenu(int id)
		{
			var user = Get(id, true);
			var roles = new RoleBLL().ListByPks(user.RoleIds, true);
			var roleIds = roles.Where(o => o.IsEnabled == true).Select(o => o.Id).ToArray();
			var menuIds = new List<int>();
			foreach (var role in roles)
			{
				menuIds.AddRange(CommonHelper.StringToIds(role.Menus));
			}
			menuIds = menuIds.Distinct().ToList();
			if (menuIds == null || menuIds.Count == 0)
			{
				return null;
			}
			var menus = new MenuBLL().ListByPks(menuIds, true);
			return menus.Where(o => o.IsEnabled == true).ToArray();
		}

		/// <summary>
		/// 查询权限
		/// </summary>
		/// <param name="menuIds"></param>
		/// <returns></returns>
		public ICollection<Menu> ListRight(int id)
		{
			var menus = ListMenu(id);
			return ModelHelper.Recursive(menus, Model.Config.Menu.Root);
		}

		/// <summary>
		/// 验证权限
		/// </summary>
		/// <param name="id"></param>
		/// <param name="url"></param>
		/// <returns></returns>
		public bool VerifyRight(int id, string url)
		{
			var menus = ListMenu(id);
			if (menus == null)
			{
				return false;
			}
			return menus.Any(o => o.ApiUrl != null && o.ApiUrl.ToLower() == url.ToLower());
		}

		#endregion
	}
}
