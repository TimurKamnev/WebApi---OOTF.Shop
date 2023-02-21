//using Microsoft.EntityFrameworkCore;
//using OOTF.Shopping.Context;
//using OOTF.Shopping.Services;
//using System.Configuration;

//namespace OOTF.Shopping
//{
//    public class StartUp
//    {
//        public IConfiguration Configuration { get; }

//        public StartUp(IConfiguration configuration)
//        {
//            Configuration = configuration;
//        }

//        public void ConfigureServices(this IServiceCollection services)
//        {
//            // Add framework services.
//            services.AddDbContext<AppDbContext>(x => x.UseSqlServer(configuration.GetConnectionString("Default")));
//            services.AddMvc();
//            services.AddScoped<AppDbContext>();
//            services.AddTransient<AuthService>();
//        }

//        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
//        {
//            if (env.IsDevelopment())
//            {
//                app.UseDeveloperExceptionPage();
//            }
//            else
//            {
//                app.UseExceptionHandler("/Home/Error");
//            }

//            app.UseStaticFiles();
//            app.Services();

//            app.UseMvc(routes =>
//            {
//                routes.MapRoute(
//                    name: "default",
//                    template: "{controller=Shop}/{action=GetShops}/{id?}");
//            });
//        }
//    }
//}
