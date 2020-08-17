using Newtonsoft.Json;

namespace Adai.Standard
{
	/// <summary>
	/// JsonHelper
	/// </summary>
	public static class JsonHelper
	{
		/// <summary>
		/// Settings
		/// </summary>
		public static JsonSerializerSettings Settings = new JsonSerializerSettings
		{

			//NullValueHandling = NullValueHandling.Ignore,
			//DateFormatString = "yyyy-MM-dd HH:mm:ss.fff",
			//DateTimeZoneHandling = DateTimeZoneHandling.Utc,
			//PreserveReferencesHandling = PreserveReferencesHandling.Objects,
			//MissingMemberHandling = MissingMemberHandling.Ignore,
			ReferenceLoopHandling = ReferenceLoopHandling.Ignore
		};

		/// <summary>
		/// 序列化
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="obj"></param>
		/// <returns></returns>
		public static string SerializeObject<T>(T obj)
		{
			return JsonConvert.SerializeObject(obj, Settings);
		}

		/// <summary>
		/// 反序列化
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="json"></param>
		/// <returns></returns>
		public static T DeserializeObject<T>(string json)
		{
			return JsonConvert.DeserializeObject<T>(json, Settings);
		}
	}
}
