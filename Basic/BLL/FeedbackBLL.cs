using Adai.Standard.Models;
using Basic.DAL;
using Basic.Model;
using System;

namespace Basic.BLL
{
	/// <summary>
	/// FeedbackBLL
	/// </summary>
	public class FeedbackBLL : BLL<Feedback>
	{
		/// <summary>
		/// Dal
		/// </summary>
		readonly FeedbackDAL Dal;

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="loginInfo"></param>
		public FeedbackBLL(Token<TokenData> loginInfo = null) : base(loginInfo)
		{
			IDal = Dal = new FeedbackDAL();
		}

		/// <summary>
		/// 验证
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public override string Validate(Feedback data)
		{
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
			var data = new Feedback()
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
		public Feedback GetByMobile(string mobile)
		{
			return Dal.GetByMobile(mobile);
		}

		#endregion
	}
}
