using Autofac;
using Autofac.Extensions.DependencyInjection;
using DDD.BuildingBlocks.Application;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Scheduling.API.Configuration.ExecutionContext;
using Scheduling.API.Configuration.Extensions;
using Scheduling.API.Modules.Scheduling;
using Scheduling.Domain.Clinics;
using Scheduling.Infrastructure;
using Scheduling.Infrastructure.Configuration;
using Scheduling.Infrastructure.Domain.Clinics;

namespace Scheduling.API
{
    public class Startup
    {
        private const string ConnectionString = "ConnectionSring";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSwaggerDocumentation();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IExecutionContextAccessor, ExecutionContextAccessor>();
            //services.AddDbContext<SchedulingContext>(options => options.UseSqlServer("Server=localhost,1433;Database=TestDb;User ID=SA;Password=Passw0rd;", x => x.MigrationsAssembly("Scheduling.Infrastructure")));
            //services.AddScoped<IClinicRepository, ClinicRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var container = app.ApplicationServices.GetAutofacRoot();

            InitializeModules(container);            

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwaggerDocumentation();
        }

        public void ConfigureContainer(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterModule(new SchedulingAutofacModule());
        }

        private void InitializeModules(ILifetimeScope container)
        {
            var httpContextAccessor = container.Resolve<IHttpContextAccessor>();
            var executionContextAccessor = new ExecutionContextAccessor(httpContextAccessor);

            SchedulingStartup.Initialize(
                "Server=localhost,1433;Database=TestDb;User ID=SA;Password=Passw0rd;", 
                executionContextAccessor);

        }

    }
}
