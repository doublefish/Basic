using Basic.BLL;
using Basic.Model;
using Basic.Model.PageArg;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Agent.Controllers
{
	/// <summary>
	/// 银行卡
	/// </summary>
	[Route("api/BankCard"), ApiExplorerSettings(GroupName = "agent")]
	public class BankCardController : ApiController
	{
		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="id">Id</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true, VerifyRight = true)]
		[HttpGet("Get/{id}")]
		public ReturnResult<AgentBankCard> Get(int id)
		{
			var result = new AgentBankCardBLL(LoginInfo).Get(id);
			return Json(result);
		}

		/// <summary>
		/// 分页查询
		/// </summary>
		/// <param name="bankId">开户行Id</param>
		/// <param name="cardNumber">卡号</param>
		/// <param name="cardholder">持卡人</param>
		/// <param name="branch">开户行支行</param>
		/// <param name="pageNumber">页码（从0开始），默认0</param>
		/// <param name="pageSize">每页条数，默认20</param>
		/// <param name="sortName">排序字段</param>
		/// <param name="sortType">排序方式，默认0，0：降序，1：升序</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true, VerifyRight = true)]
		[HttpGet("List")]
		public ReturnResult<BankCardArg<AgentBankCard>> List(int? bankId = null, string cardNumber = null, string cardholder = null, string branch = null,
			int? pageNumber = null, int? pageSize = null, string sortName = null, int? sortType = null)
		{
			var arg = new BankCardArg<AgentBankCard>(pageNumber, pageSize, sortName, sortType)
			{
				BankId = bankId,
				CardNumber = cardNumber,
				Cardholder = cardholder,
				Branch = branch
			};
			new AgentBankCardBLL(LoginInfo).List(arg);
			return Json(arg);
		}
	}
}
