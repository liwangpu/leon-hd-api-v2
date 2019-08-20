using App.Base.API.Infrastructure.Consts;
using App.Base.API.Infrastructure.Libraries;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace App.Base.API
{
    public static class ServiceCollectionExtension
    {

        public static IServiceCollection AddCustomDbContext<Context, Startup>(this IServiceCollection services, string dbType, string database, string server, string port, string userId, string password)
            where Context: DbContext
        {
            services.AddEntityFrameworkNpgsql()
                .AddDbContext<Context>(options =>
                {
                    var connectionString = DbConnectionStringBuilder.Build(dbType, server, port, database, userId, password);

                    if (dbType == AppDatabaseConst.Postgres)
                    {
                        options.UseNpgsql(connectionString, sqlContextOptionsBuilder =>
                        {
                            sqlContextOptionsBuilder.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                        });
                    }
                    else if (dbType == AppDatabaseConst.SQLServer)
                    {
                        options.UseSqlServer(connectionString, sqlContextOptionsBuilder =>
                        {
                            sqlContextOptionsBuilder.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                        });
                    }
                    else if (dbType == AppDatabaseConst.MySQL)
                    {
                        options.UseMySQL(connectionString, sqlContextOptionsBuilder =>
                        {
                            sqlContextOptionsBuilder.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                        });
                    }
                    else { }
                    options.UseNpgsql(connectionString, sqlContextOptionsBuilder =>
                     {
                         sqlContextOptionsBuilder.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                     });
                }, ServiceLifetime.Scoped);
            return services;
        }

        public static IServiceCollection AddJwtBearer(this IServiceCollection services, string issuer, string audience, string secretKey)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                    ClockSkew = TimeSpan.Zero
                };
            });
            return services;
        }

        public static IServiceCollection AddCustomSwagger(this IServiceCollection services, string xmlFile = "App.API.Description.xml")
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "API",
                    Description = "App API Description"
                });

                // Swagger 2.+ support
                var security = new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[] { }},
                };

                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
                c.AddSecurityRequirement(security);
                c.AddFluentValidationRules();

                // Set the comments path for the Swagger JSON and UI.
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            return services;
        }
    }
}
