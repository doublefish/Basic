namespace Basic.Model.Config
{
	/// <summary>
	/// 短信
	/// </summary>
	public class Sms
	{
		/// <summary>
		/// 模板
		/// </summary>
		public class Template
		{
			/// <summary>
			/// 根节点
			/// </summary>
			public const string Root = "SmsTemplate";
			/// <summary>
			/// 注册
			/// </summary>
			public const string Signup = "Signup";
			/// <summary>
			/// 登入
			/// </summary>
			public const string SignIn = "SignIn";
			/// <summary>
			/// 修改绑定手机号码
			/// </summary>
			public const string ChangeMobile = "ChangeMobile";
			/// <summary>
			/// 修改登录密码
			/// </summary>
			public const string ChangePassword = "ChangePassword";
			/// <summary>
			/// 找回登录密码
			/// </summary>
			public const string FindPassword = "FindPassword";
			/// <summary>
			/// 修改支付密码
			/// </summary>
			public const string ChangePayPassword = "ChangePayPassword";
		}

		/// <summary>
		/// 类别
		/// </summary>
		public class Type : Adai.Standard.Models.Config
		{
			/// <summary>
			/// 注册
			/// </summary>
			public const int SignUp = 10;
			/// <summary>
			/// 登入
			/// </summary>
			public const int SignIn = 11;
			/// <summary>
			/// 修改绑定手机号码
			/// </summary>
			public const int ChangeMobile = 20;
			/// <summary>
			/// 修改登录密码
			/// </summary>
			public const int ChangePassword = 30;
			/// <summary>
			/// 找回登录密码
			/// </summary>
			public const int FindPassword = 31;
			/// <summary>
			/// 修改支付密码
			/// </summary>
			public const int ChangePayPassword = 40;

			/// <summary>
			/// 构造函数
			/// </summary>
			public Type()
			{
				Add(SignUp, "注册");
				Add(SignIn, "登入");
				Add(ChangeMobile, "修改绑定手机号码");
				Add(ChangePassword, "修改登录密码");
				Add(FindPassword, "找回登录密码");
			}

			/// <summary>
			/// 获取模板Id
			/// </summary>
			/// <param name="type"></param>
			/// <returns></returns>
			public static string GetTemplateCode(int type)
			{
				return type switch
				{
					SignUp => Template.Signup,
					SignIn => Template.SignIn,
					ChangeMobile => Template.ChangeMobile,
					ChangePassword => Template.ChangePassword,
					FindPassword => Template.FindPassword,
					ChangePayPassword => Template.ChangePayPassword,
					_ => null,
				};
			}
		}

		/// <summary>
		/// 状态
		/// </summary>
		public class Status : Adai.Standard.Models.Config
		{
			/// <summary>
			/// 成功
			/// </summary>
			public const int Succeeded = 1;
			/// <summary>
			/// 失败
			/// </summary>
			public const int Failed = 2;

			/// <summary>
			/// 构造函数
			/// </summary>
			public Status()
			{
				Add(Succeeded, "成功");
				Add(Failed, "失败");
			}
		}
	}
}
