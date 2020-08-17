using Adai.Standard.Models;
using Basic.DAL;
using Basic.Model;
using System;

namespace Basic.BLL
{
	/// <summary>
	/// UserPasswordBLL
	/// </summary>
	public class UserPasswordBLL : BLL<UserPassword>
	{
		/// <summary>
		/// Dal
		/// </summary>
		readonly UserPasswordDAL Dal;

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="loginInfo"></param>
		public UserPasswordBLL(Token<TokenData> loginInfo = null) : base(loginInfo)
		{
			IDal = Dal = new UserPasswordDAL();
		}

		#region 扩展

		/// <summary>
		/// 新增
		/// </summary>
		/// <param name="userId">用户Id</param>
		/// <param name="type">类型</param>
		/// <param name="secretKey">密钥</param>
		/// <param name="password">密码</param>
		internal void Add(int userId, int type, string secretKey, string password)
		{
			var data = new UserPassword()
			{
				UserId = userId,
				Type = type,
				Password = CommonHelper.GeneratePassword(secretKey, password),
				CreateTime = DateTime.Now
			};
			Dal.Add(data);
		}

		/// <summary>
		/// 是否存在
		/// </summary>
		/// <param name="userId">用户Id</param>
		/// <param name="type">类型</param>
		/// <returns></returns>
		public bool Exist(int userId, int type)
		{
			var result = Dal.Get(userId, type);
			return result != null;
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="userId">用户Id</param>
		/// <param name="type">类型</param>
		/// <returns></returns>
		public UserPassword Get(int userId, int type)
		{
			return Dal.Get(userId, type);
		}

		/// <summary>
		/// 验证密码
		/// </summary>
		/// <param name="userId">用户Id</param>
		/// <param name="type">类型</param>
		/// <param name="secretKey">密钥</param>
		/// <param name="password">密码</param>
		/// <returns></returns>
		internal bool Verify(int userId, int type, string secretKey, string password)
		{
			var result = Dal.Get(userId, type);
			if (result == null)
			{
				return false;
			}
			return CommonHelper.VerifyPassword(secretKey, password, result.Password);
		}

		#endregion

		#region 业务

		/// <summary>
		/// 修改登录密码
		/// </summary>
		/// <param name="newPassword">新密码</param>
		/// <param name="oldPassword">旧密码</param>
		public void ChangeLogin(string newPassword, string oldPassword)
		{
			var user = new UserBLL().Get(LoginInfo.Id, true);
			if (!Verify(user.Id, Model.Config.Password.Type.Login, user.SecretKey, oldPassword))
			{
				throw new CustomException("旧密码错误。");
			}
			Add(user.Id, Model.Config.Password.Type.Login, user.SecretKey, newPassword);
		}

		/// <summary>
		/// 找回登录密码
		/// </summary>
		/// <param name="mobile">绑定手机</param>
		/// <param name="newPassword">新密码</param>
		/// <param name="smsCode">短信验证码</param>
		public void FindLogin(string mobile, string newPassword, string smsCode)
		{
			var user = new UserBLL().GetByMobile(mobile, true);
			if (user == null)
			{
				throw new CustomException("手机号码未注册。");
			}
			if (string.IsNullOrEmpty(smsCode))
			{
				throw new CustomException("短信验证码不能为空。");
			}
			//验证短信验证码
			//if (!new UserSmsBLL().VerifyCode(user.Mobile,Config.Sms.Type.FindPassword, user.Mobile, smsCode, out var error))
			//{
			//	throw new CustomException(error);
			//}
			Add(user.Id, Model.Config.Password.Type.Login, user.SecretKey, newPassword);
		}

		/// <summary>
		/// 重置登录密码
		/// </summary>
		/// <param name="userId">用户Id</param>
		/// <param name="newPassword">新密码</param>
		public void ResetLogin(int userId, string newPassword)
		{
			if (string.IsNullOrEmpty(newPassword))
			{
				newPassword = Model.Config.Password.Default;
			}
			var user = new UserBLL().Get(userId, true);
			if (user == null)
			{
				throw new CustomException("用户不存在。");
			}
			Add(user.Id, Model.Config.Password.Type.Login, user.SecretKey, newPassword);
		}

		#endregion
	}
}
