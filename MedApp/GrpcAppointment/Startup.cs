﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedAppCore;
using MedAppCore.Services;
using MedAppData;
using MedAppServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GrpcAppointment
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGrpc();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IAppointmentService, AppointmentService>();
            services.AddTransient<IBillingService, BillingService>();
            services.AddDbContext<MedAppDbContext>(options => options.UseSqlServer("Server=localhost,1433;Database=TimeScheduel;User ID=SA;Password=Passw0rd;", x => x.MigrationsAssembly("MedAppData")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<GreeterService>();

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                });
            });
        }
    }
}
