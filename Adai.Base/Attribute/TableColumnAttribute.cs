using System.Reflection;

namespace Adai.Base.Attribute
{
	/// <summary>
	/// 表属性
	/// </summary>
	public class TableAttribute : System.Attribute
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="name">名称</param>
		public TableAttribute(string name)
		{
			Name = name;
		}

		/// <summary>
		/// 名称
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// 列的属性
		/// </summary>
		public PropertyInfo[] PropertyInfos { get; set; }
	}
}
