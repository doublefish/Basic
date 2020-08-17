using Basic.BLL;
using Basic.Model;
using Basic.Model.PageArg;
using Microsoft.AspNetCore.Mvc;
using System;
using WebApi.Background.Models;
using WebApi.Models;

namespace WebApi.Background.Controllers
{
	/// <summary>
	/// 员工
	/// </summary>
	[Route("api/Employee"), ApiExplorerSettings(GroupName = "business")]
	public class EmployeeController : ApiController
	{
		/// <summary>
		/// 新增
		/// </summary>
		/// <param name="model">新增内容</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true, VerifyRight = true)]
		[HttpPost("Add")]
		public ReturnResult<int> Add([FromBody] EmployeeModel model)
		{
			var data = new Employee()
			{
				Code = model.Code,
				FirstName = model.FirstName,
				LastName = model.LastName,
				Sex = model.Sex ?? 0,
				Birthday = model.Birthday,
				IdType = model.IdType,
				IdNumber = model.IdNumber,
				Nation = model.Nation,
				NativePlace = model.NativePlace,
				PoliticalStatus = model.PoliticalStatus,
				MaritalStatus = model.MaritalStatus,
				HealthStatus = model.HealthStatus,
				Faith = model.Faith,
				Education = model.Education,
				School = model.School,
				Major = model.Major,
				JobTitle = model.JobTitle,
				LanguageSkills = model.LanguageSkills,
				ComputerSkills = model.ComputerSkills,
				Certificates = model.Certificates,
				Email = model.Email,
				Mobile = model.Mobile,
				Tel = model.Tel,
				PostalCode = model.PostalCode,
				Address = model.Address,
				Post = model.Post,
				EntryTime = model.EntryTime,
				Photo = model.Photo,
				Status = model.Status,
				Note = model.Note
			};
			new EmployeeBLL(LoginInfo).Add(data);
			return Json(data.Id);
		}

		/// <summary>
		/// 修改
		/// </summary>
		/// <param name="id">Id</param>
		/// <param name="model">修改内容</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true, VerifyRight = true)]
		[HttpPut("Update/{id}")]
		public ReturnResult<string> Update(int id, [FromBody] EmployeeModel model)
		{
			var data = new Employee()
			{
				Id = id,
				Code = model.Code,
				FirstName = model.FirstName,
				LastName = model.LastName,
				Sex = model.Sex ?? 0,
				Birthday = model.Birthday,
				IdType = model.IdType,
				IdNumber = model.IdNumber,
				Nation = model.Nation,
				NativePlace = model.NativePlace,
				PoliticalStatus = model.PoliticalStatus,
				MaritalStatus = model.MaritalStatus,
				HealthStatus = model.HealthStatus,
				Faith = model.Faith,
				Education = model.Education,
				School = model.School,
				Major = model.Major,
				JobTitle = model.JobTitle,
				LanguageSkills = model.LanguageSkills,
				ComputerSkills = model.ComputerSkills,
				Certificates = model.Certificates,
				Email = model.Email,
				Mobile = model.Mobile,
				Tel = model.Tel,
				PostalCode = model.PostalCode,
				Address = model.Address,
				Post = model.Post,
				EntryTime = model.EntryTime,
				Photo = model.Photo,
				Status = model.Status,
				Note = model.Note
			};
			new EmployeeBLL(LoginInfo).Update(data);
			return Ok();
		}

		/// <summary>
		/// 修改状态
		/// </summary>
		/// <param name="id">Id</param>
		/// <param name="status">修改内容</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true, VerifyRight = true)]
		[HttpPut("UpdateStatus/{id}")]
		public ReturnResult<string> UpdateStatus(int id, [FromBody] int status)
		{
			new EmployeeBLL(LoginInfo).UpdateStatus(id, status);
			return Ok();
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="id">Id</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true, VerifyRight = true)]
		[HttpDelete("Delete/{id}")]
		public ReturnResult<string> Delete(int id)
		{
			new EmployeeBLL(LoginInfo).Delete(id);
			return Ok();
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="id">Id</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true, VerifyRight = true)]
		[HttpGet("Get/{id}")]
		public ReturnResult<Employee> Get(int id)
		{
			var result = new EmployeeBLL(LoginInfo).Get(id);
			return Json(result);
		}

		/// <summary>
		/// 分页查询
		/// </summary>
		/// <param name="fullName">姓名</param>
		/// <param name="mobile">手机号码</param>
		/// <param name="start">入职时间.开始时间</param>
		/// <param name="end">入职时间.结束时间</param>
		/// <param name="status">状态</param>
		/// <param name="pageNumber">页码（从0开始），默认0</param>
		/// <param name="pageSize">每页条数，默认20</param>
		/// <param name="sortName">排序字段</param>
		/// <param name="sortType">排序方式，默认0，0：降序，1：升序</param>
		/// <returns></returns>
		[ApiAuthorize(VerifyToken = true, VerifyRight = true)]
		[HttpGet("List")]
		public ReturnResult<BaseArg<Employee>> List(string fullName = null,
			string mobile = null, DateTime? start = null, DateTime? end = null, int? status = null,
			int? pageNumber = null, int? pageSize = null, string sortName = null, int? sortType = null)
		{
			var arg = new BaseArg<Employee>(pageNumber, pageSize, sortName, sortType)
			{
				FullName = fullName,
				Mobile = mobile,
				Start = start,
				End = end,
				Status = status
			};
			new EmployeeBLL(LoginInfo).List(arg);
			return Json(arg);
		}
	}
}