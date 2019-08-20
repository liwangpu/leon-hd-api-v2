using App.Base.API;
using App.Base.API.Infrastructure.Filters;
using App.Base.API.Infrastructure.Services;
using App.Basic.API.Infrastructure.AutofacModules;
using App.Basic.API.Infrastructure.Consts;
using App.Basic.API.Infrastructure.Extensions;
using App.Basic.API.Infrastructure.Services;
using App.Basic.Infrastructure;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace App.Basic.API
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
            services.AddSingleton<IDapperConnection>(new DapperConnection(DBType, DBServer, DBPort, Database, DBUserId, DBPassword));

            services.Configure<AppConfig>(Configuration);
            services.AddAutoMapper();
            //services.AddAutoMapper(cfg =>
            //{
            //    //cfg.ForAllMaps((map, exp) => exp.ForAllOtherMembers(opt => opt.Ignore()));
            //    cfg.IgnoreUnmapped();
            //});
            services.AddCustomDbContext<BasicAppContext, Startup>(DBType, Database, DBServer, DBPort, DBUserId, DBPassword);
            services.AddJwtBearer(JwtIssuer, JwtAudience, JwtSecretKey);
            services.AddCustomSwagger();
            services.AddLocalization(o => o.ResourcesPath = "Resources");

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(HttpGlobalExceptionFilter));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //use FluentValidation in Swagger
            // One more way to set custom factory.
            services = services.Replace(ServiceDescriptor.Scoped<IValidatorFactory, ScopedServiceProviderValidatorFactory>());

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
            app.UseHealthChecks($"/{BasicConst.AppRouteArea}/healths");
            app.UseAuthentication();
            app.UseCustomSwaggerUI(BasicConst.AppRouteArea);
            app.UseLocalization();
            app.RegisterConsul(lifetime, ConsulServerIP, ConsulServerPort, ConsulClientIP, ConsulClientPort, BasicConst.AppRouteArea);
            //app.InitDatabase(env, serviceProvider);
            //app.UseHealthCheck();
            app.UseMvc();
        }
        #endregion
    }


}
