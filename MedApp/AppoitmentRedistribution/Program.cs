using MedAppCore;
using MedAppCore.Services;
using MedAppData;
using MedAppServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nest;
using System;

namespace AppoitmentRedistribution
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<Worker>();
                services.AddScoped<IRescheduleService, RescheduleService>();
                services.AddScoped<IAppointmentService, AppointmentService>();
                services.AddScoped<IUnitOfWork, UnitOfWork>();
                //services.AddDbContext<MedAppDbContext>(options => options.UseSqlServer("Server=localhost;Database=TimeScheduel;Trusted_Connection=True;"));
                services.AddDbContext<MedAppDbContext>(options => options.UseSqlServer($"Server=db,1433;Database=TimeScheduel;User ID=SA;Password=Passw0rd"));
                services.AddScoped<IDoctorService, DoctorService>();
                services.AddScoped<IPatientService, PatientService>();

                var settings = new ConnectionSettings(new Uri("http://elasticsearch:9200"));

                services.AddSingleton(settings);

                services.AddScoped(s =>
                {
                    var connectionSettings = s.GetRequiredService<ConnectionSettings>();
                    var client = new ElasticClient(connectionSettings);

                    return client;
                });
            });
    }

}

