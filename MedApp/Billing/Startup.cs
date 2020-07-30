using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedAppCore;
using MedAppData;
using MedAppServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Billing
{
    public class Startup
    {
        private string server;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );

            services.AddControllers();

            Services(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void Services(IServiceCollection services)
        {
            server = Configuration["DBServer"] ?? "localhost";
            var port = Configuration["DBPort"] ?? "1433";
            var user = Configuration["DBUser"] ?? "SA";
            var password = Configuration["DBPassword"] ?? "Passw0rd";
            var database = Configuration["Database"] ?? "TimeScheduel";

            if (server == "localhost")
            {
                var constring = Configuration.GetConnectionString("Default");
                services.AddDbContext<MedAppDbContext>(options => options.UseSqlServer(constring, x => x.MigrationsAssembly("MedAppData")));
            }
            else
            {
                services.AddDbContext<MedAppDbContext>(options => options.UseSqlServer($"Server={server},{port};Database={database};User ID ={user};Password={password}", x => x.MigrationsAssembly("MedAppData")));
            }

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBillingService, BillingService>();
        }
    }
}
