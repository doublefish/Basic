using System.Reflection;

namespace Adai.Base.Attribute
{
	/// <summary>
	/// 列属性
	/// </summary>
	public class TableColumnAttribute : System.Attribute
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="name">名称</param>
		/// <param name="isExtend">是否扩展</param>
		public TableColumnAttribute(string name, bool isExtend = false)
		{
			Name = name;
			IsExtend = isExtend;
		}

		/// <summary>
		/// 名称
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// 是否扩展字段
		/// </summary>
		public bool IsExtend { get; set; }
		/// <summary>
		/// PropertyInfo
		/// </summary>
		public PropertyInfo PropertyInfo { get; set; }
	}
}
