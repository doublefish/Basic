using Adai.Standard.Model;
using Basic.DAL;
using Basic.Model;
using System;

namespace Basic.BLL
{
	/// <summary>
	/// AccountPasswordBLL
	/// </summary>
	public class AccountPasswordBLL : BLL<AccountPassword>
	{
		/// <summary>
		/// Dal
		/// </summary>
		readonly AccountPasswordDAL Dal;

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="loginInfo"></param>
		public AccountPasswordBLL(Token<TokenData> loginInfo = null) : base(loginInfo)
		{
			IDal = Dal = new AccountPasswordDAL();
		}

		#region 扩展

		/// <summary>
		/// 新增
		/// </summary>
		/// <param name="accountId">用户Id</param>
		/// <param name="type">类型</param>
		/// <param name="secretKey">密钥</param>
		/// <param name="password">密码</param>
		internal void Add(int accountId, int type, string secretKey, string password)
		{
			var data = new AccountPassword()
			{
				AccountId = accountId,
				Type = type,
				Password = CommonHelper.GeneratePassword(secretKey, password),
				CreateTime = DateTime.Now
			};
			Dal.Add(data);
		}

		/// <summary>
		/// 是否存在
		/// </summary>
		/// <param name="accountId">用户Id</param>
		/// <param name="type">类型</param>
		/// <returns></returns>
		public bool Exist(int accountId, int type)
		{
			var result = Dal.Get(accountId, type);
			return result != null;
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="accountId">用户Id</param>
		/// <param name="type">类型</param>
		/// <returns></returns>
		public AccountPassword Get(int accountId, int type)
		{
			return Dal.Get(accountId, type);
		}

		/// <summary>
		/// 验证密码
		/// </summary>
		/// <param name="accountId">用户Id</param>
		/// <param name="type">类型</param>
		/// <param name="secretKey">密钥</param>
		/// <param name="password">密码</param>
		/// <returns></returns>
		internal bool Verify(int accountId, int type, string secretKey, string password)
		{
			var result = Dal.Get(accountId, type);
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
		/// <param name="smsCode">短信验证码</param>
		public void ChangeLogin(string newPassword, string smsCode)
		{
			var account = new AccountBLL().Get(LoginInfo.Id, true);
			if (string.IsNullOrEmpty(smsCode))
			{
				throw new CustomException("短信验证码不能为空。");
			}
			//验证短信验证码
			var error = new AccountSmsBLL().VerifyCode(account.Mobile, Model.Config.Sms.Type.ChangePassword, smsCode);
			if (!string.IsNullOrEmpty(error))
			{
				throw new CustomException(error);
			}
			Add(account.Id, Model.Config.Password.Type.Login, account.SecretKey, newPassword);
		}

		/// <summary>
		/// 找回登录密码
		/// </summary>
		/// <param name="mobile">绑定手机</param>
		/// <param name="newPassword">新密码</param>
		/// <param name="smsCode">短信验证码</param>
		public void FindLogin(string mobile, string newPassword, string smsCode)
		{
			var account = new AccountBLL().GetByMobile(mobile, true);
			if (account == null)
			{
				throw new CustomException("手机号码未注册。");
			}
			if (string.IsNullOrEmpty(smsCode))
			{
				throw new CustomException("短信验证码不能为空。");
			}
			//验证短信验证码
			var error = new AccountSmsBLL().VerifyCode(account.Mobile, Model.Config.Sms.Type.FindPassword, smsCode);
			if (!string.IsNullOrEmpty(error))
			{
				throw new CustomException(error);
			}
			Add(account.Id, Model.Config.Password.Type.Login, account.SecretKey, newPassword);
		}

		/// <summary>
		/// 重置登录密码
		/// </summary>
		/// <param name="accountId">用户Id</param>
		/// <param name="newPassword">新密码</param>
		public void ResetLogin(int accountId, string newPassword)
		{
			if (string.IsNullOrEmpty(newPassword))
			{
				newPassword = Model.Config.Password.Default;
			}
			var account = new AccountBLL().Get(accountId, true);
			if (account == null)
			{
				throw new CustomException("用户不存在。");
			}
			Add(account.Id, Model.Config.Password.Type.Login, account.SecretKey, newPassword);
		}

		#endregion
	}
}
