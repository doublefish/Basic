using System;
using System.Collections.Generic;

namespace Adai.Base
{
	/// <summary>
	/// CustomAttributeHelper
	/// </summary>
	public static class CustomAttributeHelper
	{
		/// <summary>
		/// 读取类的属性的的特性
		/// </summary>
		/// <typeparam name="T">类的属性的特性的类型</typeparam>
		/// <param name="type">类的Type</param>
		/// <returns></returns>
		public static T[] GetPropertyAttributes<T>(Type type) where T : Attribute.CustomAttribute
		{
			var properties = type.GetProperties();
			var list = new List<T>();
			var typeA = typeof(T);
			foreach (var pi in properties)
			{
				var attrs = pi.GetCustomAttributes(typeA, true);
				if (attrs == null || attrs.Length == 0)
				{
					continue;
				}
				var attr = attrs[0] as T;
				attr.Property = pi;
				list.Add(attr);
			}
			return list.ToArray();
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="attributes"></param>
		/// <param name="name"></param>
		/// <returns></returns>
		public static T GetByName<T>(this ICollection<T> attributes, string name) where T : Attribute.CustomAttribute
		{
			T data = null;
			foreach (var attr in attributes)
			{
				if (string.Compare(attr.Name, name, StringComparison.OrdinalIgnoreCase) == 0)
				{
					data = attr;
					break;
				}
				if (string.Compare(attr.Property.Name, name, StringComparison.OrdinalIgnoreCase) == 0)
				{
					data = attr;
					break;
				}
			}
			return data;
		}
	}
}
