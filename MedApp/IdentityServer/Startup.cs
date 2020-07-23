using MedAppCore.Models;
using MedAppData;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IdentityServer
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //WorkingWithDatabase(services);
            WorkingWithInMemoryData(services);


            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();

            app.UseIdentityServer();

            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }

        private void WorkingWithDatabase(IServiceCollection services)
        {
            string constring = Configuration.GetConnectionString("Default");

            services.AddDbContext<MedAppDbContext>(options => options.UseSqlServer(constring, x => x.MigrationsAssembly("MedAppData")));
            services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<MedAppDbContext>();

            services.AddIdentityServer()
                .AddAspNetIdentity<ApplicationUser>()
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = b => b.UseSqlServer(constring,
                        sql => sql.MigrationsAssembly("MedAppData"));
                })
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = b => b.UseSqlServer(constring,
                        sql => sql.MigrationsAssembly("MedAppData"));
                })
                .AddDeveloperSigningCredential();
        }

        private void WorkingWithInMemoryData(IServiceCollection services)
        {
            services.AddIdentityServer()
                .AddInMemoryApiScopes(InMemoryConfiguration.ApiScopes)
                .AddInMemoryApiResources(InMemoryConfiguration.ApiResources)
                .AddInMemoryIdentityResources(InMemoryConfiguration.IdentityResources)
                .AddTestUsers(InMemoryConfiguration.Users)
                .AddInMemoryClients(InMemoryConfiguration.Clients)
                .AddDeveloperSigningCredential();
        }
    }
}
