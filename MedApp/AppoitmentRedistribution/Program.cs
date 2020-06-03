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
                services.AddSingleton<IPatientService, PatientService>();
                services.AddSingleton<IUnitOfWork, UnitOfWork>();
                services.AddDbContext<MedAppDbContext>(options => options.UseSqlServer("Server=localhost;Database=TimeScheduel;Trusted_Connection=True;"));

                services.AddTransient<IDoctorService, DoctorService>();
                services.AddTransient<IPatientService, PatientService>();
                services.AddTransient<IAppointmentService, AppointmentService>();
                services.AddTransient<IRescheduleService, RescheduleService>();
                services.AddHostedService<Worker>();
            });
    }

}

