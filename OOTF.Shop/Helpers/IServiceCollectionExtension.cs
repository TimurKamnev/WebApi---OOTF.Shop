using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using OOTF.Shopping.Context;
using OOTF.Shopping.Models;
using OOTF.Shopping.Services;

namespace OOTF.Shopping.Helpers;

internal static class IServiceCollectionExtension
{

    internal static void RegisterConnectionString(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(x => x.UseSqlServer(configuration.GetConnectionString("ConnectionString")));
    }

    //internal static void RegisterAuth(this IServiceCollection services)
    //{
    //    services.AddIdentityCore<User>(x =>
    //    {
    //        x.Password.RequiredLength = 6;
    //        x.Password.RequireLowercase = false;
    //        x.Password.RequireUppercase = false;
    //        x.Password.RequireNonAlphanumeric = false;
    //        x.Password.RequireDigit = false;
    //    })
    //    .AddRoles<Role>()
    //        .AddEntityFrameworkStores<AppDbContext>()
    //        .AddDefaultTokenProviders();
    //}

    //internal static void RegisterJwtAuthorization(this IServiceCollection services, IConfiguration configuration)
    //{
    //    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    //        .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    //        {
    //            options.RequireHttpsMetadata = false;
    //            options.SaveToken = true;
    //            options.TokenValidationParameters = JwtBuilder.Parameters(configuration);
    //        });
    //}

    internal static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<AppDbContext>();

        services.AddTransient<AuthService>();

    }

    internal static void RegisterSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(x =>
        {
            x.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "worbolt api",
                Version = "v1",
            });
            x.DescribeAllParametersInCamelCase();

            x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter JWT with Bearer into field",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
            });

            x.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer",
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                    },
                    new List<string>()
                },
            });
        });
    }
}
