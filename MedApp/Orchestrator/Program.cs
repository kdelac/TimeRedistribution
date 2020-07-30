using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedAppCore;
using MedAppCore.Client;
using MedAppCore.Services;
using MedAppData;
using MedAppServices;
using MedAppServices.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.SqlServer.Management.Common;
using Nest;

namespace Orchestrator
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
                    services.AddScoped<IOrcestratorService, OrchestratorService>();
                    services.AddScoped<IAmqService, AmqService>();
                    services.AddScoped<IUnitOfWork, UnitOfWork>();
                    services.AddScoped<ILogService, LogService>();
                    services.AddScoped<IApiCall, ApiCall>();
                    services.AddDbContext<MedAppDbContext>(options => options.UseSqlServer("Server=localhost;Database=TimeScheduel;Trusted_Connection=True;"));
                    services.AddHostedService<Worker>();

                    var settings = new Nest.ConnectionSettings(new Uri("http://elasticsearch:9200"));

                    services.AddSingleton(settings);

                    services.AddScoped(s =>
                    {
                        var connectionSettings = s.GetRequiredService<Nest.ConnectionSettings>();
                        var client = new ElasticClient(connectionSettings);

                        return client;
                    });
                });
    }
}
