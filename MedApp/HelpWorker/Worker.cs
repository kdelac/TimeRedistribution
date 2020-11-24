using Apache.NMS;
using MedAppCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HelpWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private IConnectionFactory connectionFactory;
        private IConnection connection;
        private ISession session;
        private readonly string SEND_EMAIL = "Consumer.B.VirtualTopic.Message";

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
            ITextMessage message = receivedMsg as ITextMessage;
            _logger.LogInformation($"Drugi servis: {message.Text}");
        }
    }
}
