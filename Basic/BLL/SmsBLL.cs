using Adai.Standard;
using Adai.Standard.Models;
using Basic.DAL;
using Basic.Model;
using System;
using System.Collections.Generic;

namespace Basic.BLL
{
	/// <summary>
	/// SmsBLL
	/// </summary>
	public class SmsBLL : BLL<Sms>
	{
		/// <summary>
		/// Dal
		/// </summary>
		readonly SmsDAL Dal;

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="loginInfo"></param>
		public SmsBLL(Token<TokenData> loginInfo = null) : base(loginInfo)
		{
			IDal = Dal = new SmsDAL();
		}

		/// <summary>
		/// 验证
		/// </summary>
		/// <param name="data"></param>
		/// <param name="error"></param>
		/// <returns></returns>
		public override string Validate(Sms data)
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
			var data = new Sms()
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
		public Sms Get(string mobile, int type)
		{
			return Dal.Get(mobile, type);
		}

		#endregion

		#region 业务

		/// <summary>
		/// 发送短信验证码
		/// </summary>
		/// <param name="mobile"></param>
		/// <param name="type"></param>
		public void SendCode(string mobile, int type)
		{
			if (ConfigIntHelper<Model.Config.Sms.Type>.ContainsKey(type))
			{
				throw new CustomException("类型标识无效。");
			}

			var result = Dal.Get(mobile, type);
			if (result != null && result.CreateTime.Add(SmsHelper.Configuration.Expiry) > DateTime.Now)
			{
				throw new CustomException("短信发送过于频繁。");
			}

			//创建6位数字验证码
			var checkCode = StringHelper.GenerateRandomDigital(6);
			var parameters = new Dictionary<string, string>() { { "CheckCode", checkCode } };

			Send(mobile, type, parameters);
		}

		/// <summary>
		/// 发送短信
		/// </summary>
		/// <param name="mobile"></param>
		/// <param name="type"></param>
		/// <param name="parameters"></param>
		public void Send(string mobile, int type, IDictionary<string, string> parameters)
		{
			if (!ConfigIntHelper<Model.Config.Sms.Type>.ContainsKey(type))
			{
				throw new CustomException("类型标识无效。");
			}

			//读取模板
			var templateCode = Model.Config.Sms.Type.GetTemplateCode(type);
			var dictBll = new DictBLL();
			var templateRoot = dictBll.GetByCode(Model.Config.Sms.Template.Root, true);
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
			var data = new Sms()
			{
				Mobile = mobile,
				Type = type,
				Content = content,
				CheckCode = checkCode ?? "",
				CreateTime = DateTime.Now
			};

			try
			{
				var result = SmsHelper.Send(data.Mobile, data.Content);
				data.Status = result.IsSucceed ? Model.Config.Sms.Status.Succeeded : Model.Config.Sms.Status.Failed;
				Log4netHelper.InfoFormat("【短信发送成功】=>data={0}，发送结果=>{1}", JsonHelper.SerializeObject(data), JsonHelper.SerializeObject(result));
			}
			catch (Exception ex)
			{
				data.Status = Model.Config.Sms.Status.Failed;
				Log4netHelper.Error(string.Format("【短信发送报错】=>data={0}", JsonHelper.SerializeObject(data)), ex);
				throw;
			}
			finally
			{
				Dal.Add(data);
			}
		}

		/// <summary>
		/// 检查短信验证码
		/// </summary>
		/// <param name="mobile">手机号码</param>
		/// <param name="type">类型</param>
		/// <param name="code">验证码</param>
		/// <returns></returns>
		public string VerifyCode(string mobile, int type, string code)
		{
			var result = Dal.Get(mobile, type);
			if (result == null)
			{
				//return "短信验证码错误";
				return null;
			}
			if (string.Compare(code, result.CheckCode, StringComparison.OrdinalIgnoreCase) != 0)
			{
				return "短信验证码错误";
			}
			if (result.CreateTime.Add(SmsHelper.Configuration.Expiry) < DateTime.Now)
			{
				return "短信验证码失效";
			}
			return null;
		}

		#endregion
	}
}
