using Adai.Standard.Models;
using Basic.DAL;
using Basic.Model;
using System;

namespace Basic.BLL
{
	/// <summary>
	/// AgentUserPasswordBLL
	/// </summary>
	public class AgentUserPasswordBLL : BLL<AgentUserPassword>
	{
		/// <summary>
		/// Dal
		/// </summary>
		readonly AgentUserPasswordDAL Dal;

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="loginInfo"></param>
		public AgentUserPasswordBLL(Token<TokenData> loginInfo = null) : base(loginInfo)
		{
			IDal = Dal = new AgentUserPasswordDAL();
		}

		#region 扩展

		/// <summary>
		/// 新增
		/// </summary>
		/// <param name="agentUserId">用户Id</param>
		/// <param name="type">类型</param>
		/// <param name="secretKey">密钥</param>
		/// <param name="password">密码</param>
		internal void Add(int agentUserId, int type, string secretKey, string password)
		{
			var data = new AgentUserPassword()
			{
				AgentUserId = agentUserId,
				Type = type,
				Password = CommonHelper.GeneratePassword(secretKey, password),
				CreateTime = DateTime.Now
			};
			Dal.Add(data);
		}

		/// <summary>
		/// 是否存在
		/// </summary>
		/// <param name="agentUserId">用户Id</param>
		/// <param name="type">类型</param>
		/// <returns></returns>
		public bool Exist(int agentUserId, int type)
		{
			var result = Dal.Get(agentUserId, type);
			return result != null;
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="agentUserId">用户Id</param>
		/// <param name="type">类型</param>
		/// <returns></returns>
		public AgentUserPassword Get(int agentUserId, int type)
		{
			return Dal.Get(agentUserId, type);
		}

		/// <summary>
		/// 验证密码
		/// </summary>
		/// <param name="agentUserId">用户Id</param>
		/// <param name="type">类型</param>
		/// <param name="secretKey">密钥</param>
		/// <param name="password">密码</param>
		/// <returns></returns>
		internal bool Verify(int agentUserId, int type, string secretKey, string password)
		{
			var result = Dal.Get(agentUserId, type);
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
			var agentUser = new AgentUserBLL().Get(LoginInfo.Id, true);
			if (!Verify(agentUser.Id, Model.Config.Password.Type.Login, agentUser.SecretKey, oldPassword))
			{
				throw new CustomException("旧密码错误。");
			}
			Add(agentUser.Id, Model.Config.Password.Type.Login, agentUser.SecretKey, newPassword);
		}

		/// <summary>
		/// 找回登录密码
		/// </summary>
		/// <param name="mobile">绑定手机</param>
		/// <param name="newPassword">新密码</param>
		/// <param name="smsCode">短信验证码</param>
		public void FindLogin(string mobile, string newPassword, string smsCode)
		{
			var agentUser = new AgentUserBLL().GetByMobile(mobile, true);
			if (agentUser == null)
			{
				throw new CustomException("手机号码未注册。");
			}
			if (string.IsNullOrEmpty(smsCode))
			{
				throw new CustomException("短信验证码不能为空。");
			}
			//验证短信验证码
			var error = new AgentUserSmsBLL().VerifyCode(agentUser.Mobile, Model.Config.Sms.Type.FindPassword, smsCode);
			if (!string.IsNullOrEmpty(error))
			{
				throw new CustomException(error);
			}
			Add(agentUser.Id, Model.Config.Password.Type.Login, agentUser.SecretKey, newPassword);
		}

		/// <summary>
		/// 重置登录密码
		/// </summary>
		/// <param name="agentUserId">用户Id</param>
		/// <param name="newPassword">新密码</param>
		public void ResetLogin(int agentUserId, string newPassword)
		{
			if (string.IsNullOrEmpty(newPassword))
			{
				newPassword = Model.Config.Password.Default;
			}
			var agentUser = new AgentUserBLL().Get(agentUserId, true);
			if (agentUser == null)
			{
				throw new CustomException("用户不存在。");
			}
			Add(agentUser.Id, Model.Config.Password.Type.Login, agentUser.SecretKey, newPassword);
		}

		#endregion
	}
}
