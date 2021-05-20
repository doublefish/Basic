using Adai.Standard;
using Adai.Standard.Model;
using Basic.DAL;
using Basic.Model;
using System;
using System.Collections.Generic;

namespace Basic.BLL
{
	/// <summary>
	/// AccountBLL
	/// </summary>
	public class AccountBLL : BLL<Account>
	{
		/// <summary>
		/// Dal
		/// </summary>
		readonly AccountDAL Dal;

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="loginInfo"></param>
		public AccountBLL(Token<TokenData> loginInfo = null) : base(loginInfo)
		{
			IDal = Dal = new AccountDAL();
		}

		/// <summary>
		/// 验证
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public override string Validate(Account data)
		{
			if (data.Id == 0)
			{
				if (string.IsNullOrEmpty(data.Username))
				{
					return "用户名不能为空。";
				}
				if (ExistByUsername(data.Id, data.Username))
				{
					return "用户名已存在。";
				}
			}
			if (!string.IsNullOrEmpty(data.Email) && ExistByEmail(data.Id, data.Email))
			{
				return "电子邮箱已存在。";
			}
			if (!string.IsNullOrEmpty(data.Mobile) && ExistByMobile(data.Id, data.Mobile))
			{
				return "手机号码已存在。";
			}
			if (!ValidateStatus(data.Status))
			{
				return "状态标识无效。";
			}
			data.Avatar = CommonHelper.ReplaceFilePath(data.Avatar);
			data.UpdateTime = DateTime.Now;
			if (data.Id == 0)
			{
				data.SecretKey = Guid.NewGuid().ToString();
				data.CreateTime = data.UpdateTime;
			}
			else
			{
				return "不允许修改。";
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
			var data = new Account()
			{
				Id = id,
				Status = status
			};
			Dal.Update(data, new string[] { "Status" });
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="id"></param>
		/// <param name="useCache"></param>
		/// <returns></returns>
		public override Account Get(int id, bool useCache = false)
		{
			var result = base.Get(id, useCache);
			result.Avatar = CommonHelper.RestoreFilePath(result.Avatar);
			return result;
		}

		/// <summary>
		/// 关联数据
		/// </summary>
		/// <param name="list"></param>
		public override void List(ICollection<Account> list)
		{
			foreach (var data in list)
			{
				data.Avatar = CommonHelper.RestoreFilePath(data.Avatar);
			}
		}

		#region 扩展

		/// <summary>
		/// 修改
		/// </summary>
		/// <param name="id"></param>
		/// <param name="data"></param>
		public void Modify(int id, Account data)
		{
			data.Id = id;
			if (string.IsNullOrEmpty(data.Nickname))
			{
				throw new CustomException("昵称不能为空。");
			}
			data.UpdateTime = DateTime.Now;
			Dal.Update(data, new string[] { "Nickname", "UpdateTime" });
		}

		/// <summary>
		/// 修改昵称
		/// </summary>
		/// <param name="id"></param>
		/// <param name="nickname"></param>
		public void UpdateNickname(int id, string nickname)
		{
			if (string.IsNullOrEmpty(nickname))
			{
				throw new CustomException("昵称不能为空。");
			}
			var data = new Account()
			{
				Id = id,
				Nickname = nickname,
				UpdateTime = DateTime.Now
			};
			Dal.Update(data, new string[] { "Nickname", "UpdateTime" });
		}

		/// <summary>
		/// 修改头像
		/// </summary>
		/// <param name="id"></param>
		/// <param name="avatar"></param>
		public void UpdateAvatar(int id, string avatar)
		{
			if (string.IsNullOrEmpty(avatar))
			{
				throw new CustomException("头像地址不能为空。");
			}
			var data = new Account()
			{
				Id = id,
				Avatar = CommonHelper.ReplaceFilePath(avatar),
				UpdateTime = DateTime.Now
			};
			Dal.Update(data, new string[] { "Avatar", "UpdateTime" });
		}

		/// <summary>
		/// 是否存在相同用户名
		/// </summary>
		/// <param name="id"></param>
		/// <param name="username"></param>
		/// <returns></returns>
		public bool ExistByUsername(int id, string username, bool useCache = false)
		{
			var result = Dal.GetByUsername(username, useCache);
			return result != null && result.Id != id;
		}

		/// <summary>
		/// 是否存在相同邮箱
		/// </summary>
		/// <param name="id"></param>
		/// <param name="email"></param>
		/// <returns></returns>
		public bool ExistByEmail(int id, string email, bool useCache = false)
		{
			var result = Dal.GetByEmail(email, useCache);
			return result != null && result.Id != id;
		}

		/// <summary>
		/// 是否存在相同手机号码
		/// </summary>
		/// <param name="id"></param>
		/// <param name="mobile"></param>
		/// <returns></returns>
		public bool ExistByMobile(int id, string mobile, bool useCache = false)
		{
			var result = Dal.GetByMobile(mobile, useCache);
			return result != null && result.Id != id;
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="username"></param>
		/// <param name="useCache"></param>
		/// <returns></returns>
		public Account GetByUsername(string username, bool useCache = false)
		{
			return Dal.GetByUsername(username, useCache);
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="mail"></param>
		/// <param name="useCache"></param>
		/// <returns></returns>
		public Account GetByEmail(string mail, bool useCache = false)
		{
			return Dal.GetByEmail(mail, useCache);
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="mobile"></param>
		/// <param name="useCache"></param>
		/// <returns></returns>
		public Account GetByMobile(string mobile, bool useCache = false)
		{
			return Dal.GetByMobile(mobile, useCache);
		}

		#endregion

		#region 业务

		/// <summary>
		/// 锁用户名
		/// </summary>
		//static readonly object lockOfUsername = new object();
		/// <summary>
		/// 锁手机号码
		/// </summary>
		static readonly object lockOfMobile = new object();

		/// <summary>
		/// 注册
		/// </summary>
		/// <param name="data"></param>
		/// <param name="password"></param>
		/// <param name="smsCode"></param>
		/// <param name="promoCode"></param>
		/// <param name="promoterId"></param>
		public void SignUp(Account data, string password, string smsCode, string promoCode = null, int? promoterId = null)
		{
			//if (string.IsNullOrEmpty(data.Username))
			//{
			//	throw new CustomException("用户名不能为空。");
			//}
			if (string.IsNullOrEmpty(data.Mobile))
			{
				throw new CustomException("手机号码不能为空。");
			}
			if (string.IsNullOrEmpty(password))
			{
				throw new CustomException("密码不能为空。");
			}
			if (string.IsNullOrEmpty(smsCode))
			{
				throw new CustomException("短信验证码不能为空。");
			}
			//验证短信验证码
			var error = new SmsBLL().VerifyCode(data.Mobile, Model.Config.Sms.Type.SignUp, smsCode);
			if (!string.IsNullOrEmpty(error))
			{
				throw new CustomException(error);
			}

			//if (ExistByUsername(0, data.Username, true))
			//{
			//	throw new CustomException("用户名已注册。");
			//}
			if (ExistByMobile(0, data.Mobile, true))
			{
				throw new CustomException("手机号码已注册。");
			}

			var redis = RedisHelper.Db;
			//var keyOfLockUsername = "Account-Username-Lock";
			//lock (lockOfUsername)
			//{
			//	if (redis.HashExists(keyOfLockUsername, data.Username))
			//	{
			//		throw new CustomException("用户名已注册。");
			//	}
			//	//写入缓冲区
			//	redis.HashSet(keyOfLockUsername, data.Username, 1);
			//}
			var keyOfLockMobile = "Account-Mobile-Lock";
			lock (lockOfMobile)
			{
				if (redis.HashExists(keyOfLockMobile, data.Mobile))
				{
					throw new CustomException("用户名已注册。");
				}
				//写入缓冲区
				redis.HashSet(keyOfLockMobile, data.Mobile, 1);
			}

			data.Id = 0;
			data.Username = CommonHelper.GenerateUsername();
			data.SecretKey = Guid.NewGuid().ToString();
			data.Status = Model.Config.Status.Enabled;
			data.CreateTime = DateTime.Now;
			data.UpdateTime = DateTime.Now;

			try
			{
				//生成推广关系
				var promotion = new AccountPromotionBLL().Create(promoCode, promoterId);
				Dal.Add(data, password, promotion);
			}
			catch
			{
				throw;
			}
			finally
			{
				//从缓冲区删除
				//redis.HashDelete(keyOfLockUsername, data.Username);
				redis.HashDelete(keyOfLockMobile, data.Mobile);
			}
		}

		/// <summary>
		/// 登录
		/// </summary>
		/// <param name="username"></param>
		/// <param name="password"></param>
		/// <returns></returns>
		internal TokenData SignIn(string username, string password)
		{
			var result = CommonHelper.IsMobile(username) ? GetByMobile(username, true) : GetByUsername(username, true);
			if (result == null || !new AccountPasswordBLL().Verify(result.Id, Model.Config.Password.Type.Login, result.SecretKey, password))
			{
				throw new CustomException("用户名或密码错误。");
			}
			if (!result.IsEnabled)
			{
				throw new CustomException("用户状态不可用。");
			}
			return new TokenData()
			{
				Id = result.Id,
				Username = result.Username,
				Nickname = result.Nickname,
				Avatar = result.Avatar,
				FirstName = result.FirstName,
				LastName = result.LastName,
				Email = result.Email,
				Mobile = result.Mobile
			};
		}

		#endregion
	}
}
