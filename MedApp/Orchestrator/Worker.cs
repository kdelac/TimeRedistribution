using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Apache.NMS;
using Apache.NMS.ActiveMQ.Commands;
using MedAppCore;
using MedAppCore.Client;
using MedAppCore.Models;
using MedAppCore.Services;
using MedAppData;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Nest;

namespace Orchestrator
{
    public class Worker : BackgroundService
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
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var scoped = scope.ServiceProvider.GetRequiredService<IOrcestratorService>();
                scope.ServiceProvider.GetRequiredService<IAmqService>();
                scope.ServiceProvider.GetRequiredService<ILogService>();
                scope.ServiceProvider.GetRequiredService<IApiCall>();
                scope.ServiceProvider.GetRequiredService<MedAppDbContext>();
                scope.ServiceProvider.GetRequiredService<ElasticClient>();
                scoped.Listening();
                _logger.LogInformation($"Prolaz u: {DateTime.Now.Hour}:{DateTime.Now.Minute}");
                while (!stoppingToken.IsCancellationRequested)
                {
                    await Task.Delay(1000, stoppingToken);
                    _logger.LogInformation($"Prolaz u: {DateTime.Now.Hour}:{DateTime.Now.Minute}");
                    await Task.Delay(10000, stoppingToken);
                }                           
            }           
        }

    }
}
