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
        public IServiceScopeFactory _serviceScopeFactory;
        private readonly string url = "tcp://localhost:61616";
        private IConnectionFactory connectionFactory;
        private IConnection connection;
        private ISession session;

        public Worker(ILogger<Worker> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            connectionFactory = new NMSConnectionFactory(url);
            connection = connectionFactory.CreateConnection();
            connection.Start();
            session = connection.CreateSession(AcknowledgementMode.AutoAcknowledge);
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            IDestination destinationStatus = session.GetQueue("status");
            IMessageConsumer messageConsumerStatus = session.CreateConsumer(destinationStatus);
            messageConsumerStatus.Listener += new MessageListener(Message_ListenerStatus);
            await Task.Delay(5000, stoppingToken);
        }

        protected void Message_ListenerStatus(IMessage receivedMsg)
        {
            IObjectMessage message = receivedMsg as IObjectMessage;
            TransactionSetup transactionSetup = (TransactionSetup)message.Body;

            try
            {
                if (transactionSetup.AppoitmentCreated == Status.success)
                {
                    _logger.LogInformation("Posla sam mail ne!");

                    IObjectMessage objectMessage;

                    transactionSetup.MailSent = Status.success;

                    ISession session = connection.CreateSession();
                    ActiveMQQueue queue = new ActiveMQQueue("status");
                    IMessageProducer messageProducer = session.CreateProducer();

                    objectMessage = session.CreateObjectMessage(transactionSetup);

                    messageProducer.Send(queue, objectMessage);
                }
                
            }
            catch (Exception)
            {
                IObjectMessage objectMessage;

                transactionSetup.MailSent = Status.failed;

                ISession session = connection.CreateSession();
                ActiveMQQueue queue = new ActiveMQQueue("status");
                IMessageProducer messageProducer = session.CreateProducer();

                objectMessage = session.CreateObjectMessage(transactionSetup);

                messageProducer.Send(queue, objectMessage);
            }
        }
    }
}
