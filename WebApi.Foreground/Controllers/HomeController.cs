using Adai.Standard;
using Adai.Standard.Models;
using Basic.BLL;
using Basic.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using WebApi.Foreground.Models;
using WebApi.Models;

namespace WebApi.Foreground.Controllers
{
	/// <summary>
	/// HomeController
	/// </summary>
	[Route("api"), ApiExplorerSettings(GroupName = "public")]
	public class HomeController : ApiController
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="factory"></param>
		public HomeController(IStringLocalizerFactory factory) : base(factory)
		{
		}

		/// <summary>
		/// 登录
		/// </summary>
		/// <param name="model">登录信息</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyCode = true)]
		[HttpPost("SignIn")]
		public ReturnResult<Token<TokenData>> SignIn([FromBody] SignInModel model)
		{
			var token = LoginBLL.SignIn("Account", model.Username, model.Password, Platform);
			return Json(token);
		}

		/// <summary>
		/// 获取登录信息
		/// </summary>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = false)]
		[HttpGet("GetLogin")]
		public ReturnResult<Token<TokenData>> GetLogin()
		{
			return Json(LoginInfo);
		}

		/// <summary>
		/// 注册
		/// </summary>
		/// <param name="model">注册信息</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyCode = true)]
		[HttpPost]
		[Route("SignUp")]
		public ReturnResult<string> SignUp([FromBody] SignUpModel model)
		{
			var data = new Account()
			{
				Username = model.Username,
				Nickname = model.Nickname,
				Mobile = model.Mobile
			};
			new AccountBLL().SignUp(data, model.Password, model.SmsCode, model.PromoCode, model.PromoterId);
			return Ok();
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="username">用户名</param>
		/// <returns></returns>
		[ApiAuthorize]
		[HttpGet("ExistByUsername/{username}")]
		public ReturnResult<bool> ExistByUsername(string username)
		{
			var result = new AccountBLL().ExistByUsername(0, username, true);
			return Json(result);
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="mobile">手机号码</param>
		/// <returns></returns>
		[ApiAuthorize]
		[HttpGet("ExistByMobile/{mobile}")]
		public ReturnResult<bool> ExistByMobile(string mobile)
		{
			var result = new AccountBLL().ExistByMobile(0, mobile, true);
			return Json(result);
		}

		/// <summary>
		/// 关于我们
		/// </summary>
		/// <returns></returns>
		[ApiAuthorize]
		[HttpGet("AboutUs")]
		public ReturnResult<string> AboutUs()
		{
			var result = new ArticleBLL().GetAboutUs(true);
			return Json(result);
		}

		/// <summary>
		/// 联系我们
		/// </summary>
		/// <returns></returns>
		[ApiAuthorize]
		[HttpGet("ContactUs")]
		public ReturnResult<string> ContactUs()
		{
			var result = new ArticleBLL().GetContactUs(true);
			return Json(result);
		}

		/// <summary>
		/// 常见问题
		/// </summary>
		/// <returns></returns>
		[ApiAuthorize]
		[HttpGet("FAQ")]
		public ReturnResult<string> FAQ()
		{
			var result = new ArticleBLL().GetFAQ(true);
			return Json(result);
		}

		/// <summary>
		/// 注册协议
		/// </summary>
		/// <returns></returns>
		[ApiAuthorize]
		[HttpGet("SignupAgreement")]
		public ReturnResult<string> SignupAgreement()
		{
			var result = new ArticleBLL().GetSignup(true);
			return Json(result);
		}

		/// <summary>
		/// 应用程序配置
		/// </summary>
		/// <returns></returns>
		[ApiAuthorize]
		[HttpGet("AppSetting")]
		public ReturnResult<AppSetting> AppSetting()
		{
			return Json(AppSettingBLL.AppSetting);
		}

		#region 字典

		/// <summary>
		/// 查询地域
		/// </summary>
		/// <param name="parentId">父节点Id</param>
		/// <returns></returns>
		[ApiAuthorize(Frequency = 4D)]
		[HttpGet("ListRegion/{parentId}")]
		public ReturnResult<ICollection<Region>> ListRegion(int parentId = 0)
		{
			var results = new RegionBLL(LoginInfo).ListByParentId(parentId, true);
			return Json(results);
		}

		/// <summary>
		/// 查询所有银行
		/// </summary>
		/// <returns></returns>
		[ApiAuthorize]
		[HttpGet("ListBank")]
		public ReturnResult<ICollection<Dict>> ListBank()
		{
			var results = new DictBLL(LoginInfo).ListByParentCode(Basic.Model.Config.Dict.Bank.Root, true);
			return Json(results);
		}

		/// <summary>
		/// 查询所有学位
		/// </summary>
		/// <returns></returns>
		[ApiAuthorize]
		[HttpGet("ListDegree")]
		public ReturnResult<ICollection<Dict>> ListDegree()
		{
			var results = new DictBLL(LoginInfo).ListByParentCode(Basic.Model.Config.Dict.Degree.Root, true);
			return Json(results);
		}

		/// <summary>
		/// 查询所有性别
		/// </summary>
		/// <returns></returns>
		[ApiAuthorize]
		[HttpGet("ListSex")]
		public ReturnResult<IDictionary<int, string>> ListSex()
		{
			var results = ConfigIntHelper<Basic.Model.Config.Sex>.KeyValuePairs;
			return Json(results);
		}

		/// <summary>
		/// 查询所有目的地
		/// </summary>
		/// <returns></returns>
		[ApiAuthorize]
		[HttpGet("ListDestination")]
		public ReturnResult<ICollection<Dict>> ListDestination()
		{
			var results = new DictBLL(LoginInfo).ListByParentCode(Basic.Model.Config.Dict.Business.Destination, true);
			return Json(results);
		}

		/// <summary>
		/// 查询所有出发地
		/// </summary>
		/// <returns></returns>
		[ApiAuthorize]
		[HttpGet("ListDeparture")]
		public ReturnResult<ICollection<Dict>> ListDeparture()
		{
			var results = new DictBLL(LoginInfo).ListByParentCode(Basic.Model.Config.Dict.Business.Departure, true);
			return Json(results);
		}

		/// <summary>
		/// 查询所有酒店等级
		/// </summary>
		/// <returns></returns>
		[ApiAuthorize]
		[HttpGet("ListHotelLevel")]
		public ReturnResult<ICollection<Dict>> ListHotelLevel()
		{
			var results = new DictBLL(LoginInfo).ListByParentCode(Basic.Model.Config.Dict.Business.HotelLevel, true);
			return Json(results);
		}

		/// <summary>
		/// 查询所有联系方式
		/// </summary>
		/// <returns></returns>
		[ApiAuthorize]
		[HttpGet("ListContactMethod")]
		public ReturnResult<ICollection<Dict>> ListContactMethod()
		{
			var results = new DictBLL(LoginInfo).ListByParentCode(Basic.Model.Config.Dict.Business.ContactMethod, true);
			return Json(results);
		}

		#endregion
	}
}
