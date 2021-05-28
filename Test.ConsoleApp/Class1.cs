using Adai.Base.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.ConsoleApp
{
	/// <summary>
	/// Class1
	/// </summary>
	[Table("table")]
	public class Class1
	{
		/// <summary>
		/// Id
		/// </summary>
		[TableColumn("id")]
		public int Id { get; set; }

		/// <summary>
		/// Name
		/// </summary>
		[TableColumn("name")]
		public string Name { get; set; }
	}
}
