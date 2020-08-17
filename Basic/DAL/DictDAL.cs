using Basic.Model;
using System;
using System.Linq.Expressions;

namespace Basic.DAL
{
	/// <summary>
	/// DictDAL
	/// </summary>
	internal class DictDAL : TreeDAL<Dict>
	{
		/// <summary>
		/// 创建个性化查询表达式
		/// </summary>
		/// <returns></returns>
		public override Expression<Func<Dict, Dict>> CreatePersonalSelector()
		{
			return o => new Dict()
			{
				Value = o.Value
			};
		}
	}
}
