using System;
using System.Threading;
using System.Threading.Tasks;
using MedAppCore;
using MedAppCore.Services;
using MedAppData;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Nest;

namespace AppoitmentRedistribution
{
    public class Worker : BackgroundService, IHostedService
    {
        private readonly ILogger<Worker> _logger;
        public IServiceScopeFactory _serviceScopeFactory;

        public Worker(ILogger<Worker> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var scoped = scope.ServiceProvider.GetRequiredService<IRescheduleService>();
                    scope.ServiceProvider.GetRequiredService<IAppointmentService>();
                    scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                    scope.ServiceProvider.GetRequiredService<IDoctorService>();
                    scope.ServiceProvider.GetRequiredService<IPatientService>();
                    scope.ServiceProvider.GetRequiredService<ElasticClient>();
                    scope.ServiceProvider.GetRequiredService<MedAppDbContext>();
                    await scoped.Reschedule(5, DateTime.Now, "Waiting");
                    _logger.LogInformation($"Prolaz u: {DateTime.Now.Hour}:{DateTime.Now.Minute}");
                    await Task.Delay(1000, stoppingToken);
                }
            }
        }
    }
}

