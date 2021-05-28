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
		/// <param name="Extend">扩展</param>
		public TableColumnAttribute(string name, bool Extend = false) : base(name)
		{
			this.Extend = Extend;
		}

		/// <summary>
		/// 主键
		/// </summary>
		public bool PrimaryKey { get; set; }

		/// <summary>
		/// 扩展字段
		/// </summary>
		public bool Extend { get; set; }
	}
}
