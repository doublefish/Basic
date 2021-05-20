using Adai.Standard.Model;
using Basic.DAL;
using Basic.Model;

namespace Basic.BLL
{
	/// <summary>
	/// CustomerBLL
	/// </summary>
	public class CustomerBLL : BLL<Customer>
	{
		/// <summary>
		/// Dal
		/// </summary>
		readonly CustomerDAL Dal;

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="loginInfo"></param>
		public CustomerBLL(Token<TokenData> loginInfo = null) : base(loginInfo)
		{
			IDal = Dal = new CustomerDAL();
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
			var data = new Customer()
			{
				Id = id,
				Status = status
			};
			Dal.Update(data, new string[] { "Status" });
		}

		#region 扩展

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="mobile"></param>
		/// <returns></returns>
		public Customer GetByMobile(string mobile)
		{
			return Dal.GetByMobile(mobile);
		}

		#endregion
	}
}
