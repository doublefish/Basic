using System;

namespace Basic.Model
{
	/// <summary>
	/// 文章
	/// </summary>
	public partial class Article
	{
		/// <summary>
		/// Desc:ID
		/// Default:
		/// Nullable:False
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Desc:标题
		/// Default:
		/// Nullable:False
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// Desc:版块
		/// Default:
		/// Nullable:False
		/// </summary>
		public string Sections { get; set; }

		/// <summary>
		/// Desc:摘要
		/// Default:
		/// Nullable:True
		/// </summary>
		public string Summary { get; set; }

		/// <summary>
		/// Desc:作者
		/// Default:
		/// Nullable:True
		/// </summary>
		public string Author { get; set; }

		/// <summary>
		/// Desc:来源
		/// Default:
		/// Nullable:True
		/// </summary>
		public string Source { get; set; }

		/// <summary>
		/// Desc:内容
		/// Default:
		/// Nullable:True
		/// </summary>
		public string Content { get; set; }

		/// <summary>
		/// Desc:封面
		/// Default:
		/// Nullable:True
		/// </summary>
		public string Cover { get; set; }

		/// <summary>
		/// Desc:发布时间
		/// Default:
		/// Nullable:False
		/// </summary>
		public DateTime ReleaseTime { get; set; }

		/// <summary>
		/// Desc:是否置顶
		/// Default:
		/// Nullable:False
		/// </summary>
		public bool IsStick { get; set; }

		/// <summary>
		/// Desc:点击量
		/// Default:
		/// Nullable:False
		/// </summary>
		public int Clicks { get; set; }

		/// <summary>
		/// Desc:收藏数
		/// Default:
		/// Nullable:False
		/// </summary>
		public int Favorites { get; set; }

		/// <summary>
		/// Desc:分享数
		/// Default:
		/// Nullable:False
		/// </summary>
		public int Shares { get; set; }

		/// <summary>
		/// Desc:修改人
		/// Default:
		/// Nullable:False
		/// </summary>
		public int UserId { get; set; }

		/// <summary>
		/// Desc:状态
		/// Default:
		/// Nullable:False
		/// </summary>
		public int Status { get; set; }

		/// <summary>
		/// Desc:创建时间
		/// Default:
		/// Nullable:False
		/// </summary>
		public DateTime CreateTime { get; set; }

		/// <summary>
		/// Desc:修改时间
		/// Default:
		/// Nullable:False
		/// </summary>
		public DateTime UpdateTime { get; set; }

		/// <summary>
		/// Desc:说明
		/// Default:
		/// Nullable:True
		/// </summary>
		public string Note { get; set; }
	}
}
