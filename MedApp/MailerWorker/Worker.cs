using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Apache.NMS;
using Apache.NMS.Util;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MailerWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
            
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {           
            IConnectionFactory connectionFactory = new NMSConnectionFactory("tcp://activemq:61616");
            IConnection connection = connectionFactory.CreateConnection();
            connection.Start();
            ISession session = connection.CreateSession(AcknowledgementMode.AutoAcknowledge);
            IDestination destination = SessionUtil.GetDestination(session, "novePoruke");
            IMessageConsumer messageConsumer = session.CreateConsumer(destination);
            messageConsumer.Listener += new MessageListener(Message_Listener);
            _logger.LogInformation($"Pocetak rada: {DateTime.Now.Hour}:{DateTime.Now.Minute}");
            await Task.Delay(5000, stoppingToken);
        }

        protected void Message_Listener(IMessage receivedMsg)
        {
            ITextMessage message = receivedMsg as ITextMessage;
            _logger.LogInformation($"Poruka je: {message.Text}");
        }
    }
}
