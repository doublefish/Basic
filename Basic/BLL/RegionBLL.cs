using Adai.Standard.Models;
using Basic.DAL;
using Basic.Model;

namespace Basic.BLL
{
	/// <summary>
	/// RegionBLL
	/// </summary>
	public class RegionBLL : TreeBLL<Region>
	{
		/// <summary>
		/// Dal
		/// </summary>
		readonly RegionDAL Dal;

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="loginInfo"></param>
		public RegionBLL(Token<TokenData> loginInfo = null) : base(loginInfo)
		{
			IDal = Dal = new RegionDAL();
			if (Dal == null)
			{
				//防止编辑器提示警告信息
			}
		}
	}
}
