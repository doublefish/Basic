﻿using System.Collections.Generic;

namespace Adai.Standard.Ext
{
	/// <summary>
	/// DictionaryExt
	/// </summary>
	public static class SortedDictionaryExt
	{
		/// <summary>
		/// 类型转换
		/// </summary>
		/// <typeparam name="TKey"></typeparam>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="sorted"></param>
		/// <returns></returns>
		public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this SortedDictionary<TKey, TValue> sorted)
		{
			var dic = new Dictionary<TKey, TValue>();
			foreach (var kv in sorted)
			{
				dic.Add(kv.Key, kv.Value);
			}
			return dic;
		}
	}
}
