using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AppoitmentRedistribution.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AppoitmentRedistribution
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private ScheduleRedistribution scheduleRedistribution;
        private TimeScheduelContext _contex;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {

            return base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _contex = new TimeScheduelContext();
                scheduleRedistribution = new ScheduleRedistribution(_contex, _logger);
                scheduleRedistribution.Redistribut();

                await Task.Delay(60*1000, stoppingToken);
            }
        }
    }
}
