using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace OOTF.Shopping.Helpers;

internal static class WebApplicationBuilderExtension
{
    internal static void RegisterSerilog(this WebApplicationBuilder builder)
    {
        var logging = builder.Logging;
        logging.ClearProviders();

        var env = builder.Environment.EnvironmentName;
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false, true)
            .AddJsonFile($"appsettings.{env}.json", true)
            .Build();
        Console.WriteLine($"{nameof(env)} = '{env}'");

        var connectionString = configuration.GetConnectionString("ConnectionString");
    }

    internal static void ConfigureServices(this WebApplicationBuilder builder)
    {
        var services = builder.Services;
        var configuration = builder.Configuration;

        services.AddControllers(x =>
        {
            x.OutputFormatters.Clear();
            x.InputFormatters.Clear();
        });
        services.AddHttpContextAccessor();

        services.RegisterConnectionString(configuration);

        services.RegisterServices();

        services.RegisterSwagger();

        services.UseSwagger();

        services.UseSwaggerUI();
    }

    internal static WebApplication Configure(this WebApplicationBuilder builder)
    {
        var app = builder.Build();
        var env = builder.Environment;
        var configuration = builder.Configuration;

        if (env.IsDevelopment())
            app.UseDeveloperExceptionPage();


        app.UseHttpsRedirection();

        app.UseRouting();
        app.UseCors();

        app.UseAuthentication();
        app.UseAuthorization();


        app.UseEndpoints(x =>
        {
            x.MapControllers();
        });

        return app;
    }
}

