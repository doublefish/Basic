namespace Adai.Base.Attribute
{
	/// <summary>
	/// 表里的列的特性
	/// </summary>
	public class TableColumnAttribute : CustomAttribute
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="name">名称</param>
		/// <param name="isExtend">是否扩展</param>
		public TableColumnAttribute(string name, bool isExtend = false) : base(name)
		{
			IsExtend = isExtend;
		}

		/// <summary>
		/// 是否扩展字段
		/// </summary>
		public bool IsExtend { get; set; }
	}
}
