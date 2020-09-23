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
        private int brojac = 0;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            IConnectionFactory connectionFactory = new NMSConnectionFactory(Urls.ActiveMQ);
            IConnection connection = connectionFactory.CreateConnection();
            connection.Start();
            ISession session = connection.CreateSession(AcknowledgementMode.AutoAcknowledge);
            IDestination destination = session.GetQueue("porukeQueueTestni");
            IMessageConsumer messageConsumer = session.CreateConsumer(destination);
            messageConsumer.Listener += new MessageListener(Message_Listener);
            brojac++;
            _logger.LogInformation($"Pocetak rada: {DateTime.Now.Hour}:{DateTime.Now.Minute}");
            await Task.Delay(5000, stoppingToken);
        }

        protected void Message_Listener(IMessage receivedMsg)
        {
            ITextMessage message = receivedMsg as ITextMessage;
            _logger.LogInformation($"Poruka je: {message.Text}     broj poruka: {brojac}");
        }
    }
}
