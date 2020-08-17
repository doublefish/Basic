using Basic.BLL;
using Basic.Model;
using Microsoft.AspNetCore.Mvc;
using WebApi.Agent.Models;
using WebApi.Models;

namespace WebApi.Agent.Controllers
{
	/// <summary>
	/// 个人银行卡
	/// </summary>
	[Route("api/Personal/BankCard"), ApiExplorerSettings(GroupName = "personal")]
	public class PersonalBankCardController : ApiController
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
		/// 查询
		/// </summary>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true)]
		[HttpGet("Get")]
		public ReturnResult<AgentBankCard> Get()
		{
			var result = new AgentBankCardBLL(LoginInfo).GetByAgentId(LoginInfo.Id);
			return Json(result);
		}
	}
}
