using Basic.BLL;
using Basic.Model;
using Basic.Model.PageArg;
using Microsoft.AspNetCore.Mvc;
using WebApi.Foreground.Models;
using WebApi.Models;

namespace WebApi.Foreground.Controllers
{
	/// <summary>
	/// 个人银行卡
	/// </summary>
	[Route("api/Account/BankCard"), ApiExplorerSettings(GroupName = "account")]
	public class AccountBankCardController : ApiController
	{
		/// <summary>
		/// 新增
		/// </summary>
		/// <param name="model">新增内容</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true)]
		[HttpPost("Add")]
		public ReturnResult<int> Add([FromBody] BankCardModel model)
		{
			var data = new AccountBankCard()
			{
				BankId = model.BankId,
				CardNumber = model.CardNumber,
				Cardholder = model.Cardholder,
				Branch = model.Branch
			};
			new AccountBankCardBLL(LoginInfo).Add(data);
			return Json(data.Id);
		}

		/// <summary>
		/// 修改
		/// </summary>
		/// <param name="id">Id</param>
		/// <param name="model">内容</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true)]
		[HttpPut("Update/{id}")]
		public ReturnResult<string> Update(int id, [FromBody] BankCardModel model)
		{
			var data = new AccountBankCard()
			{
				Id = id,
				BankId = model.BankId,
				CardNumber = model.CardNumber,
				Cardholder = model.Cardholder,
				Branch = model.Branch
			};
			new AccountBankCardBLL(LoginInfo).Update(data);
			return Ok();
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="id">Id</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true)]
		[HttpDelete("Delete/{id}")]
		public ReturnResult<string> Delete(int id)
		{
			new AccountBankCardBLL(LoginInfo).Delete(id);
			return Ok();
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true)]
		[HttpGet("Get/{id}")]
		public ReturnResult<AccountBankCard> Get()
		{
			var result = new AccountBankCardBLL(LoginInfo).GetByAccountId(LoginInfo.Id);
			return Json(result);
		}

		/// <summary>
		/// 分页查询
		/// </summary>
		/// <param name="username">用户名</param>
		/// <param name="mobile">手机号码</param>
		/// <param name="bankId">银行Id</param>
		/// <param name="cardNumber">卡号</param>
		/// <param name="cardholder">持卡人</param>
		/// <param name="branch">支行</param>
		/// <param name="pageNumber">页码（从0开始），默认0</param>
		/// <param name="pageSize">每页条数，默认20</param>
		/// <param name="sortName">排序字段</param>
		/// <param name="sortType">排序方式，默认0，0：降序，1：升序</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true)]
		[HttpGet("List")]
		public ReturnResult<BankCardArg<AccountBankCard>> List(string username = null, string mobile = null,
			int? bankId = null, string cardNumber = null, string cardholder = null, string branch = null,
			int? pageNumber = null, int? pageSize = null, string sortName = null, int? sortType = null)
		{
			var arg = new BankCardArg<AccountBankCard>(pageNumber, pageSize, sortName, sortType)
			{
				Username = username,
				Mobile = mobile,
				BankId = bankId,
				CardNumber = cardNumber,
				Cardholder = cardholder,
				Branch = branch
			};
			new AccountBankCardBLL(LoginInfo).List(arg);
			return Json(arg);
		}
	}
}
