﻿using Adai.Standard;
using Adai.Standard.Ext;
using Adai.Standard.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi
{
	/// <summary>
	/// ApiAuthorizeAttribute
	/// </summary>
	public class ApiAuthorizeAttribute : ActionFilterAttribute
	{
		/// <summary>
		/// 访问频率
		/// </summary>
		public double Frequency { get; set; }
		/// <summary>
		/// 是否验证图片验证码
		/// </summary>
		public bool VerifyCode { get; set; }
		/// <summary>
		/// 是否验证Excel导出
		/// </summary>
		public bool VerifyExcelExport { get; set; }
		/// <summary>
		/// 是否验证Token
		/// </summary>
		public bool VerifyToken { get; set; }
		/// <summary>
		/// 是否验证权限
		/// </summary>
		public bool VerifyRight { get; set; }

		/// <summary>
		/// 构造函数
		/// </summary>
		public ApiAuthorizeAttribute()
		{
			Frequency = 2D;
		}

		/// <summary>
		/// OnActionExecutionAsync
		/// </summary>
		/// <param name="context"></param>
		/// <param name="next"></param>
		/// <returns></returns>
		public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			if (context == null)
			{
				return null;
			}
			var version = context.HttpContext.Request.Headers["X-Version"].FirstOrDefault();
			if (version != "1.0")
			{
				throw new CustomException("无效的版本。");
			}

			var temp = context.HttpContext.Request.Headers["X-Timestamp"].FirstOrDefault();
			if (string.IsNullOrEmpty(temp))
			{
				throw new CustomException("无效的时间戳。");
			}
			var timestamp = temp.ToDouble();

			//判断时间戳是否有效
			var currentTimestamp = DateTimeHelper.TimestampOfMilliseconds;
			var timestampDiff = currentTimestamp - timestamp;
			if (timestampDiff < 0D || timestampDiff > 300000D)
			{
				//throw new CustomException("请求超时。");
			}

			//请求地址
			var path = context.HttpContext.Request.GetPath();

			//限制请求频率
			if (Frequency > 0D)
			{
				context.HttpContext.Request.VerifyRequestFrequencyLimit(Frequency, path);
			}

			//验证图片验证码
			if (VerifyCode)
			{
				context.HttpContext.Request.VerifyImageCode();
			}

			//验证Excel导出
			if (VerifyExcelExport)
			{
				VerifyExcel(context.HttpContext);
			}

			if (VerifyToken)
			{
				var token = context.HttpContext.Request.Headers["X-Token"].FirstOrDefault();
				if (string.IsNullOrEmpty(token))
				{
					throw new CustomException("login_timeout");
				}
				if (!VerifyLogin(token, out var login))
				{
					throw new CustomException("login_timeout");
				}
				if (VerifyRight && !VerifyRequestRight(login.Id, path))
				{
					throw new CustomException("没有访问权限。");
				}
			}
			try
			{
				WriteLog(context.HttpContext);
			}
			catch
			{
				throw;
			}
			return base.OnActionExecutionAsync(context, next);
		}

		/// <summary>
		/// 验证登录
		/// </summary>
		/// <param name="token"></param>
		/// <param name="login"></param>
		/// <returns></returns>
		protected virtual bool VerifyLogin(string token, out Token<TokenData> login)
		{
			return Basic.BLL.LoginBLL.VerifyLogin(token, out login);
		}

		/// <summary>
		/// 验证权限
		/// </summary>
		/// <param name="userId"></param>
		/// <param name="path"></param>
		/// <returns></returns>
		protected virtual bool VerifyRequestRight(int userId, string path)
		{
			return false;
		}

		/// <summary>
		/// 验证Excel导出
		/// </summary>
		/// <param name="httpRequest"></param>
		/// <returns></returns>
		protected virtual void VerifyExcel(HttpContext httpContext)
		{
			var fileName = httpContext.Request.Headers["X-ExcelName"].FirstOrDefault();
			var headers = httpContext.Request.Headers["X-ExcelHeaders"].FirstOrDefault();
			if (string.IsNullOrEmpty(fileName))
			{
				throw new CustomException("参数不能为空（X-ExcelName）。");
			}
			if (string.IsNullOrEmpty(headers))
			{
				throw new CustomException("参数不能为空（X-ExcelHeaders）。");
			}
			var _headers = headers.Split(',');
			if (_headers.Length == 0)
			{
				throw new CustomException("参数格式错误（X-ExcelHeaders）。");
			}
			var dic = new Dictionary<string, string>();
			foreach (var _header in _headers)
			{
				var kv = _header.Split(':');
				if (kv.Length != 2)
				{
					throw new CustomException("列头格式错误。");
				}
				var key = kv[0];
				if (string.IsNullOrEmpty(key))
				{
					throw new CustomException("列头格式错误。");
				}
				var value = kv[1];
				if (dic.ContainsKey(key))
				{
					throw new CustomException("列头格式错误。");
				}
				dic.Add(key, value);
			}
		}

		/// <summary>
		/// 记录日志
		/// </summary>
		/// <param name="httpContext"></param>
		protected virtual void WriteLog(HttpContext httpContext)
		{
			var data = httpContext.Request.GetParameters();
			Log4netHelper.InfoFormat("接收请求=>{0},{1} {2} {3} {4} {5}",
				httpContext.Connection.RemoteIpAddress.ToString(),
				httpContext.Connection.RemotePort,
				httpContext.Request.Headers["X-Mac"].FirstOrDefault(),
				httpContext.Request.Method,
				httpContext.Request.Path.Value,
				data);

		}
	}
}