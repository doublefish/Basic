using Adai.Base;
using Adai.Base.Ext;
using Adai.Security;
using Adai.Standard;
using Adai.Standard.Ext;
using Adai.Standard.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Basic
{
	/// <summary>
	/// CommonHelper
	/// </summary>
	public static class CommonHelper
	{
		/// <summary>
		/// 文件服务器
		/// </summary>
		static readonly string FileServer = JsonConfigHelper.Configuration["FileServer"].ToString();

		/// <summary>
		/// 获取本地语言
		/// </summary>
		/// <param name="type"></param>
		/// <param name="name"></param>
		/// <param name="culture"></param>
		/// <param name="shared"></param>
		/// <returns></returns>
		public static string GetLocalString(Type type, string name, CultureInfo culture = null, bool shared = false)
		{
			if (culture == null)
			{
				culture = CultureInfo.CurrentCulture;
			}
			var baseName = string.Format("{0}.Language.{1}", type.Namespace, shared ? "Shared" : type.Name);
			var localizer = ResourceHelper.Get(baseName, type.Assembly);
			return localizer.GetString(name, culture);
		}

		/// <summary>
		/// 生成编号
		/// </summary>
		/// <param name="typeName"></param>
		/// <returns></returns>
		public static string GenerateNumber(string typeName)
		{
			var dateStr = DateTime.Now.ToString("yyyyMMdd");
			var redis = RedisHelper.Db15;
			var key = string.Format("NumberCount-{0}", dateStr);
			var hashField = typeName;
			var count = 0;
			if (!redis.KeyExists(key))
			{
				redis.HashSet(key, hashField, count);
				redis.KeyExpire(key, DateTime.Now.AddDays(1));
			}
			count = redis.HashGet(key, hashField).ToInt32(0);
			count++;
			var number = string.Format("{0}{1}", dateStr, count.ToString().Complete(8));
			redis.HashSet(key, hashField, count);
			return number;
		}

		/// <summary>
		/// 生成用户名
		/// </summary>
		/// <returns></returns>
		public static string GenerateUsername()
		{
			return string.Format("+{0}", StringHelper.GenerateRandom(10));
		}

		/// <summary>
		/// 生成密码
		/// </summary>
		/// <param name="secretKey">密钥</param>
		/// <param name="original">原文</param>
		/// <returns></returns>
		public static string GeneratePassword(string secretKey, string original)
		{
			var array = secretKey.Split('-');
			array[4] = original;
			var cleartext = string.Join("-", array);
			return MD5Helper.Encrypt(cleartext);
		}

		/// <summary>
		/// 生成密码
		/// </summary>
		/// <param name="secretKey">密钥</param>
		/// <param name="original">原文</param>
		/// <param name="password">密文</param>
		/// <returns></returns>
		public static bool VerifyPassword(string secretKey, string original, string password)
		{
			var cipher = GeneratePassword(secretKey, original);
			return string.Compare(password, cipher) == 0;
		}

		/// <summary>
		/// 是否手机号码
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public static bool IsMobile(string input)
		{
			return Regex.IsMatch(input, @"^[1][34578]\d{9}$");
		}

		/// <summary>
		/// 加密
		/// </summary>
		/// <param name="original">原文</param>
		/// <returns></returns>
		public static string Encrypt(string original)
		{
			try
			{
				var array = original.Split('-');
				var temp = array[0];

				array[0] = array[1] + array[2];
				array[1] = temp.Substring(0, 4);
				array[2] = temp.Substring(4, 4);
				//array[3] = array[3];
				//array[4] = array[4];

				return string.Join("-", array);
			}
			catch (Exception ex)
			{
				throw new CustomException(string.Format("加密失败：", ex.Message));
			}
		}

		/// <summary>
		/// 解密
		/// </summary>
		/// <param name="cipher">密码</param>
		/// <returns></returns>
		public static string Decrypt(string cipher)
		{
			try
			{
				var array = cipher.Split('-');
				var temp = array[0];

				array[0] = array[1] + array[2];
				array[1] = temp.Substring(0, 4);
				array[2] = temp.Substring(4, 4);
				//array[3] = array[3];
				//array[4] = array[4];

				var original = string.Join("-", array);

				return original.Replace("-", "");
			}
			catch (Exception ex)
			{
				throw new CustomException(string.Format("解密失败：", ex.Message));
			}
		}

		/// <summary>
		/// IdsToString
		/// </summary>
		/// <param name="ids"></param>
		/// <returns></returns>
		public static string IdsToString(ICollection<int> ids)
		{
			if (ids == null || ids.Count == 0)
			{
				return "";
			}
			var str = string.Join(",", ids);
			return string.Format(",{0},", str);
		}

		/// <summary>
		/// StringToIds
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public static ICollection<int> StringToIds(string str)
		{
			var ids = new HashSet<int>();
			if (!string.IsNullOrEmpty(str))
			{
				var array = str.Split(",");
				foreach (var id in array)
				{
					if (string.IsNullOrEmpty(id))
					{
						continue;
					}
					ids.Add(id.ToInt32());
				}
			}
			return ids;
		}

		/// <summary>
		/// 替换文件路径
		/// </summary>
		/// <param name="content"></param>
		/// <returns></returns>
		public static string ReplaceFilePath(string content)
		{
			return string.IsNullOrEmpty(content) ? content : content.Replace(FileServer, "{root}");
		}

		/// <summary>
		/// 还原文件路径
		/// </summary>
		/// <param name="content"></param>
		/// <returns></returns>
		public static string RestoreFilePath(string content)
		{
			return string.IsNullOrEmpty(content) ? content : content.Replace("{root}", FileServer);
		}
	}
}
