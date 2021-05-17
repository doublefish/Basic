using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Adai.Standard.Ext
{
	public static class DataTableExt
	{
		/// <summary>
		/// ToList
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="dataTable"></param>
		/// <returns></returns>
		public static ICollection<T> ToList<T>(this DataTable dataTable) where T : class
		{
			var type = typeof(T);
			var columns = type.GetMappingProperties();
			var list = new List<T>();
			foreach (DataRow dataRow in dataTable.Rows)
			{
				var data = Activator.CreateInstance<T>();
				foreach (DataColumn dataColumn in dataTable.Columns)
				{
					var name = dataColumn.ColumnName;
					var value = dataRow[name];
					SetValue(data, name, value, columns);
				}
				list.Add(data);
			}
			return list;
		}

		/// <summary>
		/// ToList
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="dataTable"></param>
		/// <returns></returns>
		public static ICollection<T> ToList<T>(this IDataReader dataReader) where T : class
		{
			var type = typeof(T);
			var columns = type.GetMappingProperties();
			var list = new List<T>();
			while (dataReader.Read())
			{
				var data = Activator.CreateInstance<T>();
				for (var i = 0; i < dataReader.FieldCount; i++)
				{
					var name = dataReader.GetName(i);
					var value = dataReader[name];
					SetValue(data, name, value, columns);
				}
				list.Add(data);
			}
			return list;
		}

		/// <summary>
		/// 赋值
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="data"></param>
		/// <param name="name"></param>
		/// <param name="value"></param>
		/// <param name="columns"></param>
		private static void SetValue<T>(T data, string name, object value, IDictionary<string, PropertyInfo> columns) where T : class
		{
			columns.TryGetValue(name.ToUpper(), out var pi);
			if (pi == null)
			{
			}
			else if (value == DBNull.Value)
			{
				pi.SetValue(data, default);
			}
			else
			{
				data.SetValue(pi, value);
			}
		}

		/// <summary>
		/// 读取映射
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static IDictionary<string, PropertyInfo> GetMappingProperties<T>()
		{
			return typeof(T).GetMappingProperties();
		}

		/// <summary>
		/// 读取映射
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public static IDictionary<string, PropertyInfo> GetMappingProperties(this Type type)
		{
			var typeOfColumn = typeof(Models.TableColumnAttribute);
			var properties = type.GetProperties();
			var dic = new Dictionary<string, PropertyInfo>();
			foreach (var pi in properties)
			{
				var tableColumnAttrs = pi.GetCustomAttributes(typeOfColumn, true);
				if (tableColumnAttrs == null || tableColumnAttrs.Length == 0)
				{
					continue;
				}
				var tableColumnAttr = tableColumnAttrs.FirstOrDefault() as Models.TableColumnAttribute;
				dic.Add(tableColumnAttr.Name.ToUpper(), pi);
			}
			return dic;
		}
	}
}
