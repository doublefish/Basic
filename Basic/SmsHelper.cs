using Adai.Base.Ext;
using Adai.Standard;
using Adai.Standard.Ext;
using Adai.Standard.Model;
using Basic.Model;
using System;
using System.Collections.Generic;

namespace Basic
{
	/// <summary>
	/// SmsHelper
	/// </summary>
	internal static class SmsHelper
	{
		/// <summary>
		/// configuration
		/// </summary>
		static SmsConfiguration configuration;

		/// <summary>
		/// Configuration
		/// </summary>
		public static SmsConfiguration Configuration
		{
			get
			{
				if (configuration == null)
				{
					var config = JsonConfigHelper.Get("Sms");
					if (config != null)
					{
						configuration = new SmsConfiguration()
						{
							Url = config.Value<string>("Url"),
							Account = config.Value<string>("Account"),
							Password = config.Value<string>("Password"),
							Signature = config.Value<string>("Signature"),
							Expiry = config.Value<TimeSpan>("Expiry")
						};
					}
					else
					{
						throw new Exception("Not configured.");
					}
				}
				return configuration;
			}
		}

		/// <summary>
		/// 发送信息
		/// </summary>
		/// <param name="mobile">手机号码</param>
		/// <param name="message">信息</param>
		/// <returns></returns>
		public static ReturnResult Send(string mobile, string message)
		{
			if (string.IsNullOrEmpty(mobile))
			{
				throw new ArgumentNullException("手机号码不能为空");
			}
			if (string.IsNullOrEmpty(message))
			{
				throw new ArgumentNullException("信息不能为空");
			}
			message = string.Format("{0}【{1}】", message, Configuration.Signature);

			//请求参数
			var parameters = new Dictionary<string, string>()
			{
				{ "action", "send" },
				{ "userid", string.Empty },
				{ "account", Configuration.Account },
				{ "mobile", mobile },
				{ "content", message },
				{ "sendTime", DateTime.Now.ToString("yyyyMMddHHmmss") }
			};

			if (string.IsNullOrEmpty(Configuration.Url))
			{
				return new ReturnResult()
				{
					Status = "success"
				};
			}

			HttpResponse postResult;
			try
			{
				//发送请求
				postResult = HttpHelper.SendPost(Configuration.Url, parameters);
			}
			catch (Exception ex)
			{
				throw new Exception(string.Format("短信发送服务请求失败：{0}", ex.Message));
			}
			//接收结果
			var xml = new System.Xml.XmlDocument();
			try
			{
				xml.Load(postResult.Content);
			}
			catch (Exception ex)
			{
				throw new Exception(string.Format("短信发送服务请求结果解析失败：{0}", ex.Message));
			}
			if (xml == null)
			{
				throw new Exception("短信发送服务请求结果解析失败");
			}

			return new ReturnResult()
			{
				Message = xml.GetElementsByTagName("message").Item(0).InnerText,
				SucessCount = xml.GetElementsByTagName("successCounts").Item(0).InnerText.ToInt32(0),
				TaskId = xml.GetElementsByTagName("taskID").Item(0).InnerText,
				RemainPoint = xml.GetElementsByTagName("remainPoint").Item(0).InnerText,
				Status = xml.GetElementsByTagName("returnstatus").Item(0).InnerText.Trim()
			};
		}

		/// <summary>
		/// 返回结果
		/// </summary>
		public class ReturnResult
		{
			/// <summary>
			/// 消息
			/// </summary>
			public string Message { get; set; }
			/// <summary>
			/// 成功条数
			/// </summary>
			public int SucessCount { get; set; }
			/// <summary>
			/// 任务Id
			/// </summary>
			public string TaskId { get; set; }
			/// <summary>
			/// 残留点？
			/// </summary>
			public string RemainPoint { get; set; }
			/// <summary>
			/// 消息
			/// </summary>
			public string Status { get; set; }
			/// <summary>
			/// 消息
			/// </summary>
			public bool IsSucceed => Status.Equals("success");
		}
	}
}
