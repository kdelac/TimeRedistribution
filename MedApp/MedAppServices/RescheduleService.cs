using MedAppCore;
using System;
using System.Collections.Generic;
using System.Text;
using MedAppCore.Models;
using MedAppCore.Services;
using System.Threading.Tasks;
using System.Linq;
using MimeKit;
using System.Runtime.CompilerServices;
using Apache.NMS;
using Apache.NMS.Util;
using Apache.NMS.ActiveMQ.Commands;

namespace MedAppServices
{
    public class RescheduleService : IRescheduleService
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IDoctorService _doctorService;

        public RescheduleService(IAppointmentService appointmentService, IDoctorService doctorService)
        {
            _appointmentService = appointmentService;
            _doctorService = doctorService;
        }

        public async Task Reschedule(int deleyMin, DateTime date, string status)
        {
            List<Doctor> doctors = await _doctorService.GetAllWithAppointmentExistAsync();
            TimeSpan deley = new TimeSpan(0, deleyMin, 0);

            if (doctors.Count > 0)
            {
                await Doctors(doctors, deley, status, date);                
            }
        }

        public async Task Doctors(List<Doctor> doctors, TimeSpan deley, string status, DateTime date)
        {
            if (doctors.Count > 0)
            {
                var doctor = doctors.FirstOrDefault();
                var appointments = doctor.Appointments.OrderBy(a => a.DateTime).Where(b => b.Status == status && b.DateTime.Date == date.Date).ToList();

                if (appointments.Count() > 0 && Check(appointments.FirstOrDefault(), deley))
                {
                    await ChangeAppoitments(appointments, deley);
                }
                doctors.Remove(doctor);
                await Doctors(doctors, deley, status, date);
            }
        }

        public async Task ChangeAppoitments(List<Appointment> appointments, TimeSpan deley)
        {
            await UpdateAppoitment(appointments[0], deley);
            for (int i = 1; i < appointments.Count(); i++)
            {
                if (appointments[i].DateTime-appointments[i-1].DateTime <= deley)
                {
                    await UpdateAppoitment(appointments[i], deley);
                }
                else
                {
                    break;
                }
            }
        }

        public async Task UpdateAppoitment(Appointment appointment, TimeSpan appoitmentExtend)
        {
            Appointment appointmentToBeUpdated = new Appointment();
            appointmentToBeUpdated.DoctorId = appointment.DoctorId;
            appointmentToBeUpdated.PatientId = appointment.PatientId;
            appointmentToBeUpdated.DateTime = appointment.DateTime.Date + appointment.DateTime.TimeOfDay + appoitmentExtend;
            await _appointmentService.UpdateAppointment(appointmentToBeUpdated.DateTime, appointmentToBeUpdated.DoctorId, appointmentToBeUpdated.PatientId);
            Send(appointment.Patient.Email);
        } 

        public bool CheckGap(Appointment appoitment, Appointment appoitmentNext, TimeSpan deley)
        {
            if (appoitmentNext.DateTime.TimeOfDay - appoitment.DateTime.TimeOfDay > deley)
            {
                return true;
            }
            return false;
        }

        public bool Check(Appointment appointment, TimeSpan deley)
        {
            TimeSpan vr = DateTime.Now.TimeOfDay - deley;
            if (vr > appointment.DateTime.TimeOfDay)
            {
                return true;
            }
            return false;
        }

        public void SendEmail(Patient patient, DateTime time)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("xxxxxx", "xxxx@gmail.com"));
                message.To.Add(new MailboxAddress("Reschedule", patient.Email));
                message.Subject = "Reschedule";
                message.Body = new TextPart("plain")
                {
                    Text = $"Your appoitment is rescheduled for {time:MM/dd/yyyy HH:mm}"
                };

                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, false);
                    client.Authenticate("xxxxxxx@gmail.com", "password");
                    client.Send(message);
                    client.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Send(string message)
        {
            ITextMessage objectMessage;
            IConnectionFactory connectionFactory = new NMSConnectionFactory("tcp://activemq:61616");
            //IConnectionFactory connectionFactory = new NMSConnectionFactory("tcp://localhost:8888");

            IConnection connection = connectionFactory.CreateConnection();
            connection.Start();
            ISession session = connection.CreateSession();
            ActiveMQQueue queue = new ActiveMQQueue("porukeQueue, porukeQueueTestni");
            IMessageProducer messageProducer = session.CreateProducer();

            objectMessage = session.CreateTextMessage(message);

            messageProducer.Send(queue, objectMessage);
            session.Close();
            connection.Stop();
        }

        public void AddingAppointment(AppoitmentAdd patient)
        {
            throw new NotImplementedException();
        }
    }
}

