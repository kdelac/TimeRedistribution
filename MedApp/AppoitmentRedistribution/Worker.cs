using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AppoitmentRedistribution
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IScheduleRedistribution _scheduleRedistribution;

        public Worker(ILogger<Worker> logger, IScheduleRedistribution scheduleRedistribution)
        {
            _logger = logger;
            _scheduleRedistribution = scheduleRedistribution;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _scheduleRedistribution.Redistribut();
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
