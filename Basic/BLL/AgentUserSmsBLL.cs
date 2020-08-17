using Adai.Standard;
using Adai.Standard.Models;
using Basic.DAL;
using Basic.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Basic.BLL
{
	/// <summary>
	/// AgentUserSmsBLL
	/// </summary>
	public class AgentUserSmsBLL : BLL<AgentUserSms>
	{
		/// <summary>
		/// Dal
		/// </summary>
		readonly AgentUserSmsDAL Dal;

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="loginInfo"></param>
		public AgentUserSmsBLL(Token<TokenData> loginInfo = null) : base(loginInfo)
		{
			IDal = Dal = new AgentUserSmsDAL();
			if (Dal == null)
			{
				//为了编辑器不显示警告信息
			}
		}

		/// <summary>
		/// 验证
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public override string Validate(AgentUserSms data)
		{
			if (data.Id == 0)
			{
				var agent = new AgentBLL().Get(data.AgentId, true);
				if (agent == null)
				{
					return "代理商不存在。";
				}
				var agentUser = new AgentUserBLL().Get(data.AgentUserId, true);
				if (agentUser == null)
				{
					return "代理商用户不存在。";
				}
			}
			if (string.IsNullOrEmpty(data.Mobile))
			{
				return "手机号码不能为空。";
			}
			if (!ConfigIntHelper<Model.Config.Sms.Type>.ContainsKey(data.Type))
			{
				return "类型标识无效。";
			}
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
			var data = new AgentUserSms()
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
		public override void List(ICollection<AgentUserSms> list)
		{
			var agentIds = new HashSet<int>();
			var agentUserIds = new HashSet<int>();
			foreach (var data in list)
			{
				agentIds.Add(data.AgentId);
				agentUserIds.Add(data.AgentUserId);
			}
			var agents = new AgentBLL().ListByPks(agentIds, true);
			var agentUsers = new AgentUserBLL().ListByPks(agentUserIds, true);
			foreach (var data in list)
			{
				data.Agent = agents.Where(o => o.Id == data.AgentId).FirstOrDefault();
				data.AgentUser = agentUsers.Where(o => o.Id == data.AgentUserId).FirstOrDefault();
			}
		}

		/// <summary>
		/// 验证权限
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public override bool VerifyRight(AgentUserSms data)
		{
			return AgentBLL.CheckRight(LoginInfo, data.AgentId, data.AgentUserId);
		}

		#region 扩展

		#endregion

		#region 业务

		/// <summary>
		/// 发送短信验证码
		/// </summary>
		/// <param name="mobile"></param>
		/// <param name="type"></param>
		public void SendCode(string mobile, int type)
		{
			SendCode(0, mobile, type);
		}

		/// <summary>
		/// 发送短信验证码
		/// </summary>
		/// <param name="agentUserId">绑定手机号码、修改登录密码、修改支付密码必传</param>
		/// <param name="mobile"></param>
		/// <param name="type"></param>
		public void SendCode(int agentUserId, string mobile, int type)
		{
			if (type == Model.Config.Sms.Type.SignUp)
			{
				if (new AgentUserBLL().ExistByMobile(0, mobile, true))
				{
					throw new CustomException("手机号码已注册。");
				}
			}
			else if (type == Model.Config.Sms.Type.SignIn)
			{
				if (!new AgentUserBLL().ExistByMobile(0, mobile, true))
				{
					throw new CustomException("手机号码未注册。");
				}
			}
			else if (type == Model.Config.Sms.Type.ChangeMobile
				|| type == Model.Config.Sms.Type.ChangePassword
				|| type == Model.Config.Sms.Type.ChangePayPassword)
			{
				if (agentUserId == 0)
				{
					throw new CustomException("用户不存在。");
				}
				var agentUser = new AgentUserBLL().Get(agentUserId, true);
				if (mobile != agentUser.Mobile)
				{
					throw new CustomException("和绑定手机号码不一致");
				}
			}
			else if (type == Model.Config.Sms.Type.FindPassword)
			{
				if (!new AgentUserBLL().ExistByMobile(0, mobile, true))
				{
					throw new CustomException("手机号码未注册。");
				}
			}
			else
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

			Send(agentUserId, mobile, type, parameters);
		}

		/// <summary>
		/// 发送短信
		/// </summary>
		/// <param name="agentUserId"></param>
		/// <param name="mobile"></param>
		/// <param name="type"></param>
		/// <param name="parameters"></param>
		public void Send(int agentUserId, string mobile, int type, Dictionary<string, string> parameters)
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
			var agentUser = new AgentUserBLL().Get(agentUserId, true);
			var data = new AgentUserSms()
			{
				AgentId = agentUser.AgentId,
				AgentUserId = agentUserId,
				Type = type,
				Mobile = mobile,
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
				throw ex;
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
