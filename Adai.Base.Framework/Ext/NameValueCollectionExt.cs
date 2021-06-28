﻿using System.Collections.Generic;
using System.Collections.Specialized;

namespace Adai.Base.Ext
{
	/// <summary>
	/// NameValueCollectionExt
	/// </summary>
	public static class NameValueCollectionExt
	{
		/// <summary>
		/// 转换为字典
		/// </summary>
		/// <param name="collection"></param>
		/// <returns></returns>
		public static IDictionary<string, string> ToDictionary(this NameValueCollection collection)
		{
			var dic = new Dictionary<string, string>();
			foreach (var key in collection.AllKeys)
			{
				dic.Add(key, collection.Get(key));
			}
			return dic;
		}
	}
}
