using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IRescheduleService _scheduleRedistribution;

        public Worker(ILogger<Worker> logger, IRescheduleService scheduleRedistribution)
        {
            _logger = logger;
            _scheduleRedistribution = scheduleRedistribution;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _scheduleRedistribution.Reschedule();
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
