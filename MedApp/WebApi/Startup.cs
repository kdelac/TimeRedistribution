using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MedAppCore.Client;
using MedAppData;
using MedAppServices.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace WebApi
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

            app.UseCors("enableCORS");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Time");
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


            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "TimeRedistribution", Version = "v1" });
            });

            services.AddCors(options =>
            {
                options.AddPolicy(name: "enableCORS",
                                  builder =>
                                  {
                                      builder.AllowAnyHeader()
                                      .AllowAnyMethod()
                                      .AllowAnyOrigin();
                                  });
            });

            services.AddAutoMapper(typeof(Startup));

            services.AddScoped<IApiCall, ApiCall>();
        }
    }
}
