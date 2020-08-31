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
using System.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using MedAppCore.Client;
using MedAppServices.Client;
using Polly;
using System.Net.Http;
using Polly.Extensions.Http;

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
            services.AddControllers()
                .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                ); 

            services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                options.RequireHttpsMetadata = false;

                options.Authority = "https://localhost:44399/";

                options.Audience = "api1";
            });

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

            app.UseAuthentication();
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
                Debug.WriteLine(constring);
                services.AddDbContext<MedAppDbContext>(options => options.UseSqlServer(constring, x => x.MigrationsAssembly("MedAppData")));
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
            services.AddScoped<ILogService, LogService>();

            services.AddTransient<ISignService, SignService>();

            services.AddTransient<IUserSearchService, UserSearchService>();
            services.AddTransient<IDateSearchService, DateSearchService>();
            services.AddTransient<IUriService, UriService>();

            services.AddScoped<IAmqService, AmqService>();

            //services.AddScoped<IOrcestratorService, OrchestratorService>();
            //services.AddHttpClient<IApiCall, ApiCall>()
            //        .SetHandlerLifetime(TimeSpan.FromMinutes(5))
            //        .AddPolicyHandler(GetRetryPolicy());

            services.AddScoped<IApiCall, ApiCall>();

            /// <summary>
            /// Pomocno, samo u svrhu pregleda podataka
            /// </summary>
            /// <returns></returns>
            services.AddTransient<IDodavanjeTermina, DodavanjeTermina>();
        }

        static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2,
                                                                            retryAttempt)));
        }
    }
}
