using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace WebApi.Foreground
{
	/// <summary>
	/// Startup
	/// </summary>
	public class Startup : WebApi.Startup
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="configuration"></param>
		public Startup(IConfiguration configuration) : base(configuration)
		{
		}

		/// <summary>
		/// This method gets called by the runtime. Use this method to add services to the container.
		/// </summary>
		/// <param name="services"></param>
		public override void ConfigureServices(IServiceCollection services)
		{
			base.ConfigureServices(services);
		}

		/// <summary>
		/// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		/// </summary>
		/// <param name="app"></param>
		/// <param name="env"></param>
		public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			base.Configure(app, env);
		}

		/// <summary>
		/// AddSwaggerGen
		/// </summary>
		/// <param name="options"></param>
		public override void AddSwaggerGen(SwaggerGenOptions options)
		{
			options.SwaggerDoc("business", new OpenApiInfo { Title = "业务模块", Version = "v1", Description = "By Adai" });
			options.SwaggerDoc("account", new OpenApiInfo { Title = "会员模块", Version = "v1", Description = "By Adai" });
			base.AddSwaggerGen(options);
		}

		/// <summary>
		/// ConfigureSwaggerGen
		/// </summary>
		/// <param name="options"></param>
		public override void UseSwaggerUI(SwaggerUIOptions options)
		{
			options.SwaggerEndpoint("/swagger/business/swagger.json", "接口文档-业务");
			options.SwaggerEndpoint("/swagger/account/swagger.json", "接口文档-会员");
			base.UseSwaggerUI(options);
		}
	}
}
