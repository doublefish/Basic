namespace Basic.Model
{
	/// <summary>
	/// 产品图片
	/// </summary>
	public partial class ProductImage
	{
		/// <summary>
		/// Desc:ID
		/// Default:
		/// Nullable:False
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Desc:产品Id
		/// Default:
		/// Nullable:False
		/// </summary>
		public int ProductId { get; set; }

		/// <summary>
		/// Desc:图片地址
		/// Default:
		/// Nullable:False
		/// </summary>
		public string ImageUrl { get; set; }

		/// <summary>
		/// Desc:序号
		/// Default:
		/// Nullable:False
		/// </summary>
		public int Sequence { get; set; }
	}
}
