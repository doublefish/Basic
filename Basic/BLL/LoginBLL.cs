using Adai.Standard;
using Adai.Standard.Models;

namespace Basic.BLL
{
	/// <summary>
	/// LoginBLL
	/// </summary>
	public class LoginBLL
	{
		/// <summary>
		/// 登录
		/// </summary>
		/// <param name="type">用户类型：User/AgentUser/Account</param>
		/// <param name="username"></param>
		/// <param name="password"></param>
		/// <param name="platform">平台：Web/Wap/PC/App</param>
		/// <returns></returns>
		public static Token<TokenData> SignIn(string type, string username, string password, int platform)
		{
			if (string.IsNullOrEmpty(username))
			{
				throw new CustomException("用户名不能为空。");
			}
			if (string.IsNullOrEmpty(password))
			{
				throw new CustomException("密码不能为空。");
			}
			if (!ConfigIntHelper<Model.Config.Platform>.ContainsKey(platform))
			{
				throw new CustomException("平台标识无效。");
			}

			var data = type switch
			{
				"Account" => new AccountBLL().SignIn(username, password),
				"AgentUser" => new AgentUserBLL().SignIn(username, password),
				"User" => new UserBLL().SignIn(username, password),
				_ => throw new CustomException("用户类型标识无效。"),
			};

			data.Type = type;
			data.Platform = ConfigIntHelper<Model.Config.Platform>.GetValue(platform);
			return TokenHelper.Set(data);
		}

		/// <summary>
		/// 获取登录信息
		/// </summary>
		/// <param name="token"></param>
		/// <returns></returns>
		public static Token<TokenData> GetLogin(string token)
		{
			return TokenHelper.Get<TokenData>(token);
		}

		/// <summary>
		/// 验证登录信息
		/// </summary>
		/// <param name="token"></param>
		/// <param name="login"></param>
		/// <returns></returns>
		public static bool VerifyLogin(string token, out Token<TokenData> login)
		{
			return TokenHelper.Verify(token, out login);
		}
	}
}
