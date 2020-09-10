using MedAppCore;
using MedAppCore.Models;
using MedAppCore.Services;
using MedAppData;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SignalR.Interfaces;
using SignalR.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SignalR
{
    public class UpdatingDatabaseService : BackgroundService
    {
        private IHubContext<NoPHub> _signalrHub;
        private readonly ILoginEventHandler _loginevent;
        private readonly ILogoutEventHandler _logoutevent;

        private IServiceProvider Services { get; }


        public UpdatingDatabaseService(
            IServiceProvider services,
            IHubContext<NoPHub> signalrHub,
            ILoginEventHandler loginevent,
            ILogoutEventHandler logoutevent)
        {
            _signalrHub = signalrHub;
            _loginevent = loginevent;
            _logoutevent = logoutevent;
            Services = services;

        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {



                _loginevent.Subscribe(subscriberName: typeof(RecivedLoginMessageEvent).Name,
                                    action: async (e) =>
                                    {
                                        if (e is RecivedLoginMessageEvent)
                                        {
                                            await AddApplication(e);
                                        }
                                    });

            _logoutevent.Subscribe(subscriberName: typeof(ReciveLogoutMessageEvent).Name,
                                    action: async (e) =>
                                    {
                                        if (e is ReciveLogoutMessageEvent)
                                        {
                                            await RemoveApplication(e);
                                        }
                                    });

            return Task.CompletedTask;
        }

        private async Task AddApplication(RecivedLoginMessageEvent e)
        {
            using (var scope = Services.CreateScope())
            {
                var _ordinationService =
                    scope.ServiceProvider
                        .GetRequiredService<IOrdinationService>();

                var _applicationService = scope.ServiceProvider
                        .GetRequiredService<IApplicationService>();


                var ordination = await _ordinationService.GetOrdinationById(Guid.Parse(e.OrdinationId.ToString()));
                Application application = new Application();
                application.Id = Guid.NewGuid();
                application.Ordination = ordination;
                application.OrdinationId = ordination.Id;
                application.PatientId = Guid.Parse(e.PatientId.ToString());
                application.TimeOfApplication = DateTime.Now;
                application.Position = Status2.Out;

                var inside = await _applicationService.NumberInside(ordination.Id);


                if (ordination.MaxIn > inside)
                {
                    application.Position = Status2.In;
                    inside++;
                }

                await _applicationService.CreateApplication(application);

                var outside = await _applicationService.NumberOutside(ordination.Id);

                await _signalrHub.Clients.All.SendAsync("ava", inside, outside);
            }
        }

        private async Task RemoveApplication(ReciveLogoutMessageEvent e)
        {
            await System.Console.Out.WriteLineAsync($"Logout: {e.PatientId} and ordination: {e.OrdinationId}");

            await _signalrHub.Clients.All.SendAsync("ava", e.PatientId, e.OrdinationId);
        }
    }
}
