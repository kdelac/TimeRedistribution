using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Apache.NMS;
using Apache.NMS.ActiveMQ.Commands;
using Apache.NMS.Util;
using MedAppCore;
using MedAppCore.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MailerWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private IConnectionFactory connectionFactory;
        private IConnection connection;
        private ISession session;
        private readonly string SEND_EMAIL = "sendMAil";

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
            connectionFactory = new NMSConnectionFactory(Urls.ActiveMQ);
            connection = connectionFactory.CreateConnection();
            connection.Start();
            session = connection.CreateSession(AcknowledgementMode.AutoAcknowledge);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            IDestination destinationStatus = session.GetQueue(SEND_EMAIL);
            IMessageConsumer messageConsumerStatus = session.CreateConsumer(destinationStatus);
            messageConsumerStatus.Listener += new MessageListener(Message_ListenerStatus);
            await Task.Delay(5000, stoppingToken);
        }

        protected void Message_ListenerStatus(IMessage receivedMsg)
        {
            IObjectMessage message = receivedMsg as IObjectMessage;
            _logger.LogInformation(message.ToString());
        }
    }
}
