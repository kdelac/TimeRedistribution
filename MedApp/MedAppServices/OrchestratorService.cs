using Apache.NMS;
using Apache.NMS.ActiveMQ.Commands;
using Apache.NMS.ActiveMQ.Threads;
using MedAppCore;
using MedAppCore.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace MedAppServices
{
    public class OrchestratorService : IOrcestratorService
    {
        static HttpClient client = new HttpClient();
        private readonly string url = "tcp://localhost:61616";
        private IConnectionFactory connectionFactory;
        private IConnection connection;
        private ISession session;

        public OrchestratorService()
        {
            client.BaseAddress = new Uri("https://localhost:44308/");
            connectionFactory = new NMSConnectionFactory(url);
            connection = connectionFactory.CreateConnection();
            connection.Start();
            session = connection.CreateSession(AcknowledgementMode.AutoAcknowledge);
        }


        public async void CreateAppoitment()
        {
            IDestination destination = session.GetQueue("createAppoitment");
            IMessageConsumer messageConsumer = session.CreateConsumer(destination);
            messageConsumer.Listener += new MessageListener(Message_ListenerCreateAppointment);           
        }


        protected async void Message_ListenerCreateAppointment(IMessage receivedMsg)
        {
            IObjectMessage message = receivedMsg as IObjectMessage;
            AppoitmentAdd appoitmentAdd = (AppoitmentAdd)message.Body;

            Appointment appointment = new Appointment();
            appointment.DoctorId = appoitmentAdd.DoctorId;
            appointment.PatientId = appoitmentAdd.PatientId;
            appointment.DateTime = appoitmentAdd.DateTime;
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "api/appointment", appointment);

            if (response.IsSuccessStatusCode)
            {
                IObjectMessage objectMessage;

                var resultString = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Appointment>(resultString);

                TransactionSetup transactionSetup = new TransactionSetup();

                transactionSetup.Id = result.Id;
                transactionSetup.AppoitmentCreated = Status.success;

                ISession session = connection.CreateSession();
                ActiveMQQueue queue = new ActiveMQQueue("status");
                IMessageProducer messageProducer = session.CreateProducer();

                objectMessage = session.CreateObjectMessage(transactionSetup);

                messageProducer.Send(queue, objectMessage);
            }
            else
            {
                IObjectMessage objectMessage;

                TransactionSetup transactionSetup = new TransactionSetup();

                transactionSetup.AppoitmentCreated = Status.failed;

                ISession session = connection.CreateSession();
                ActiveMQQueue queue = new ActiveMQQueue("status");
                IMessageProducer messageProducer = session.CreateProducer();

                objectMessage = session.CreateObjectMessage(transactionSetup);

                messageProducer.Send(queue, objectMessage);
            }
        }


        public void CheckStatus()
        {
            IDestination destination = session.GetQueue("status");
            IMessageConsumer messageConsumer = session.CreateConsumer(destination);
            messageConsumer.Listener += new MessageListener(Message_ListenerStatus);
            
        }

        protected void Message_ListenerStatus(IMessage receivedMsg)
        {
            IObjectMessage message = receivedMsg as IObjectMessage;
            TransactionSetup transactionSetup = (TransactionSetup)message.Body;

            if (Status.failed == transactionSetup.AppoitmentCreated)
            {
                //Failed to create appointment
            }            

            if (Status.failed == transactionSetup.BillCreated)
            {
                //Failed to create bill
                //Delete created appoitment
                //Poziv apija za brisanje apoitmenta...
                //_appointmentService.Delete(transactionSetup.Id);
            }

            if (Status.failed == transactionSetup.MailSent)
            {
                //Failed to send mail
                //Delete created appoitment
                //Delete created bill
            }
        }
    }
}
