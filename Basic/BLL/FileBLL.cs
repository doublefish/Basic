using Adai.Standard.Model;
using Basic.DAL;
using Basic.Model;
using System;

namespace Basic.BLL
{
	/// <summary>
	/// FileBLL
	/// </summary>
	public class FileBLL : BLL<File>
	{
		/// <summary>
		/// Dal
		/// </summary>
		readonly FileDAL Dal;

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="loginInfo"></param>
		public FileBLL(Token<TokenData> loginInfo = null) : base(loginInfo)
		{
			IDal = Dal = new FileDAL();
		}

		/// <summary>
		/// 验证
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public override string Validate(File data)
		{
			if (string.IsNullOrEmpty(data.Code))
			{
				return "编码不能为空。";
			}
			if (string.IsNullOrEmpty(data.Name))
			{
				return "名称不能为空。";
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

		#region 扩展

		#endregion
	}
}
