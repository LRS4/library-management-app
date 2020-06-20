using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ILS.Library.DataAccess.SecurityDb.Entities;
using ILS.Library.Web.Services;
using ILS.Library.Web.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ILS.Library.Web
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
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddMvc();
            services.AddSingleton(Configuration);

            services.AddScoped<ILibraryAssetService, LibraryAssetService>();
            services.AddScoped<ICheckoutService, CheckoutService>();
            services.AddScoped<IPatronService, PatronService>();
            services.AddScoped<IBranchService, BranchService>();
            services.AddScoped<ICommsService, CommsService>();

            services.AddDbContext<ILSContext>(options =>
            {
                options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ILS");
            });

            services.AddIdentity<IdentityUser, IdentityRole>()
                    .AddEntityFrameworkStores<ILSContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsEnvironment("test"))
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseStatusCodePagesWithRedirects("/Home/Error/{0}");
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
