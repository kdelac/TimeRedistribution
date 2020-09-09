
using MedAppCore;
using MedAppCore.Services;
using MedAppData;
using MedAppServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SignalR.Handlers;
using SignalR.Interfaces;

namespace SignalR
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            Services(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<NoPHub>("/nophub");
            });
        }

        private void Services(IServiceCollection services)
        {
            //services.AddHostedService<UpdatingDatabaseService>();
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => builder
                .WithOrigins("http://localhost:4200")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
            });

            services.AddSignalR();
            services.AddSingleton<ILoginEventHandler, ReciveLoginEventHandler>();
            services.AddSingleton<ILogoutEventHandler, ReciveLogoutMessageHandler>();
            

            var constring = Configuration.GetConnectionString("Default");
            services.AddDbContext<MedAppDbContext>(options => options.UseSqlServer(constring, x => x.MigrationsAssembly("MedAppData")));


            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IApplicationService, ApplicationService>();
            services.AddTransient<IOrdinationService, OrdinationService>();
            
        }
    }
}
