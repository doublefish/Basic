using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApi.Foreground
{
	/// <summary>
	/// MvcController
	/// </summary>
	public class MvcController : WebApi.MvcController
	{
		/// <summary>
		/// Action 执行前执行
		/// </summary>
		/// <param name="context"></param>
		public override void OnActionExecuting(ActionExecutingContext context)
		{
			base.OnActionExecuting(context);
		}

		/// <summary>
		/// Action 执行后执行
		/// </summary>
		/// <param name="context"></param>
		public override void OnActionExecuted(ActionExecutedContext context)
		{
			base.OnActionExecuted(context);
		}

		/// <summary>
		/// 查找视图
		/// </summary>
		/// <param name="viewName"></param>
		/// <param name="model"></param>
		/// <returns></returns>
		public override ViewResult View(string viewName, object model)
		{
			return base.View(viewName, model);
		}
	}
}