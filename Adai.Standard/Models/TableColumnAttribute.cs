using System;

namespace Adai.Standard.Models
{
	/// <summary>
	/// 列属性
	/// </summary>
	public class TableColumnAttribute : Attribute
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="name"></param>
		public TableColumnAttribute(string name)
		{
			Name = name;
		}

		/// <summary>
		/// 列名
		/// </summary>
		public string Name { get; set; }
	}
}
