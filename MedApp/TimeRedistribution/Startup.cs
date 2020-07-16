using MedAppCore;
using MedAppCore.Services;
using MedAppData;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MedAppServices;
using AutoMapper;
using Nest;
using System;
using Microsoft.AspNetCore.Http;
using MedAppCore.Services.ElasticSearch;
using MedAppServices.ElasticSearch;
using MedAppCore.Models;
using Microsoft.AspNetCore.Identity;
using MedAppCore.Repositories;
using MedAppData.Repositories;

namespace TimeRedistribution
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
            services.AddControllers();            

            services.AddControllersWithViews()
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

            app.UseIdentityServer();
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
                services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<MedAppDbContext>();
            }
            else
            {
                services.AddDbContext<MedAppDbContext>(options => options.UseSqlServer($"Server={server},{port};Database={database};User ID ={user};Password={password}", x => x.MigrationsAssembly("MedAppData")));
            }



            #region elastic

            string connectionString;

            if (server == "localhost")
            {
                connectionString = Configuration.GetConnectionString("elasticsearch");
            }
            else
            {
                connectionString = Configuration.GetConnectionString("elasticsearchDock");
            }

                var settings = new ConnectionSettings(new Uri(connectionString));

            services.AddSingleton(settings);

            services.AddScoped(s =>
            {
                var connectionSettings = s.GetRequiredService<ConnectionSettings>();
                var client = new ElasticClient(connectionSettings);

                return client;
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            #endregion
            services.AddIdentityServer();

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

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IDoctorService, DoctorService>();
            services.AddTransient<IPatientService, PatientService>();
            services.AddTransient<IAppointmentService, AppointmentService>();
            services.AddTransient<IRescheduleService, RescheduleService>();
            services.AddTransient<IDoctorPatientService, DoctorPatientService>();

            services.AddTransient<ISignService, SignService>();

            services.AddTransient<IUserSearchService, UserSearchService>();
            services.AddTransient<IDateSearchService, DateSearchService>();
            services.AddTransient<IUriService, UriService>();

            services.AddTransient<IUserRepository<ApplicationUser>, UserRepository<ApplicationUser>>();

            /// <summary>
            /// Pomocno, samo u svrhu pregleda podataka
            /// </summary>
            /// <returns></returns>
            services.AddTransient<IDodavanjeTermina, DodavanjeTermina>();
        }
    }
}
