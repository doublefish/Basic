﻿using Adai.Base.Ext;
using System;
using System.Collections.Generic;
using System.Data;

namespace Adai.Base
{
	/// <summary>
	/// DataTableHelper
	/// </summary>
	public static class DataTableHelper
	{
		/// <summary>
		/// ToList
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="dataTable"></param>
		/// <param name="tableColumns"></param>
		/// <returns></returns>
		public static ICollection<T> ToList<T>(DataTable dataTable, Attribute.TableColumnAttribute[] tableColumns) where T : class
		{
			var list = new List<T>();
			foreach (DataRow dataRow in dataTable.Rows)
			{
				var data = Activator.CreateInstance<T>();
				foreach (DataColumn dataColumn in dataTable.Columns)
				{
					var name = dataColumn.ColumnName;
					var value = dataRow[name];
					SetValue(data, name, value, tableColumns);
				}
				list.Add(data);
			}
			return list;
		}

		/// <summary>
		/// ToList
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="dataReader"></param>
		/// <param name="tableColumns"></param>
		/// <returns></returns>
		public static ICollection<T> ToList<T>(IDataReader dataReader, Attribute.TableColumnAttribute[] tableColumns) where T : class
		{
			var list = new List<T>();
			while (dataReader.Read())
			{
				var data = Activator.CreateInstance<T>();
				for (var i = 0; i < dataReader.FieldCount; i++)
				{
					var name = dataReader.GetName(i);
					var value = dataReader[name];
					SetValue(data, name, value, tableColumns);
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
		static void SetValue<T>(T data, string name, object value, ICollection<Attribute.TableColumnAttribute> columns) where T : class
		{
			var column = columns.GetByName(name);
			if (column == null || column.Property == null)
			{
			}
			else if (value == DBNull.Value)
			{
				column.Property.SetValue(data, default);
			}
			else
			{
				data.SetValue(column.Property, value);
			}
		}
	}
}
