using Apache.NMS;
using Apache.NMS.ActiveMQ.Commands;
using iText.Forms.Xfdf;
using MedAppCore;
using MedAppCore.Models;
using MedAppCore.Services;
using Microsoft.Extensions.Logging;
using Nest;
using System;
using System.Threading.Tasks;

namespace MedAppServices
{
    public class AmqService : IAmqService
    {
        private IConnectionFactory connectionFactory;
        private IConnection connection;
        private ISession session;

        public AmqService()
        {
            connectionFactory = new NMSConnectionFactory(Urls.ActiveMQLoc);
            connection = connectionFactory.CreateConnection();
            connection.Start();
            session = connection.CreateSession(AcknowledgementMode.AutoAcknowledge);
        }

        public IObjectMessage ReciveEvent(string queueName)
        {
            IDestination destinationStatus = session.GetQueue(queueName);
            IMessageConsumer messageConsumerStatus = session.CreateConsumer(destinationStatus);
            var resutl =  messageConsumerStatus.Receive();
            IObjectMessage message = resutl as IObjectMessage;

            return message;
            //messageConsumerStatus.Listener += new MessageListener(Message_ListenerStatus<TEntity>);
        }

        public bool SendEvent<TEntity>(TEntity model, string queueName)
        {
            bool succes = false;
            try
            {
                IObjectMessage objectMessage;


                ISession session = connection.CreateSession();
                ActiveMQQueue queue = new ActiveMQQueue(queueName);
                IMessageProducer messageProducer = session.CreateProducer();

                objectMessage = session.CreateObjectMessage(model);

                messageProducer.Send(queue, objectMessage);
                succes = true;
            }
            catch (Exception)
            {
                throw;
            }

            return succes;            
        }

        public bool SendEventTopic(string message, string queueName)
        {
            bool succes = false;
            try
            {
                ITextMessage textMessage;


                ISession session = connection.CreateSession();
                ActiveMQTopic topic = new ActiveMQTopic(queueName);
                IMessageProducer messageProducer = session.CreateProducer();

                textMessage = session.CreateTextMessage(message);

                messageProducer.Send(topic, textMessage);
                succes = true;
            }
            catch (Exception)
            {
                throw;
            }

            return succes;
        }

        protected void Message_ListenerStatus<TEntity>(IMessage receivedMsg)
        {
            IObjectMessage message = receivedMsg as IObjectMessage;
        }
    }
}
