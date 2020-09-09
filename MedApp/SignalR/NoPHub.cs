using Microsoft.AspNetCore.SignalR;
using SignalR.Interfaces;
using SignalR.Models;
using System.Threading.Tasks;

namespace SignalR
{
    public class NoPHub : Hub
    {
        private readonly ILogoutEventHandler _logoutEventHandler;
        private readonly ILoginEventHandler _loginEventHandler;

        public NoPHub(ILogoutEventHandler logoutEventHandler, ILoginEventHandler loginEventHandler)
        {
            _logoutEventHandler = logoutEventHandler;
            _loginEventHandler = loginEventHandler;
        }

        public Task SendMessageLogin(string patientId, string ordinationId)
        {
            _loginEventHandler.Publish(new RecivedLoginMessageEvent
            {
                PatientId = patientId,
                OrdinationId = ordinationId
            });

            return Task.CompletedTask;
        }

        public Task SendMessageLogout(string patientId, string ordinationId)
        {
            _logoutEventHandler.Publish(new ReciveLogoutMessageEvent
            {
                PatientId = patientId,
                OrdinationId = ordinationId
            });

            return Task.CompletedTask;
        }

        public async Task SendMessages(int numberW, int numberO)
        {
            await Clients.All.SendAsync("ReceiveMessages", numberW, numberO);
        }
    }
}
