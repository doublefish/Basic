using Adai.Standard.Models;
using Basic.DAL;
using Basic.Model;
using System;

namespace Basic.BLL
{
	/// <summary>
	/// LogBLL
	/// </summary>
	public class LogBLL : BLL<Log>
	{
		/// <summary>
		/// Dal
		/// </summary>
		readonly LogDAL Dal;

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="loginInfo"></param>
		public LogBLL(Token<TokenData> loginInfo = null) : base(loginInfo)
		{
			IDal = Dal = new LogDAL();
		}

		/// <summary>
		/// 验证
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public override string Validate(Log data)
		{
			if (data.Id == 0)
			{
				data.CreateTime = DateTime.Now;
			}
			return base.Validate(data);
		}

		#region 扩展

		#endregion
	}
}
