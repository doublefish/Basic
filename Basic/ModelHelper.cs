using Adai.Standard;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace Basic
{
	/// <summary>
	/// ModelHelper
	/// </summary>
	public static class ModelHelper
	{
		static readonly IDictionary<string, ICollection<string>> propertyNames = new Dictionary<string, ICollection<string>>();
		static readonly object lockOfPropertyNames = new object();

		/// <summary>
		/// 可以修改的列
		/// </summary>
		public static readonly IDictionary<string, ICollection<string>> UpdateColumns = new Dictionary<string, ICollection<string>>()
		{
			{ "Agent", new HashSet<string>() { "Code", "Name", "Sequence", "Status", "UpdateTime", "Note" } },
			{ "AgentMenu", new HashSet<string>() { "Code", "Name", "Type", "PageUrl", "ApiUrl", "Icon", "Level", "IsAdmin", "Sequence", "Status", "UpdateTime", "Note" } },
			{ "AgentCommissionRule", new HashSet<string>() { "Year", "Month", "Rate", "Amount", "UpdateTime", "Note" } },
			{ "AgentUser", new HashSet<string>() { "Nickname", "FirstName", "LastName", "Sex", "Birthday", "IdType", "IdNumber", "Email", "Mobile", "Tel", "Status", "UpdateTime", "Note" } },
			{ "CommissionRule", new HashSet<string>() { "Year", "Month", "AgentRate", "AgentAmount", "PersonalRate", "PersonalAmount", "UpdateTime", "Note" } },
			{ "Destination", new HashSet<string>() { "RegionId", "Cover", "PopularIndex", "Sequence", "Status", "UpdateTime", "Note" } },
			{ "Dict", new HashSet<string>() { "Code", "Name", "Value", "Sequence", "Status", "UpdateTime", "Note" } },
			{ "Menu", new HashSet<string>() { "Code", "Name", "Type", "PageUrl", "ApiUrl", "Icon", "Sequence", "Status", "UpdateTime", "Note" } },
			{ "Product", new HashSet<string>() { "Name", "Type", "Tags", "Themes", "Price", "Overview", "Sequence", "Recommends", "Status", "UpdateTime", "Note" } },
			{ "ProductDiscount", new HashSet<string>() { "Name", "Rate", "Amount", "Total", "StartTime", "EndTime", "Status", "UpdateTime", "Note" } },
			{ "Region", new HashSet<string>() { "Code", "Name", "FullName", "EnName", "Pinyin", "AreaCode", "ZipCode", "Sequence", "Status", "UpdateTime", "Note" } },
			{ "Role", new HashSet<string>() { "Code", "Name", "Status", "UpdateTime", "Note" } },
			{ "User", new HashSet<string>() { "Nickname", "FirstName", "LastName", "Email", "Mobile", "Tel", "Roles", "Status", "UpdateTime", "Note" } }
		};

		/// <summary>
		/// 获取属性名称
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static ICollection<string> GetPropertyNames<T>() where T : class, new()
		{
			var type = typeof(T);
			return GetPropertyNames(type);
		}

		/// <summary>
		/// 获取属性名称
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public static ICollection<string> GetPropertyNames(Type type)
		{
			lock (lockOfPropertyNames)
			{
				if (!propertyNames.ContainsKey(type.FullName))
				{
					var names = new HashSet<string>();
					var properties = ReflectionHelper.GetProperties(type);
					foreach (var property in properties)
					{
						if (property.Name == "Id" || property.Name == "SecretKey" || property.Name == "CreateTime")
						{
							continue;
						}
						//if (property.CustomAttributes.Any(o => o.AttributeType.FullName == "SqlSugar.SugarColumn"))
						//{
						//	continue;
						//}
						names.Add(property.Name);
					}
					propertyNames.Add(type.FullName, names);
				}
				return propertyNames[type.FullName];
			}
		}

		/// <summary>
		/// 转换为字典
		/// </summary>
		/// <param name="dicts"></param>
		/// <returns></returns>
		public static IDictionary<int, string> ToDictionary(this ICollection<Model.Role> list)
		{
			var dic = new Dictionary<int, string>();
			foreach (var data in list)
			{
				dic.Add(data.Id, data.Name);
			}
			return dic;
		}

		/// <summary>
		/// 转换为字典
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="list"></param>
		/// <returns></returns>
		public static IDictionary<int, string> ToDictionary<T>(this ICollection<T> list) where T : Model.TreeModel
		{
			var dic = new Dictionary<int, string>();
			foreach (var data in list)
			{
				dic.Add(data.Id, data.Name);
			}
			return dic;
		}

		/// <summary>
		/// 获取名称
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="list"></param>
		/// <param name="id"></param>
		/// <returns></returns>
		public static string GetName<T>(this ICollection<T> list, int id) where T : Model.TreeModel
		{
			if (id <= 0)
			{
				return "";
			}
			var data = list.Where(o => o.Id == id).FirstOrDefault();
			return data == null ? "" : data.Name;
		}

		/// <summary>
		/// 递归排序
		/// </summary>
		/// <param name="list"></param>
		/// <param name="parentId"></param>
		/// <returns></returns>
		public static ICollection<T> Recursive<T>(ICollection<T> list, int parentId) where T : Model.TreeModel<T>
		{
			var datas = list.Where(o => o.ParentId == parentId).OrderBy(o => o.Sequence).ThenBy(o => o.Id).ToList();
			foreach (var data in datas)
			{
				var children = list.Where(o => o.Parents.Contains(string.Format(",{0},", data.Id))).ToArray();
				if (children.Length == 0)
				{
					continue;
				}
				data.Children = Recursive(children, data.Id);
				data.Children = data.Children.OrderBy(o => o.Sequence).ThenBy(o => o.Id).ToArray();
			}
			return datas;
		}

		/// <summary>
		/// 默认排序
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="query"></param>
		/// <returns></returns>
		public static ISugarQueryable<T> Sort<T>(this ISugarQueryable<T> query) where T : Model.TreeModel<T>
		{
			return query.OrderBy(o => o.ParentId).OrderBy(o => o.Sequence).OrderBy(o => o.Id);
		}
	}
}
