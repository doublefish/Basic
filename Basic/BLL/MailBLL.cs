using Adai.Standard;
using Adai.Standard.Models;
using Basic.DAL;
using Basic.Model;
using System;
using System.Collections.Generic;

namespace Basic.BLL
{
	/// <summary>
	/// MailBLL
	/// </summary>
	public class MailBLL : BLL<Mail>
	{
		/// <summary>
		/// Dal
		/// </summary>
		readonly MailDAL Dal;

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="loginInfo"></param>
		public MailBLL(Token<TokenData> loginInfo = null) : base(loginInfo)
		{
			IDal = Dal = new MailDAL();
		}

		/// <summary>
		/// 验证
		/// </summary>
		/// <param name="data"></param>
		/// <param name="error"></param>
		/// <returns></returns>
		public override string Validate(Mail data)
		{
			if (!ValidateStatus(data.Status))
			{
				return "状态标识无效。";
			}
			data.UpdateTime = DateTime.Now;
			if (data.Id == 0)
			{
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
			var data = new Mail()
			{
				Id = id,
				Status = status
			};
			Dal.Update(data, new string[] { "Status" });
		}

		#region 扩展

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="mobile"></param>
		/// <param name="type"></param>
		/// <returns></returns>
		public Mail Get(string mobile, int type)
		{
			return Dal.Get(mobile, type);
		}

		#endregion

		#region 业务

		/// <summary>
		/// 发送邮件验证码
		/// </summary>
		/// <param name="email"></param>
		/// <param name="type"></param>
		public void SendCode(string email, int type)
		{
			if (ConfigIntHelper<Model.Config.Mail.Type>.ContainsKey(type))
			{
				throw new CustomException("类型标识无效。");
			}

			var result = Dal.Get(email, type);
			if (result != null && result.CreateTime.Add(MailHelper.Expiry) > DateTime.Now)
			{
				throw new CustomException("邮件发送过于频繁。");
			}

			//创建6位数字验证码
			var checkCode = StringHelper.GenerateRandomDigital(6);
			var parameters = new Dictionary<string, string>() { { "CheckCode", checkCode } };

			Send(email, type, parameters);
		}

		/// <summary>
		/// 发送邮件
		/// </summary>
		/// <param name="emial"></param>
		/// <param name="type"></param>
		/// <param name="parameters"></param>
		public void Send(string emial, int type, IDictionary<string, string> parameters)
		{
			if (!ConfigIntHelper<Model.Config.Mail.Type>.ContainsKey(type))
			{
				throw new CustomException("类型标识无效。");
			}

			//读取模板
			var templateCode = Model.Config.Mail.Type.GetTemplateCode(type);
			var dictBll = new DictBLL();
			var templateRoot = dictBll.GetByCode(Model.Config.Mail.Template.Root, true);
			var template = dictBll.GetByCode(templateCode, templateRoot.Id, true);
			if (template == null || string.IsNullOrEmpty(template.Value))
			{
				throw new CustomException("模板不存在。");
			}

			var content = template.Value;
			foreach (var parameter in parameters)
			{
				content = content.Replace("{" + parameter.Key + "}", parameter.Value);
			}

			//获取验证码
			parameters.TryGetValue("CheckCode", out var checkCode);

			//发送记录
			var data = new Mail()
			{
				Email = emial,
				Type = type,
				Content = content,
				CheckCode = checkCode ?? "",
				CreateTime = DateTime.Now
			};

			try
			{
				MailHelper.Send(data.Email, "", data.Content);
				data.Status = Model.Config.Mail.Status.Succeeded;
				Log4netHelper.InfoFormat("【邮件发送成功】=>data={0}", JsonHelper.SerializeObject(data));
			}
			catch (Exception ex)
			{
				data.Status = Model.Config.Mail.Status.Failed;
				Log4netHelper.Error(string.Format("【邮件发送报错】=>data={0}", JsonHelper.SerializeObject(data)), ex);
				throw;
			}
			finally
			{
				Dal.Add(data);
			}
		}

		/// <summary>
		/// 检查邮件验证码
		/// </summary>
		/// <param name="mobile">电子邮箱</param>
		/// <param name="type">类型</param>
		/// <param name="code">验证码</param>
		/// <returns></returns>
		public string VerifyCode(string mobile, int type, string code)
		{
			var result = Dal.Get(mobile, type);
			if (result == null)
			{
				//return "邮件验证码错误";
				return null;
			}
			if (string.Compare(code, result.CheckCode, StringComparison.OrdinalIgnoreCase) != 0)
			{
				return "邮件验证码错误";
			}
			if (result.CreateTime.Add(MailHelper.Expiry) < DateTime.Now)
			{
				return "邮件验证码失效";
			}
			return null;
		}

		#endregion
	}
}
