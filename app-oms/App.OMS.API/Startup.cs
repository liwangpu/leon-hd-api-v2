#pragma warning disable 1591
using App.Base.API;
using App.Base.API.Infrastructure.Filters;
using App.MoreJee.Export;
using App.OMS.API.Infrastructure.AutofacModules;
using App.OMS.API.Infrastructure.Consts;
using App.OMS.API.Infrastructure.Services;
using App.OMS.Infrastructure;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace App.OMS.API
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

            services.Configure<AppConfig>(Configuration);
            services.AddSingleton<Microsoft.Extensions.Hosting.IHostedService, DBMigrationService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddLocalization(o => o.ResourcesPath = "Resources");
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(HttpGlobalExceptionFilter));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddScoped(provider =>
            {
                var options = provider.GetService<IOptions<AppConfig>>();
                var auth = provider.GetService<IHttpContextAccessor>()?.HttpContext?.Request?.Headers["Authorization"];
                return new ProductService(options.Value.APIServer, auth);
            });
            services.AddScoped(provider =>
            {
                var options = provider.GetService<IOptions<AppConfig>>();
                var auth = provider.GetService<IHttpContextAccessor>()?.HttpContext?.Request?.Headers["Authorization"];
                return new ProductSpecService(options.Value.APIServer, auth);
            });

            services.AddAutoMapper();
            services.AddCustomDbContext<OMSAppContext, Startup>(DBType, Database, DBServer, DBPort, DBUserId, DBPassword);
            services.AddJwtBearer(JwtIssuer, JwtAudience, JwtSecretKey);
            services.AddCustomSwagger();

            var container = new ContainerBuilder();
            container.Populate(services);

            container.RegisterModule(new MediatorModule());
            container.RegisterModule(new ApplicationModule());

            return new AutofacServiceProvider(container.Build());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IApplicationLifetime lifetime, IHostingEnvironment env)
        {
            app.UseCors("CorsPolicy");
            app.UseHealthChecks($"/{OMSConst.AppRouteArea}/healths");
            app.UseAuthentication();
            app.UseCustomSwaggerUI(OMSConst.AppRouteArea);
            app.UseLocalization();
            app.RegisterConsul(lifetime, ConsulServerIP, ConsulServerPort, ConsulClientIP, ConsulClientPort, OMSConst.AppRouteArea);
            app.UseMvc();
        }
    }
}
