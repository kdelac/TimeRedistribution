using System;
using System.Threading;
using System.Threading.Tasks;
using MedAppCore.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AppoitmentRedistribution
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IRescheduleService _unit;

        public Worker(ILogger<Worker> logger, IRescheduleService unit)
        {
            _logger = logger;
            _unit = unit;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await _unit.Reschedule(10, DateTime.Now, "Waiting");
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
