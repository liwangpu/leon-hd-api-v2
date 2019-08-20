#pragma warning disable 1591
using App.Base.API;
using App.Base.API.Infrastructure.Filters;
using App.Base.API.Infrastructure.Services;
using App.OSS.API.Infrastructure.AutofacModules;
using App.OSS.API.Infrastructure.Consts;
using App.OSS.API.Infrastructure.Services;
using App.OSS.Infrastructure;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.IO;

namespace App.OSS.API
{
    public class Startup
    {
        #region properties
        public IConfiguration Configuration { get; }
        public string Database => Configuration["DatabaseSettings:Database"];
        public string DBType => Configuration["DatabaseSettings:Type"];
        public string DBServer => Configuration["DatabaseSettings:Server"];
        public string DBPort => Configuration["DatabaseSettings:Port"];
        public string DBUserId => Configuration["DatabaseSettings:UserId"];
        public string DBPassword => Configuration["DatabaseSettings:Password"];
        public string JwtIssuer => Configuration["JwtSettings:Issuer"];
        public string JwtAudience => Configuration["JwtSettings:Audience"];
        public string JwtSecretKey => Configuration["JwtSettings:SecretKey"];
        public string ConsulServerIP => Configuration["ConsulSettings:Server:IP"];
        public string ConsulServerPort => Configuration["ConsulSettings:Server:Port"];
        public string ConsulClientIP => Configuration["ConsulSettings:Client:IP"];
        public string ConsulClientPort => Configuration["ConsulSettings:Client:Port"];
        #endregion

        #region ctor
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        #endregion

        #region ConfigureServices
        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
            services.AddHealthChecks();
            services.AddSingleton<Microsoft.Extensions.Hosting.IHostedService, DBMigrationService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<IUriService, UriService>();
            services.AddLocalization(o => o.ResourcesPath = "Resources");

            services.Configure<AppConfig>(Configuration);
            services.AddAutoMapper();
            services.AddCustomDbContext<OSSAppContext, Startup>(DBType, Database, DBServer, DBPort, DBUserId, DBPassword);
            services.AddJwtBearer(JwtIssuer, JwtAudience, JwtSecretKey);
            services.AddCustomSwagger();

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(HttpGlobalExceptionFilter));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            var container = new ContainerBuilder();
            container.Populate(services);

            container.RegisterModule(new MediatorModule());
            container.RegisterModule(new ApplicationModule());

            return new AutofacServiceProvider(container.Build());
        }
        #endregion

        #region Configure
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider, IApplicationLifetime lifetime)
        {
            app.UseCors("CorsPolicy");
            app.UseHealthChecks($"/{OSSConst.AppRouteArea}/healths");
            app.UseAuthentication();
            app.UseStaticFileServer(env);
            app.UseCustomSwaggerUI(OSSConst.AppRouteArea);
            app.UseLocalization();
            app.RegisterConsul(lifetime, ConsulServerIP, ConsulServerPort, ConsulClientIP, ConsulClientPort, OSSConst.AppRouteArea);
            app.InitDatabase(env, serviceProvider);
            app.UseMvc();
        }
        #endregion
    }


    public static class CustomExtensions
    {
        #region ex IApplicationBuilder InitDatabase 初始化数据库
        /// <summary>
        /// 初始化数据库
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static IApplicationBuilder InitDatabase(this IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            var dbContext = serviceProvider.GetService<OSSAppContext>();
            var appConfig = serviceProvider.GetService<IOptions<AppConfig>>().Value;


            dbContext.Database.Migrate();


            return app;
        }
        #endregion

        #region ex IApplicationBuilder UseStaticFileServer 配置静态文件服务
        /// <summary>
        /// 配置静态文件服务
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseStaticFileServer(this IApplicationBuilder app, IHostingEnvironment env)
        {
            if (Directory.Exists(env.WebRootPath) == false)
            {
                if (string.IsNullOrEmpty(env.WebRootPath))
                {
                    env.WebRootPath = Path.Combine(env.ContentRootPath, "wwwroot");
                }
                Directory.CreateDirectory(env.WebRootPath);
            }

            var tmpFolder = Path.Combine(env.WebRootPath, OSSConst.TmpFolder);
            if (!Directory.Exists(tmpFolder))
                Directory.CreateDirectory(tmpFolder);
            var clientAssetFolder = Path.Combine(env.WebRootPath, OSSConst.ClientAssetFolder);
            if (!Directory.Exists(clientAssetFolder))
                Directory.CreateDirectory(clientAssetFolder);
            var srcClientAssetFolder = Path.Combine(env.WebRootPath, OSSConst.SrcClientAssetFolder);
            if (!Directory.Exists(srcClientAssetFolder))
                Directory.CreateDirectory(srcClientAssetFolder);
            var iconFolder = Path.Combine(env.WebRootPath, OSSConst.IconFolder);
            if (!Directory.Exists(iconFolder))
                Directory.CreateDirectory(iconFolder);

            // default wwwroot directory
            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    ServeUnknownFileTypes = true,
            //    OnPrepareResponse = ctx =>
            //    {
            //        if (ctx.Context.Response.Headers.ContainsKey("Content-Type") == false)
            //            ctx.Context.Response.Headers.Add("Content-Type", "application/octet-stream");
            //    }
            //});
            app.UseStaticFiles(new StaticFileOptions
            {
                ServeUnknownFileTypes = true,
                OnPrepareResponse = ctx =>
                {
                    ctx.Context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                    if (ctx.Context.Response.Headers.ContainsKey("Content-Type") == false)
                        ctx.Context.Response.Headers.Add("Content-Type", "application/octet-stream");
                },
                FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(clientAssetFolder),
                RequestPath = $"/{OSSConst.AppRouteArea}/{OSSConst.ClientAssetFolder}"
            });
            app.UseStaticFiles(new StaticFileOptions
            {
                ServeUnknownFileTypes = true,
                OnPrepareResponse = ctx =>
                {
                    ctx.Context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                    if (ctx.Context.Response.Headers.ContainsKey("Content-Type") == false)
                        ctx.Context.Response.Headers.Add("Content-Type", "application/octet-stream");
                },
                FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(srcClientAssetFolder),
                RequestPath = $"/{OSSConst.AppRouteArea}/{OSSConst.SrcClientAssetFolder}"
            });
            app.UseStaticFiles(new StaticFileOptions
            {
                ServeUnknownFileTypes = true,
                OnPrepareResponse = ctx =>
                {
                    ctx.Context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                    if (ctx.Context.Response.Headers.ContainsKey("Content-Type") == false)
                        ctx.Context.Response.Headers.Add("Content-Type", "application/octet-stream");
                },
                FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(iconFolder),
                RequestPath = $"/{OSSConst.AppRouteArea}/{OSSConst.IconFolder}"
            });
            return app;
        }
        #endregion
    }
}
