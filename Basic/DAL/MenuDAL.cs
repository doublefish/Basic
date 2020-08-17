using Basic.Model;
using System;
using System.Linq.Expressions;

namespace Basic.DAL
{
	/// <summary>
	/// MenuDAL
	/// </summary>
	internal class MenuDAL : TreeDAL<Menu>
	{
		/// <summary>
		/// 创建个性化查询表达式
		/// </summary>
		/// <returns></returns>
		public override Expression<Func<Menu, Menu>> CreatePersonalSelector()
		{
			return o => new Menu()
			{
				Type = o.Type,
				PageUrl = o.PageUrl,
				ApiUrl = o.ApiUrl,
				Icon = o.Icon
			};
		}
	}
}
