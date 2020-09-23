using Apache.NMS;
using Grpc.Net.Client;
using GrpcAppointment;
using MedAppCore;
using MedAppCore.Client;
using MedAppCore.Models;
using MedAppCore.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MedAppServices
{
    public class OrchestratorService : IOrcestratorService
    {
        private readonly ILogService _logService;
        private readonly IAmqService _amqService;
        private readonly IApiCall _apiCall;
        private readonly string EVENT_NAME_STATUS = "appoitmentStatus";
        private readonly string SEND_EMAIL = "sendMAil";
        private readonly string COMPLETION = "completion";
        private readonly IConnectionFactory connectionFactory;
        private readonly IConnection connection;
        private readonly ISession session;
        private bool flag = false;


        public OrchestratorService(
            ILogService logService,
            IAmqService amqService,
            IApiCall apiCall)
        {
            connectionFactory = new NMSConnectionFactory(Urls.ActiveMQ);
            connection = connectionFactory.CreateConnection();
            connection.Start();
            session = connection.CreateSession(AcknowledgementMode.AutoAcknowledge);
            _logService = logService;
            _amqService = amqService;
            _apiCall = apiCall;
        }


        public async void LogStrat()
        {
            if (flag == false)
            {
                var transactionSetup = await _logService.GetAll();
                var transactions = transactionSetup.ToList();

                if (transactions.Count != 0)
                {
                    transactions.ForEach(_ =>
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
        }

        public void Listening()
        {
            IDestination destinationStatus = session.GetQueue(EVENT_NAME_STATUS);
            IMessageConsumer messageConsumerStatus = session.CreateConsumer(destinationStatus);
            messageConsumerStatus.Listener += new MessageListener(Message_ListenerStatus);
        }

        protected async void Message_ListenerStatus(IMessage receivedMsg)
        {
            IObjectMessage message = receivedMsg as IObjectMessage;
            var msg = message.Body as TransactionSetup;

            flag = true;

            if (msg.TransactionStatus == Status.Start)
            {
                var bill = new Bill();

                //Http client
                //var result = await _apiCall.Create(bill, Urls.BaseUrlBilling, Urls.UrlToCreateBill);
                //if (result.IsSuccessStatusCode)
                //{
                //    msg.TransactionStatus = Status.BilligSucces;

                //    await SendEvent(msg);
                //}
                //else
                //{
                //    msg.TransactionStatus = Status.Failed;

                //    await SendEvent(msg);

                //    await _apiCall.Delete(Urls.BaseUrlCreateAppointment, Urls.UrlToBaseAppointment, msg.AppoitmentId.ToString());
                //    flag = false;
                //}


                //gRPC client
                if (await CreateBill())
                {
                    msg.TransactionStatus = Status.BilligSucces;

                    await SendEvent(msg);
                }
                else
                {
                    msg.TransactionStatus = Status.Failed;

                    await SendEvent(msg);

                    await _apiCall.Delete(Urls.BaseUrlCreateAppointment, Urls.UrlToBaseAppointment, msg.AppoitmentId.ToString());
                    flag = false;
                }
            }

            if (msg.TransactionStatus == Status.BilligSucces)
            {
                msg.TransactionStatus = Status.SendEmail;
                if (_amqService.SendEvent(msg, SEND_EMAIL))
                {
                    msg.TransactionStatus = Status.Succes;
                    await UpdateLog(msg);
                }
            }
        }

        private async Task<TransactionSetup> SendEvent(TransactionSetup msg)
        {
            if (msg.TransactionStatus == Status.Failed || msg.TransactionStatus == Status.Succes)
            {
                _amqService.SendEvent(msg, COMPLETION);
               
            }
            else
            {
                if (_amqService.SendEvent(msg, SEND_EMAIL))
                {
                    msg.EventRaised = true;
                }
                else
                {
                    msg.EventRaised = false;
                }
                
            }
            return await UpdateLog(msg);
        }

        private async Task<TransactionSetup> UpdateLog(TransactionSetup msg)
        {
            var id = new Guid(msg.AppoitmentId.ToString());
            var log = await _logService.GetLogByAppoitmentId(id);
            await _logService.UpdateLog(log, msg);
            flag = false;
            return log;
        }


        private async Task<bool> CreateBill()
        {
            using var channel = GrpcChannel.ForAddress(Urls.gRPCAppoitment);
            var client = new Greeter.GreeterClient(channel);
            var replay = await client.SayHelloAsync(new HelloRequest());

            return replay.Message;
        }
    }
}
