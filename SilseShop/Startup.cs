using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SilseShop.Database;
using SilseShop.Models;
using SilseShop.Services;

namespace SilseShop
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
            services.AddMvc();
            services.AddDbContext<ShopDbContext>(options => options.UseNpgsql(Configuration["ConnectionString"]));
            services.AddDbContext<UsersDbContext>(options => options.UseNpgsql(Configuration["ConnectionString"]));
            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<UsersDbContext>();
            services.AddScoped<ProductRepository>();
            services.AddScoped<ShopCartManager>();
            services.AddSession(session => session.IdleTimeout = new System.TimeSpan(3,0,0,0));
            services.AddMemoryCache();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Shared/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts(); 
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Nav}/{action=Index}/{id?}");
            });
        }
    }
}
