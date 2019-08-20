using App.Base.API.Infrastructure.MiddleWares;
using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace App.Base.API
{
    public static class ApplicationBuilderExtension
    {
        /// <summary>
        /// 配置默认语言支持
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseLocalization(this IApplicationBuilder app)
        {

            app.UseMiddleware<RequestCultureMiddleware>();
            var supportedCultures = new List<CultureInfo>
             {
               new CultureInfo("en-US"),
               new CultureInfo("zh-Hans"),
               new CultureInfo("zh-Hant")
               };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("zh-Hans"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });
            return app;
        }

        public static IApplicationBuilder UseCustomSwaggerUI(this IApplicationBuilder app, string routeArea)
        {
            //prefix有大写直接启动不起来,让你郁闷到怀疑人生
            var prefix = $"{routeArea}/swagger".ToLower();
            app.UseSwagger(c =>
            {
                c.RouteTemplate = $"/{prefix}/" + "{documentName}/swagger.json";
            });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/{prefix}/v1/swagger.json", "API Description");
                c.RoutePrefix = prefix;
            });
            return app;
        }

        public static IApplicationBuilder RegisterConsul(this IApplicationBuilder app, IApplicationLifetime lifetime, string consulServerIP, string consulServerPort, string consulClientIP, string consulClientPort, string routeArea)
        {
            var consulClient = new ConsulClient(x => x.Address = new Uri($"http://{consulServerIP}:{consulServerPort}"));//请求注册的 Consul 地址
            var httpCheck = new AgentServiceCheck()
            {

                DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(2),//服务启动多久后注册

                Interval = TimeSpan.FromSeconds(10),//健康检查时间间隔，或者称为心跳间隔

                HTTP = $"http://{consulClientIP}:{consulClientPort}/{routeArea}/Healths",//健康检查地址

                Timeout = TimeSpan.FromSeconds(5)
            };

            var registration = new AgentServiceRegistration()
            {

                Checks = new[] { httpCheck },

                ID = $"app-{routeArea}-api-{consulClientIP}:{consulClientPort}",

                Name = $"app-{routeArea}-api",

                Address = $"{consulClientIP}",

                Port = Convert.ToInt32(consulClientPort),

                Tags = new[] { $"urlprefix-/app-{routeArea}-api" }//添加 urlprefix-/servicename 格式的 tag 标签，以便 Fabio 识别
            };

            consulClient.Agent.ServiceDeregister(registration.ID).Wait();
            consulClient.Agent.ServiceRegister(registration).Wait();//服务启动时注册，内部实现其实就是使用 Consul API 进行注册（HttpClient发起）

            lifetime.ApplicationStopping.Register(() =>
            {
                consulClient.Agent.ServiceDeregister(registration.ID).Wait();//服务停止时取消注册
            });
            return app;
        }



    }
}
