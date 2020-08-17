using Adai.Standard.Models;
using Basic.DAL;
using Basic.Model;
using System;

namespace Basic.BLL
{
	/// <summary>
	/// EmployeeBLL
	/// </summary>
	public class EmployeeBLL : BLL<Employee>
	{
		/// <summary>
		/// Dal
		/// </summary>
		readonly EmployeeDAL Dal;

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="loginInfo"></param>
		public EmployeeBLL(Token<TokenData> loginInfo = null) : base(loginInfo)
		{
			IDal = Dal = new EmployeeDAL();
		}

		/// <summary>
		/// 验证
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public override string Validate(Employee data)
		{
			if (string.IsNullOrEmpty(data.FirstName))
			{
				return "名不能为空。";
			}
			if (string.IsNullOrEmpty(data.LastName))
			{
				return "姓不能为空。";
			}
			if (!ValidateStatus(data.Status))
			{
				return "状态标识无效。";
			}
			data.UpdateTime = DateTime.Now;
			if (data.Id == 0)
			{
				data.CreateTime = data.UpdateTime;
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
			var data = new Employee()
			{
				Id = id,
				Status = status
			};
			Dal.Update(data, new string[] { "Status" });
		}

		#region 扩展

		/// <summary>
		/// 新增登录帐号
		/// </summary>
		/// <param name="id"></param>
		/// <param name="username"></param>
		/// <param name="roleIds"></param>
		public void AddUser(int id, string username, params int[] roleIds)
		{
			var result = Dal.Get(id, true);
			//if (result.UserId.HasValue)
			//{
			//	var _msg = Localizer.GetString("Login account already exists");
			//	throw new CustomException(_msg);
			//}
			var user = new User()
			{
				Username = username,
				FirstName = result.FirstName,
				Email = result.Email,
				Mobile = result.Mobile,
				Tel = result.Tel,
				RoleIds = roleIds,
				Status = Model.Config.Status.Enabled
			};
			new UserBLL().Add(user);
			//result.UserId = user.Id;
			//Dal.Update(result, new string[] { "UserId" });
		}

		#endregion
	}
}
