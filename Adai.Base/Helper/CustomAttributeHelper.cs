using System;
using System.Collections.Generic;
using System.Reflection;

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
		/// <typeparam name="T">类的类型</typeparam>
		/// <typeparam name="A">类的属性的特性的类型</typeparam>
		/// <returns></returns>
		public static A[] GetPropertyAttributes<T, A>() where T : class where A : Attribute.CustomAttribute
		{
			var properties = typeof(T).GetProperties();
			var list = new List<A>();
			var typeA = typeof(A);
			foreach (var pi in properties)
			{
				var attr = GetAttribute<A>(pi, typeA, true);
				if (attr == null)
				{
					continue;
				}
				attr.Property = pi;
				list.Add(attr);
			}
			return list.ToArray();
		}

		/// <summary>
		/// 获取自定义特性
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="property"></param>
		/// <param name="inherit"></param>
		/// <returns></returns>
		public static T GetAttribute<T>(PropertyInfo property, bool inherit) where T : Attribute.CustomAttribute
		{
			return GetAttribute<T>(property, typeof(T), inherit);
		}

		/// <summary>
		/// 获取自定义特性
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="property"></param>
		/// <param name="inherit"></param>
		/// <returns></returns>
		public static T GetAttribute<T>(PropertyInfo property, Type attributeType, bool inherit) where T : Attribute.CustomAttribute
		{
			var attrs = property.GetCustomAttributes(attributeType, inherit);
			if (attrs == null || attrs.Length == 0)
			{
				return null;
			}
			return attrs[0] as T;
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
