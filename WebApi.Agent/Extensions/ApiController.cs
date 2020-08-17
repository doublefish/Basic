using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Localization;

namespace WebApi.Agent
{
	/// <summary>
	/// ApiController
	/// </summary>
	public class ApiController : WebApi.ApiController
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		public ApiController() : this(null)
		{
		}

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="factory"></param>
		public ApiController(IStringLocalizerFactory factory)
			: this(factory, null)
		{
		}

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="factory"></param>
		/// <param name="webHostEnvironment"></param>
		public ApiController(IStringLocalizerFactory factory, IWebHostEnvironment webHostEnvironment)
			: base(factory, webHostEnvironment)
		{
		}
	}
}