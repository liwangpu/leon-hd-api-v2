using App.Base.API;
using App.Base.API.Infrastructure.Filters;
using App.Basic.Export;
using App.MoreJee.API.Infrastructure.AutofacModules;
using App.MoreJee.API.Infrastructure.Consts;
using App.MoreJee.API.Infrastructure.Services;
using App.MoreJee.Infrastructure;
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

namespace App.MoreJee.API
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

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


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

            services.AddScoped(provider =>
            {
                var options = provider.GetService<IOptions<AppConfig>>();
                var auth = provider.GetService<IHttpContextAccessor>()?.HttpContext?.Request?.Headers["Authorization"];
                return new AccountService(options.Value.APIServer, auth);
            });

            services.AddScoped(provider =>
            {
                var options = provider.GetService<IOptions<AppConfig>>();
                var auth = provider.GetService<IHttpContextAccessor>()?.HttpContext?.Request?.Headers["Authorization"];
                return new OrganizationService(options.Value.APIServer, auth);
            });

            services.AddScoped(provider =>
            {
                var options = provider.GetService<IOptions<AppConfig>>();
                var auth = provider.GetService<IHttpContextAccessor>()?.HttpContext?.Request?.Headers["Authorization"];
                return new AcessPointKeyService(options.Value.APIServer, auth);
            });

            services.AddLocalization(o => o.ResourcesPath = "Resources");
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(HttpGlobalExceptionFilter));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddAutoMapper();
            services.AddCustomDbContext<MoreJeeAppContext, Startup>(DBType, Database, DBServer, DBPort, DBUserId, DBPassword);
            services.AddJwtBearer(JwtIssuer, JwtAudience, JwtSecretKey);
            services.AddCustomSwagger();

            var container = new ContainerBuilder();
            container.Populate(services);

            container.RegisterModule(new MediatorModule());
            container.RegisterModule(new ApplicationModule());

            return new AutofacServiceProvider(container.Build());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider, IApplicationLifetime lifetime)
        {
            app.UseCors("CorsPolicy");
            app.UseHealthChecks($"/{MorejeeConst.AppRouteArea}/healths");
            app.UseAuthentication();
            app.UseCustomSwaggerUI(MorejeeConst.AppRouteArea);
            app.UseLocalization();
            app.RegisterConsul(lifetime, ConsulServerIP, ConsulServerPort, ConsulClientIP, ConsulClientPort, MorejeeConst.AppRouteArea);
            app.UseMvc();
        }
    }

}
