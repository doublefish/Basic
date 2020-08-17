using Basic.BLL;
using Basic.Model;
using Basic.Model.PageArg;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Background.Controllers
{
	/// <summary>
	/// 代理商银行卡
	/// </summary>
	[Route("api/Agent/BankCard"), ApiExplorerSettings(GroupName = "agent")]
	public class AgentBankCardController : ApiController
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
			new AgentBankCardBLL(LoginInfo).Delete(id);
			return Ok();
		}

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
		/// <param name="agentId">代理商Id</param>
		/// <param name="agentName">代理商名称</param>
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
		[HttpGet("List/{agentId?}")]
		public ReturnResult<BankCardArg<AgentBankCard>> List(int? agentId = null, string agentName = null,
			int? bankId = null, string cardNumber = null, string cardholder = null, string branch = null,
			int? pageNumber = null, int? pageSize = null, string sortName = null, int? sortType = null)
		{
			var arg = new BankCardArg<AgentBankCard>(pageNumber, pageSize, sortName, sortType)
			{
				AgentId = agentId,
				AgentName = agentName,
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
