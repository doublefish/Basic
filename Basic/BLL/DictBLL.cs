using Adai.Standard.Models;
using Basic.DAL;
using Basic.Model;

namespace Basic.BLL
{
	/// <summary>
	/// DictBLL
	/// </summary>
	public class DictBLL : TreeBLL<Dict>
	{
		/// <summary>
		/// Dal
		/// </summary>
		readonly DictDAL Dal;

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="loginInfo"></param>
		public DictBLL(Token<TokenData> loginInfo = null) : base(loginInfo)
		{
			IDal = Dal = new DictDAL();
			if (Dal == null)
			{
				//防止编辑器提示警告信息
			}
		}
	}
}
