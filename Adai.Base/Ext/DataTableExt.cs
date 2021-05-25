using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Adai.Base.Ext
{
	/// <summary>
	/// DataTableExt
	/// </summary>
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
			var columns = type.GetMappingRelations();
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
			var columns = type.GetMappingRelations();
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
		private static void SetValue<T>(T data, string name, object value, ICollection<Attribute.TableColumnAttribute> columns) where T : class
		{
			var column = columns.Get(name);
			if (column == null || column.PropertyInfo == null)
			{
			}
			else if (value == DBNull.Value)
			{
				column.PropertyInfo.SetValue(data, default);
			}
			else
			{
				data.SetValue(column.PropertyInfo, value);
			}
		}

		/// <summary>
		/// 读取映射
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static ICollection<Attribute.TableColumnAttribute> GetMappingRelations<T>()
		{
			return typeof(T).GetMappingRelations();
		}

		/// <summary>
		/// 读取映射关系
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public static ICollection<Attribute.TableColumnAttribute> GetMappingRelations(this Type type)
		{
			var typeOfColumn = typeof(Attribute.TableColumnAttribute);
			var properties = type.GetProperties();
			var list = new List<Attribute.TableColumnAttribute>();
			foreach (var pi in properties)
			{
				var tableColumnAttrs = pi.GetCustomAttributes(typeOfColumn, true);
				if (tableColumnAttrs == null || tableColumnAttrs.Length == 0)
				{
					continue;
				}
				var tableColumnAttr = tableColumnAttrs.FirstOrDefault() as Attribute.TableColumnAttribute;
				tableColumnAttr.PropertyInfo = pi;
				list.Add(tableColumnAttr);
			}
			return list;
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="attributes"></param>
		/// <param name="name"></param>
		/// <returns></returns>
		public static Attribute.TableColumnAttribute Get(this ICollection<Attribute.TableColumnAttribute> attributes, string name)
		{
			Attribute.TableColumnAttribute attribute = null;
			foreach (var attr in attributes)
			{
				if (string.Compare(attr.Name, name, StringComparison.OrdinalIgnoreCase) == 0)
				{
					attribute = attr;
					break;
				}
				if (string.Compare(attr.PropertyInfo.Name, name, StringComparison.OrdinalIgnoreCase) == 0)
				{
					attribute = attr;
					break;
				}
			}
			return attribute;
		}
	}
}
