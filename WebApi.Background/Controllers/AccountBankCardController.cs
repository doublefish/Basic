using Basic.BLL;
using Basic.Model;
using Basic.Model.PageArg;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Background.Controllers
{
	/// <summary>
	/// 用户银行卡
	/// </summary>
	[Route("api/Account/BankCard"), ApiExplorerSettings(GroupName = "account")]
	public class AccountBankCardController : ApiController
	{
		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="id">Id</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true, VerifyRight = true)]
		[HttpDelete("Delete/{id}")]
		public ReturnResult<string> Delete(int id)
		{
			new AccountBankCardBLL(LoginInfo).Delete(id);
			return Ok();
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="id">Id</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true, VerifyRight = true)]
		[HttpGet("Get/{id}")]
		public ReturnResult<AccountBankCard> Get(int id)
		{
			var result = new AccountBankCardBLL(LoginInfo).Get(id);
			return Json(result);
		}

		/// <summary>
		/// 分页查询
		/// </summary>
		/// <param name="accountId">用户Id</param>
		/// <param name="username">用户名</param>
		/// <param name="bankId">银行Id</param>
		/// <param name="cardNumber">卡号</param>
		/// <param name="cardholder">持卡人</param>
		/// <param name="branch">支行</param>
		/// <param name="pageNumber">页码（从0开始），默认0</param>
		/// <param name="pageSize">每页条数，默认20</param>
		/// <param name="sortName">排序字段</param>
		/// <param name="sortType">排序方式，默认0，0：降序，1：升序</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true, VerifyRight = true)]
		[HttpGet("List/{accountId?}")]
		public ReturnResult<BankCardArg<AccountBankCard>> List(int? accountId = null, string username = null,
			int? bankId = null, string cardNumber = null, string cardholder = null, string branch = null,
			int? pageNumber = null, int? pageSize = null, string sortName = null, int? sortType = null)
		{
			var arg = new BankCardArg<AccountBankCard>(pageNumber, pageSize, sortName, sortType)
			{
				AccountId = accountId,
				Username = username,
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
