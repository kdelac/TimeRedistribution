using Apache.NMS;
using Apache.NMS.ActiveMQ.Commands;
using Apache.NMS.ActiveMQ.Threads;
using iText.StyledXmlParser.Css.Resolve.Shorthand.Impl;
using MedAppCore;
using MedAppCore.Client;
using MedAppCore.Models;
using MedAppCore.Services;
using Microsoft.EntityFrameworkCore.Migrations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace MedAppServices
{
    public class OrchestratorService : IOrcestratorService
    {
        private readonly ILogService _logService;
        private readonly IAmqService _amqService;
        private readonly IApiCall _apiCall;
        private readonly string EVENT_NAME_STATUS = "appoitmentStatus";
        private readonly string SEND_EMAIL = "sendMAil";
        private readonly string url = "tcp://localhost:61616";
        private readonly IConnectionFactory connectionFactory;
        private readonly IConnection connection;
        private readonly ISession session;


        public OrchestratorService(
            ILogService logService,
            IAmqService amqService,
            IApiCall apiCall)
        {
            connectionFactory = new NMSConnectionFactory(url);
            connection = connectionFactory.CreateConnection();
            connection.Start();
            session = connection.CreateSession(AcknowledgementMode.AutoAcknowledge);
            _logService = logService;
            _amqService = amqService;
            _apiCall = apiCall;
        }


        public async void LogStrat()
        {
            var transactionSetup = await _logService.GetAll();
            var transactions = transactionSetup.ToList();

            if (transactions.Count != 0)
            {
                transactions.ForEach( _ =>
                {
                    if (_.TransactionStatus == Status.Start && !_.EventRaised)
                    {
                        _amqService.SendEvent(_, EVENT_NAME_STATUS);
                    }

                    if (_.TransactionStatus == Status.BilligSucces && !_.EventRaised)
                    {
                        _amqService.SendEvent(_, EVENT_NAME_STATUS);
                    }
                });
            }           
        }

        public async void Listening()
        {
            IDestination destinationStatus = session.GetQueue(EVENT_NAME_STATUS);
            IMessageConsumer messageConsumerStatus = session.CreateConsumer(destinationStatus);
            var resutl = messageConsumerStatus.Receive();
            IObjectMessage message = resutl as IObjectMessage;
            messageConsumerStatus.Listener += new MessageListener(Message_ListenerStatus);
        }

        protected async void Message_ListenerStatus(IMessage receivedMsg)
        {
            IObjectMessage message = receivedMsg as IObjectMessage;
            var msg = message as TransactionSetup;

            if (msg.TransactionStatus == Status.Start)
            {
                var bill = new Bill();
                var result = _apiCall.Create(bill, Urls.BaseUrlBilling, Urls.UrlToCreateBill);
                if (result.IsCompletedSuccessfully)
                {
                    if (_amqService.SendEvent(msg, SEND_EMAIL))
                    {
                        msg.EventRaised = true;
                    }
                    else
                    {
                        msg.EventRaised = false;
                    }                    

                    var log = await _logService.GetLogByAppoitmentId(msg.AppoitmentId);
                    msg.TransactionStatus = Status.BilligSucces;
                    await _logService.UpdateLog(msg, log);
                }
            }

            if (msg.TransactionStatus == Status.BilligSucces)
            {
                msg.TransactionStatus = Status.SendEmail;
                if (_amqService.SendEvent(msg, SEND_EMAIL))
                {
                    var tr = await _logService.GetLogByAppoitmentId(msg.AppoitmentId);
                    await _logService.UpdateLog(tr, msg);
                }
            }
        }
    }
}
