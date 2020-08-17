using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace WebApi.Common.Controllers
{
	/// <summary>
	/// HomeController
	/// </summary>
	[Route("api"), ApiExplorerSettings(GroupName = "public")]
	public class HomeController : ApiController
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="factory"></param>
		/// <param name="webHostEnvironment"></param>
		public HomeController(IStringLocalizerFactory factory, IWebHostEnvironment webHostEnvironment) : base(factory, webHostEnvironment)
		{
		}
	}
}