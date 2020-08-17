namespace Basic.Model
{
	/// <summary>
	/// 地域
	/// </summary>
	public partial class Region
	{
		/// <summary>
		/// Desc:全称
		/// Default:
		/// Nullable:True
		/// </summary>
		public string FullName { get; set; }

		/// <summary>
		/// Desc:英文名称
		/// Default:
		/// Nullable:True
		/// </summary>
		public string EnName { get; set; }

		/// <summary>
		/// Desc:拼音
		/// Default:
		/// Nullable:True
		/// </summary>
		public string Pinyin { get; set; }

		/// <summary>
		/// Desc:地区代码
		/// Default:
		/// Nullable:True
		/// </summary>
		public string AreaCode { get; set; }

		/// <summary>
		/// Desc:邮政编码
		/// Default:
		/// Nullable:True
		/// </summary>
		public string ZipCode { get; set; }
	}
}
