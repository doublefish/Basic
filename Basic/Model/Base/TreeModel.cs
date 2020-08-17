using Adai.Standard;
using SqlSugar;
using System;
using System.Collections.Generic;

namespace Basic.Model
{
	/// <summary>
	/// 树形实体
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class TreeModel<T> : TreeModel where T : TreeModel
	{
		/// <summary>
		/// 扩展.父节点
		/// </summary>
		[SugarColumn(IsIgnore = true)]
		public T Parent { get; set; }

		/// <summary>
		/// 扩展.子节点
		/// </summary>
		[SugarColumn(IsIgnore = true)]
		public ICollection<T> Children { get; set; }
	}

	/// <summary>
	/// 树形实体
	/// </summary>
	public class TreeModel
	{
		/// <summary>
		/// Desc:ID
		/// Default:
		/// Nullable:False
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Desc:父节点Id
		/// Default:
		/// Nullable:False
		/// </summary>
		public int ParentId { get; set; }

		/// <summary>
		/// Desc:父节点
		/// Default:
		/// Nullable:False
		/// </summary>
		public string Parents { get; set; }

		/// <summary>
		/// Desc:编码
		/// Default:
		/// Nullable:False
		/// </summary>
		public string Code { get; set; }

		/// <summary>
		/// Desc:名称
		/// Default:
		/// Nullable:False
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Desc:序号
		/// Default:
		/// Nullable:False
		/// </summary>
		public int Sequence { get; set; }

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

		#region

		/// <summary>
		/// 扩展.父节点Id
		/// </summary>
		[SugarColumn(IsIgnore = true)]
		public ICollection<int> ParentIds => CommonHelper.StringToIds(Parents);

		/// <summary>
		/// 扩展.父节点名称
		/// </summary>
		[SugarColumn(IsIgnore = true)]
		public ICollection<string> ParentNames { get; set; }

		/// <summary>
		/// 扩展.是否有子节点
		/// </summary>
		[SugarColumn(IsIgnore = true)]
		public bool HasChild { get; set; }

		/// <summary>
		/// 扩展.状态说明
		/// </summary>
		[SugarColumn(IsIgnore = true)]
		public string StatusNote => ConfigIntHelper<Config.Status>.GetValue(Status);

		/// <summary>
		/// 扩展.状态是否已启用
		/// </summary>
		[SugarColumn(IsIgnore = true)]
		public bool IsEnabled => Status == Config.Status.Enabled;

		#endregion
	}
}
