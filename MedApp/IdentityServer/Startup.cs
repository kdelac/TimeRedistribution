using MedAppCore.Models;
using MedAppData;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ise = IdentityServer.Configuration;

namespace IdentityServer
{
    public class Startup
    {
        private string server;
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {

            //string constring;

            //server =  Configuration["DBServer"] ?? "localhost";
            //var port = Configuration["DBPort"] ?? "1433";
            //var user = Configuration["DBUser"] ?? "SA";
            //var password = Configuration["DBPassword"] ?? "Passw0rd";
            //var database = Configuration["Database"] ?? "TimeScheduel";

            //constring = Configuration.GetConnectionString("Default");
            //services.AddDbContext<MedAppDbContext>(options => options.UseSqlServer(constring, x => x.MigrationsAssembly("MedAppData")));
            //services.AddIdentity<ApplicationUser, IdentityRole>()
            //        .AddEntityFrameworkStores<MedAppDbContext>();

            //services.ConfigureApplicationCookie(config =>
            //{
            //    config.Cookie.Name = "IdentityServer.Cookie";
            //    config.LoginPath = "/Authentication/Login";
            //    config.LogoutPath = "/Authentication/Logout";
            //});

            //var builder = services.AddIdentityServer()
            //    .AddAspNetIdentity<ApplicationUser>()
            //    .AddConfigurationStore(options =>
            //    {
            //        options.ConfigureDbContext = b => b.UseSqlServer(constring,
            //            sql => sql.MigrationsAssembly("MedAppData"));
            //    })
            //    .AddOperationalStore(options =>
            //    {
            //        options.ConfigureDbContext = b => b.UseSqlServer(constring,
            //            sql => sql.MigrationsAssembly("MedAppData"));
            //    });

            //builder.AddDeveloperSigningCredential();

            //services.ConfigureApplicationCookie(config =>
            //{
            //    config.Cookie.Name = "IdentityServer.Cookie";
            //    config.LoginPath = "/Account/Login";
            //    config.LogoutPath = "/Account/Logout";
            //});

            services.AddIdentityServer()
                .AddTestUsers(ise.GetUsers())
                .AddInMemoryApiResources(ise.GetApiResources())
                .AddInMemoryClients(ise.GetClients())
                .AddInMemoryApiScopes(ise.GetApiScopes())                
                .AddInMemoryIdentityResources(ise.GetIdentityResources())
                
                .AddDeveloperSigningCredential();

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer();

            app.UseStaticFiles();
            app.UseRouting();            

            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
