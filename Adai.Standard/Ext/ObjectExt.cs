using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Adai.Standard.Ext
{
	/// <summary>
	/// ObjectExt
	/// </summary>
	public static class ObjectExt
	{
		/// <summary>
		/// 克隆
		/// </summary>
		/// <typeparam name="T">类型</typeparam>
		/// <param name="source">要克隆的属性的访问级别</param>
		/// <param name="ignores">忽略的属性</param>
		public static T Clone<T>(this T source, params string[] ignores) where T : class, new()
		{
			return source.Clone(BindingFlags.Default, ignores);
		}

		/// <summary>
		/// 克隆
		/// </summary>
		/// <typeparam name="T">类型</typeparam>
		/// <param name="source"></param>
		/// <param name="bindingAttr">要克隆的属性的访问级别</param>
		/// <param name="ignores">忽略的属性</param>
		public static T Clone<T>(this T source, BindingFlags bindingAttr, params string[] ignores) where T : class, new()
		{
			var type = typeof(T);
			var data = new T();
			foreach (var propertyInfo in type.GetProperties(bindingAttr))
			{
				if (ignores.Contains(propertyInfo.Name))
				{
					continue;
				}
				type.GetProperty(propertyInfo.Name).SetValue(data, propertyInfo.GetValue(source, null), null);
			}
			return data;
		}

		/// <summary>
		/// GetValue
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="obj"></param>
		/// <param name="type"></param>
		/// <param name="propertyName"></param>
		/// <returns></returns>
		public static object GetValue<T>(this T obj, string propertyName, Type type = null) where T : class
		{
			if (propertyName.IndexOf(".") > -1)
			{
				var prefix = propertyName.Split('.')[0];//前缀
				var value = obj.GetValue(prefix, type);
				if (value == null)
				{
					return null;
				}
				var suffix = propertyName.Substring(prefix.Length + 1);//后缀
				return value.GetValue(suffix, value.GetType());
			}
			else
			{
				if (type == null)
				{
					type = obj.GetType();
				}
				var property = type.GetProperty(propertyName);
				if (property == null || !property.CanRead)
				{
					throw new ArgumentException("Without this property or unreadable.");
				}
				return property.GetValue(obj);
			}
		}

		/// <summary>
		/// SetValue
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source">数据源</param>
		/// <param name="separator">分隔符</param>
		/// <param name="type">要赋值的对象的类型</param>
		/// <param name="obj">要赋值的对象</param>
		public static void SetValue<T>(this T obj, IDictionary<string, string> source, char separator, Type type)
		{
			var dictionary0 = new Dictionary<string, string>();
			var dictionary1 = new Dictionary<string, string>();

			foreach (var kv in source)
			{
				if (kv.Key.IndexOf(separator) == -1)
				{
					dictionary0.Add(kv.Key, kv.Value);
				}
				else
				{
					dictionary1.Add(kv.Key, kv.Value);
				}
			}

			//非 class 属性 直接赋值
			if (dictionary0.Keys.Count > 0)
			{
				//var propertys = type.GetProperties();
				foreach (var kv in dictionary0)
				{
					var value = dictionary0[kv.Key];
					var property = type.GetProperty(kv.Key);
					if (property == null || !property.CanWrite)
					{
						continue;
					}
					property.SetValue(obj, value);
				}
			}

			if (dictionary1.Keys.Count > 0)
			{
				var childDictionary = new Dictionary<string, Dictionary<string, string>>();
				foreach (var kv in dictionary1)
				{
					var prefix = kv.Key.Split(separator)[0];//前缀
					var name = kv.Key.Substring(prefix.Length + 1);//后缀
					if (string.IsNullOrEmpty(prefix) || string.IsNullOrEmpty(name))
					{
						continue;
					}
					if (!childDictionary.ContainsKey(prefix))
					{
						childDictionary.Add(prefix, new Dictionary<string, string>());
					}
					if (childDictionary[prefix].ContainsKey(name))//相同key跳过，以第一个为准
					{
						continue;
					}
					childDictionary[prefix].Add(name, kv.Value);
				}
				foreach (var kv in childDictionary)
				{
					var property = type.GetProperty(kv.Key);
					if (property == null)
					{
						continue;
					}
					if (!property.CanWrite)
					{
						continue;
					}
					var childObj = type.GetProperty(kv.Key).GetValue(obj);
					childObj.SetValue(kv.Value, separator, property.PropertyType);
					property.SetValue(obj, childObj, null);
				}
			}
		}
	}
}
