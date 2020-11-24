using System;
using System.Threading;
using System.Threading.Tasks;
using Apache.NMS;
using Microsoft.Extensions.Hosting;
using MedAppCore;
using Microsoft.Extensions.Logging;

namespace TestProject
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private IConnectionFactory connectionFactory;
        private IConnection connection;
        private ISession session;
        private readonly string SEND_EMAIL = "VirtualTopic.Message";

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
            IDestination destinationStatus = session.GetTopic(SEND_EMAIL);
            IMessageConsumer messageConsumerStatus = session.CreateConsumer(destinationStatus);
            messageConsumerStatus.Listener += new MessageListener(Message_ListenerStatus);
            await Task.Delay(5000, stoppingToken);
        }

        protected void Message_ListenerStatus(IMessage receivedMsg)
        {
            ITextMessage message = receivedMsg as ITextMessage;
            _logger.LogInformation($"Ja se ponavljam servis: {message.Text}");
        }
    }
}
