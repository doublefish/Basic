using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace WebApi
{
	/// <summary>
	/// Startup
	/// </summary>
	public class Startup
	{
		/// <summary>
		/// ���캯��
		/// </summary>
		/// <param name="configuration"></param>
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		/// <summary>
		/// Configuration
		/// </summary>
		public IConfiguration Configuration { get; }
		/// <summary>
		/// ����
		/// </summary>
		readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

		/// <summary>
		/// This method gets called by the runtime. Use this method to add services to the container.
		/// </summary>
		/// <param name="services"></param>
		public virtual void ConfigureServices(IServiceCollection services)
		{
			//��ӱ��ػ�
			services.AddLocalization(options => options.ResourcesPath = "Languages");

			services.AddControllers()
				.AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
				.AddDataAnnotationsLocalization();

			//���ת��ͷ�м��
			services.Configure<ForwardedHeadersOptions>(options =>
			{
				options.ForwardedHeaders =
					ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
			});

			services.Configure<RequestLocalizationOptions>(options =>
			{
				var supportedCultures = new List<CultureInfo>
					{
						new CultureInfo("en-US"),
						new CultureInfo("zh-CN")
					};

				options.DefaultRequestCulture = new RequestCulture("en-US", "en-US");
				options.SupportedCultures = supportedCultures;
				options.SupportedUICultures = supportedCultures;
			});

			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			//services.AddSession(options =>
			//{
			//	// Set a short timeout for easy testing.
			//	options.IdleTimeout = TimeSpan.FromSeconds(10);
			//	options.Cookie.Name = ".AdventureWorks.Session";
			//	options.Cookie.HttpOnly = true;
			//	// Make the session cookie essential
			//	options.Cookie.IsEssential = true;
			//});
			//services.AddAuthorization();

			//����
			services.AddCors(options =>
			{
				options.AddPolicy(MyAllowSpecificOrigins, builder =>
				{
					builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
				});
			});

			//ע��Swagger������������һ���Ͷ��Swagger �ĵ�
			services.AddSwaggerGen(options =>
			{
				AddSwaggerGen(options);

				var type = GetType();
				var currentDirectory = Path.GetDirectoryName(type.Assembly.Location);

				var xmlPath = string.Format("{0}{1}.xml", AppDomain.CurrentDomain.BaseDirectory, type.Namespace);
				//�˴��滻�������ɵ�XML���ļ���
				options.IncludeXmlComments(xmlPath);
				options.OperationFilter<SwaggerOperationFilter>();
				options.DocumentFilter<SwaggerDocumentFilter>();
			});
		}

		/// <summary>
		/// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		/// </summary>
		/// <param name="app"></param>
		/// <param name="env"></param>
		public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			//�����ȡ���body
			app.Use(next => context =>
			{
				context.Request.EnableBuffering();
				return next(context);
			});

			// WebRootPath == null workaround.
			if (env != null && string.IsNullOrEmpty(env.WebRootPath))
			{
				env.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
			}

			//����ת��ͷ�м��
			app.UseForwardedHeaders();

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			app.UseMiddleware<ExceptionHandlerMiddleware>();

			//����Https
			//app.UseHttpsRedirection();
			//����Session
			//app.UseSession();
			//����Routing
			app.UseRouting();
			//���ÿ���
			app.UseCors(MyAllowSpecificOrigins);
			//����StaticFiles
			app.UseStaticFiles();
			//����CookiePolicy
			//app.UseCookiePolicy();

			//ʹ�м����������Swagger��ΪJSON�˵�
			app.UseSwagger();
			//�����м�����ṩ�û����棨HTML��js��CSS�ȣ����ر���ָ��JSON�˵�
			app.UseSwaggerUI(options =>
			{
				UseSwaggerUI(options);
				options.ShowExtensions();
				//options.RoutePrefix = string.Empty;
			});

			//���ñ��ػ�
			var supportedCultures = new[]
			{
				new CultureInfo("en-US"),
				new CultureInfo("zh-CN"),
			};

			app.UseRequestLocalization(new RequestLocalizationOptions
			{
				DefaultRequestCulture = new RequestCulture("en-US"),
				// Formatting numbers, dates, etc.
				SupportedCultures = supportedCultures,
				// UI strings that we have localized.
				SupportedUICultures = supportedCultures
			});

			//����Mvc
			app.UseEndpoints(endpoints =>
			{
				//endpoints.MapControllers();
				endpoints.MapControllerRoute("default_api", "api/{controller=Home}/{action=Index}/{id?}");
				endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
				//����״�����
				//endpoints.MapHealthChecks("/health", new HealthCheckOptions() { });
			});

			//ע�����
			Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
		}

		/// <summary>
		/// AddSwaggerGen
		/// </summary>
		/// <param name="options"></param>
		public virtual void AddSwaggerGen(SwaggerGenOptions options)
		{
			options.SwaggerDoc("public", new OpenApiInfo { Title = "����", Version = "v1", Description = "By Adai" });
		}

		/// <summary>
		/// ConfigureSwaggerGen
		/// </summary>
		/// <param name="options"></param>
		public virtual void UseSwaggerUI(SwaggerUIOptions options)
		{
			options.SwaggerEndpoint("/swagger/public/swagger.json", "�ӿ��ĵ�-����");
		}
	}
}
