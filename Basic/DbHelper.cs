using Adai.Standard;
using Adai.Standard.Models;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;

namespace Basic
{
	/// <summary>
	/// DbHelper
	/// </summary>
	public static class DbHelper
	{
		/// <summary>
		/// DbType
		/// </summary>
		internal static string DbType => JsonConfigHelper.Configuration["DbType"].ToString();
		/// <summary>
		/// ConnectionString
		/// </summary>
		internal static string ConnectionString => JsonConfigHelper.ConnectionStrings[DbType];

		/// <summary>
		/// ConnectionConfig
		/// </summary>
		internal static ConnectionConfig ConnectionConfig => new ConnectionConfig()
		{
			ConnectionString = ConnectionString,
			DbType = DbType == "MSSQL" ? SqlSugar.DbType.SqlServer : SqlSugar.DbType.MySql,
			//开启自动释放模式和EF原理一样
			IsAutoCloseConnection = true
		};

		/// <summary>
		/// CreateDb
		/// </summary>
		/// <returns></returns>
		internal static SqlSugarClient CreateDb()
		{
			var client = new SqlSugarClient(ConnectionConfig);
			client.Ado.IsEnableLogEvent = true;
			//用来打印Sql方便你调式    
			client.Aop.OnLogExecuting = (sql, pars) =>
			{
				Console.WriteLine(sql + "\r\n" +
				client.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
				Console.WriteLine();
			};
			return client;
		}

		/// <summary>
		/// InitModel
		/// </summary>
		/// <param name="namespaceName"></param>
		/// <param name="path"></param>
		public static void InitModel(string namespaceName = "Basic.Model", string path = "C:\\SqlSugar\\Model")
		{
			using var db = new SqlSugarClient(ConnectionConfig);
			path = string.Format("{0}\\{1}", path, namespaceName);
			db.DbFirst.CreateClassFile(path, namespaceName);
		}

		/// <summary>
		/// 缓存的实体
		/// </summary>
		internal static string[] CacheModels
		{
			get
			{
				JsonConfigHelper.Configuration.TryGetValue("CacheModel", out var obj);
				if (obj == null)
				{
					return new string[] { };
				}
				return obj.ToString().Split(',');
			}
		}

		/// <summary>
		/// 是否需要缓存的实体
		/// </summary>
		/// <param name="modelName"></param>
		/// <returns></returns>
		internal static bool IsCacheModel(string modelName)
		{
			return Array.IndexOf(CacheModels, modelName) > -1;
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="connection"></param>
		/// <param name="sql"></param>
		internal static DataTable ExecuteQuery(SqlConnection connection, string sql)
		{
			var cmd = connection.CreateCommand();
			cmd.CommandText = sql;
			var sdr = cmd.ExecuteReader();
			var dt = new DataTable();
			dt.Load(sdr);
			sdr.Close();
			return dt;
		}

		/// <summary>
		/// 执行SQL语句
		/// </summary>
		/// <param name="connection"></param>
		/// <param name="sql"></param>
		internal static int ExecuteNonQuery(SqlConnection connection, string sql)
		{
			var cmd = connection.CreateCommand();
			cmd.CommandText = sql;
			return cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// 执行SQL语句
		/// </summary>
		/// <param name="connection"></param>
		/// <param name="sql"></param>
		internal static object ExecuteRead(SqlConnection connection, string sql)
		{
			var cmd = connection.CreateCommand();
			cmd.CommandText = sql;
			using var sdr = cmd.ExecuteReader();
			if (sdr.Read())
			{
				return sdr.GetValue(0);
			}
			return null;
		}

		/// <summary>
		/// 是否存在指定名称的表
		/// </summary>
		/// <param name="connection"></param>
		/// <param name="tableName"></param>
		/// <returns></returns>
		internal static bool ExistTable(SqlConnection connection, string tableName)
		{
			var sql = string.Format("SELECT [id] FROM dbo.sysobjects WHERE [id] = object_id(N'[dbo].[{0}]') AND OBJECTPROPERTY([id], N'IsUserTable') = 1;", tableName);
			var obj = ExecuteRead(connection, sql);
			return obj != null && !string.IsNullOrEmpty(obj.ToString());
		}

		/// <summary>
		/// 查询表结构
		/// </summary>
		/// <param name="connection"></param>
		/// <param name="tableName"></param>
		/// <returns></returns>
		internal static DataTable GetTableStructure(SqlConnection connection, string tableName)
		{
			var sql = @"
SELECT  表名 = CASE WHEN a.colorder = 1 THEN d.name ELSE '' END ,
        字段说明 = ISNULL(g.[value], '') ,
        字段名 = a.name ,
        类型 = CASE WHEN b.name IN ( 'varchar', 'nvarchar' )
       THEN b.name + '('
 + CAST(COLUMNPROPERTY(a.id, a.name, 'PRECISION') AS VARCHAR(4))
 + ')'
       WHEN b.name = 'decimal'
       THEN b.name + '('
 + CAST(COLUMNPROPERTY(a.id, a.name, 'PRECISION') AS VARCHAR(4))
 + ','
 + CAST(COLUMNPROPERTY(a.id, a.name, 'Scale') AS VARCHAR(4))
 + ')'
       ELSE b.name
  END
FROM syscolumns a    -- 列名
    LEFT JOIN systypes b ON a.xusertype = b.xusertype    -- 类型
    INNER JOIN sysobjects d ON a.id = d.id AND d.xtype = 'U' AND d.name <> 'dtproperties'    --筛选用户对象
    --LEFT JOIN syscomments e ON a.cdefault = e.id    --默认值
    LEFT JOIN sys.extended_properties g ON a.id = g.major_id AND a.colid = g.minor_id    --扩展属性(字段说明)
    --LEFT JOIN sys.extended_properties f ON d.id = f.major_id AND f.minor_id = 0        --扩展属性(表说明)
WHERE d.name = '" + tableName + @"'    --可修改表名
ORDER BY a.id , a.colorder
";
			return ExecuteQuery(connection, sql);
		}

		/// <summary>
		/// 筛选
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="query"></param>
		/// <param name="pkValue"></param>
		/// <param name="selector"></param>
		/// <returns></returns>
		internal static T InSingle<T>(this ISugarQueryable<T> query, object pkValue, Expression<Func<T, T>> selector = null)
		{
			return query.Filter(selector).InSingle(pkValue);
		}

		/// <summary>
		/// 筛选
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="query"></param>
		/// <param name="selector"></param>
		/// <returns></returns>
		internal static T First<T>(this ISugarQueryable<T> query, Expression<Func<T, T>> selector = null)
		{
			return query.Filter(selector).First();
		}

		/// <summary>
		/// 筛选
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="query"></param>
		/// <param name="expression"></param>
		/// <param name="selector"></param>
		/// <returns></returns>
		internal static T First<T>(this ISugarQueryable<T> query, Expression<Func<T, bool>> expression, Expression<Func<T, T>> selector = null)
		{
			return query.Filter(selector).First(expression);
		}

		/// <summary>
		/// 筛选
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="query"></param>
		/// <param name="selector"></param>
		/// <returns></returns>
		internal static IList<T> ToList<T>(this ISugarQueryable<T> query, Expression<Func<T, T>> selector = null)
		{
			return query.Filter(selector).ToList();
		}

		/// <summary>
		/// 筛选
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="query"></param>
		/// <param name="selector"></param>
		/// <returns></returns>
		internal static ISugarQueryable<T> Filter<T>(this ISugarQueryable<T> query, Expression<Func<T, T>> selector = null)
		{
			if (selector == null)
			{
				return query;
			}
			return query.Select(selector);
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="PageArg"></typeparam>
		/// <param name="query"></param>
		/// <param name="arg"></param>
		/// <param name="selector"></param>
		/// <returns></returns>
		internal static PageArg Page<T, PageArg>(this ISugarQueryable<T> query, PageArg arg, Expression<Func<T, T>> selector = null)
			where T : class, new()
			where PageArg : PageArg<T>
		{
			if (arg.CountFlag != StatisticFlag.Not)
			{
				arg.TotalCount = query.Count();
				if (arg.CountFlag == StatisticFlag.Only)
				{
					return arg;
				}
			}

			if (arg.PageSize > 0)
			{
				query = query.Skip(arg.PageSize * arg.PageNumber).Take(arg.PageSize);
			}
			if (selector != null)
			{
				arg.Results = query.Select(selector).ToList();
			}
			else
			{
				arg.Results = query.ToList();
			}
			return arg;
		}

		/// <summary>
		/// 添加查询条件
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="query"></param>
		/// <param name="fieldName"></param>
		/// <param name="collection"></param>
		/// <param name="format"></param>
		/// <returns></returns>
		internal static ISugarQueryable<T> WhereLike<T>(this ISugarQueryable<T> query, string fieldName, ICollection<int> collection, string format = "{0}")
			where T : class, new()
		{
			var conditionalList = new List<KeyValuePair<WhereType, ConditionalModel>>();
			foreach (var data in collection)
			{
				var str = string.Format(format, data);
				var item = new KeyValuePair<WhereType, ConditionalModel>(WhereType.Or, new ConditionalModel() { FieldName = fieldName, ConditionalType = ConditionalType.Like, FieldValue = str });
				conditionalList.Add(item);
			}
			var conditionalModels = new List<IConditionalModel>() { new ConditionalCollections() { ConditionalList = conditionalList } };
			query = query.Where(conditionalModels);
			return query;
		}

		/// <summary>
		/// 添加查询条件
		/// </summary>
		/// <param name="query"></param>
		/// <param name="fieldNames"></param>
		/// <param name="parameter"></param>
		/// <returns></returns>
		internal static ISugarQueryable<T> WhereConcatLike<T>(this ISugarQueryable<T> query, ICollection<string> fieldNames, string parameter)
			where T : class, new()
		{
			var whereString = "CONCAT(" + string.Join(",", fieldNames) + ") LIKE CONCAT('%',@parameter,'%')";
			return query.Where(whereString, new { parameter });
		}
	}
}
