using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MedAppCore.Services;
using MedAppServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
                await _scheduleRedistribution.Reschedule(5, DateTime.Now, "Waiting");
                await Task.Delay(1000, stoppingToken);
            }
        }
    }

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {

        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IRescheduleService, RescheduleService>();
        }
    }
}
