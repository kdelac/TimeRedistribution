using MedAppCore;
using MedAppCore.Models;
using MedAppCore.Services;
using MedAppData;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SignalR.Interfaces;
using SignalR.Models;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace SignalR
{
    public class UpdatingDatabaseService : BackgroundService
    {
        private IHubContext<NoPHub> _signalrHub;
        private readonly ILoginEventHandler _loginevent;
        private readonly ILogoutEventHandler _logoutevent;
        string connectionString = "";

        private IServiceProvider Services { get; }


        public UpdatingDatabaseService(
            IServiceProvider services,
            IHubContext<NoPHub> signalrHub,
            ILoginEventHandler loginevent,
            ILogoutEventHandler logoutevent,
            IConfiguration configuration)
        {
            _signalrHub = signalrHub;
            _loginevent = loginevent;
            _logoutevent = logoutevent;
            Services = services;
            connectionString = configuration.GetConnectionString("Default");
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            DbListener();
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
            using var scope = Services.CreateScope();

            var _ordinationService =
               scope.ServiceProvider
                   .GetRequiredService<IOrdinationService>();

            var _applicationService = scope.ServiceProvider
                    .GetRequiredService<IApplicationService>();

            var ordination = await _ordinationService
                .GetOrdinationById(Guid.Parse(e.OrdinationId.ToString()));

            var outside = await _applicationService
                .NumberOutside(ordination.Id);

            if (ordination.MaxOut > outside)
            {
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
                else
                {
                    outside++;
                }

                await _applicationService.CreateApplication(application);

                await _signalrHub.Clients.All
                    .SendAsync("NumberOfPeople", inside, outside);
            }
            else
            {
                await _signalrHub.Clients.All
                    .SendAsync("MaxNumberReached", e.PatientId.ToString(), e.OrdinationId.ToString());
            }
        }

        private async Task RemoveApplication(ReciveLogoutMessageEvent e)
        {
            using var scope = Services.CreateScope();

            var _ordinationService =
                scope.ServiceProvider
                    .GetRequiredService<IOrdinationService>();

            var _applicationService = scope.ServiceProvider
                    .GetRequiredService<IApplicationService>();

            var application = await _applicationService
                .GetApplicationByPatientId(Guid.Parse(e.PatientId.ToString()));

            var ordination = await _ordinationService
                .GetOrdinationById(Guid.Parse(e.OrdinationId.ToString()));

            if (application != null)
            {
                await _applicationService
                    .DeleteApplication(application);
                await _signalrHub.Clients.All
                    .SendAsync("RemovedFromWaitingList", e.PatientId.ToString(), e.OrdinationId.ToString());
            }

            var inside = await _applicationService
                .NumberInside(Guid.Parse(e.OrdinationId));

            var outside = await _applicationService
                .NumberOutside(ordination.Id);

            if (ordination.MaxIn > inside && outside > 0)
            {
                var toBeUpdated = await _applicationService
                    .GetAppoitmentFirstDate();
                await _applicationService
                    .UpdateApplication(toBeUpdated, Status2.In);
                inside++;
            }



            await _signalrHub.Clients.All
                    .SendAsync("NumberOfPeople", inside, outside);
        }

        public void DbListener()
        {
            SqlConnection conn = new SqlConnection(connectionString);

            conn.Open();

            SqlDependency.Start(connectionString);

            string commandText = "SELECT OrdinationId, NumberIn, NumberOut  FROM [dbo].[Waitings]";

            SqlCommand cmd = new SqlCommand(commandText, conn);

            SqlDependency dependency = new SqlDependency(cmd);

            dependency.OnChange += new OnChangeEventHandler(dbChangeNotification);

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine($"OrdinationId: {reader["OrdinationId"]}, NumberIn: {reader["NumberIn"]}, NumberOut: {reader["NumberOut"]}");
            }
        }

        private void dbChangeNotification(object sender, SqlNotificationEventArgs e)
        {
            //Implementacija što se događa kad se promjene podaci u bazi
            Console.WriteLine($"OnDependencyChange Event fired. SqlNotificationEventArgs: Info={e.Info}, Source={e.Source}, Type={e.Type}");
            DbListener();
        }
    }
}
