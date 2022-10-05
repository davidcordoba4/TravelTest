using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebTravelTest.Core.Interfaces;
using WebTravelTest.Core.Services;
using WebTravelTest.Entities.DbContexts;
using WebTravelTest.Repositories.Interfaces;
using WebTravelTest.Repositories.Repositories;

namespace WebTravelTest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
                Configuration.GetConnectionString("DefaultConnection"),
                ef => ef.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
            services.AddTransient<IAutoresHasLibrosService, AutoresHasLibrosService>();
            services.AddTransient<IAutoresService, AutoresService>();
            services.AddTransient<IEditorialesService, EditorialesService>();
            services.AddTransient<ILibrosService, LibrosService>();
            services.AddTransient<IAutoresHasLibrosRepository, AutoresHasLibrosRepository>();
            services.AddTransient<IAutoresRepository, AutoresRepository>();
            services.AddTransient<IEditorialesRepository, EditorialesRepository>();
            services.AddTransient<ILibrosRepository, LibrosRepository>();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.Use(async (context, next) => { context.Response.Headers.Add("X-XSS-Protection", "1; mode=block"); context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
                context.Request.Headers.Add("X-XSS-Protection", "1; mode=block"); context.Request.Headers.Add("X-Content-Type-Options", "nosniff"); await next(); });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Libros/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Libros}/{action=Index}/{id?}");
            });
        }
    }
}
