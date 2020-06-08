using MedAppCore;
using MedAppCore.Services;
using MedAppData;
using MedAppServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
                services.AddDbContext<MedAppDbContext>(options => options.UseSqlServer("Server=localhost;Database=TimeScheduel;Trusted_Connection=True;"));
                services.AddScoped<IDoctorService, DoctorService>();
                services.AddScoped<IPatientService, PatientService>();
            });
    }

}

