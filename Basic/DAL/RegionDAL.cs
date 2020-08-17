using Basic.Model;
using System;
using System.Linq.Expressions;

namespace Basic.DAL
{
	/// <summary>
	/// RegionDAL
	/// </summary>
	internal class RegionDAL : TreeDAL<Region>
	{
		/// <summary>
		/// 创建个性化查询表达式
		/// </summary>
		/// <returns></returns>
		public override Expression<Func<Region, Region>> CreatePersonalSelector()
		{
			return o => new Region()
			{
				FullName = o.FullName,
				EnName = o.EnName,
				Pinyin = o.Pinyin,
				AreaCode = o.AreaCode,
				ZipCode = o.ZipCode
			};
		}
	}
}
