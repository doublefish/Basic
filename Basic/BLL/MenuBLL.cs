using Adai.Standard;
using Adai.Standard.Models;
using Basic.DAL;
using Basic.Model;

namespace Basic.BLL
{
	/// <summary>
	/// MenuBLL
	/// </summary>
	public class MenuBLL : TreeBLL<Menu>
	{
		/// <summary>
		/// Dal
		/// </summary>
		readonly MenuDAL Dal;

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="loginInfo"></param>
		public MenuBLL(Token<TokenData> loginInfo = null) : base(loginInfo)
		{
			IDal = Dal = new MenuDAL();
			if (Dal == null)
			{
				//防止编辑器提示警告信息
			}
		}

		/// <summary>
		/// 验证
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public override string Validate(Menu data)
		{
			if (!ConfigIntHelper<Model.Config.Menu.Type>.ContainsKey(data.Type))
			{
				return "类型标识无效";
			}
			return base.Validate(data);
		}
	}
}
