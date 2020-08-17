using System;

namespace Basic.Model
{
	/// <summary>
	/// SmsConfiguration
	/// </summary>
	public class SmsConfiguration
	{
		/// <summary>
		/// Url
		/// </summary>
		public string Url { get; set; }
		/// <summary>
		/// Account
		/// </summary>
		public string Account { get; set; }
		/// <summary>
		/// Password
		/// </summary>
		public string Password { get; set; }
		/// <summary>
		/// Signature
		/// </summary>
		public string Signature { get; set; }
		/// <summary>
		/// Expiry
		/// </summary>
		public TimeSpan Expiry { get; set; }
	}
}
