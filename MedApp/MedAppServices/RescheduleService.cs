using MedAppCore;
using System;
using System.Collections.Generic;
using System.Text;
using MedAppCore.Models;
using MedAppCore.Services;
using System.Threading.Tasks;
using System.Linq;
using MimeKit;

namespace MedAppServices
{
    public class RescheduleService : IRescheduleService
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IDoctorService _doctorService;
        private List<Appointment> appoitmentsToReschedule;

        public RescheduleService(IAppointmentService appointmentService, IDoctorService doctorService)
        {
            _appointmentService = appointmentService;
            _doctorService = doctorService;
        }

        public void Reschedule()
        {
            List<Appointment> appointments =  _appointmentService.GetAllForDateAndStatus(DateTime.Now, "Waiting").Result;
            List<Doctor> doctors =  _doctorService.GetAllWithAppointmentExistAsync().Result;
  
            TimeSpan tenMin = new TimeSpan(0, 5, 0);
            
            foreach (Doctor item in doctors)
            {
                appoitmentsToReschedule = new List<Appointment>();
                appoitmentsToReschedule = appointments.OrderBy(b => b.DateTime).Where(a => a.DoctorId == item.Id).ToList();

                if (appoitmentsToReschedule.Count() > 0 && Check(appoitmentsToReschedule.FirstOrDefault(), tenMin))
                {
                    ChangeAppoitments(appoitmentsToReschedule, tenMin);
                }
            }
        }

        public void ChangeAppoitments(List<Appointment> appoitments, TimeSpan a)
        {
            for (int i = 0; i < appoitments.Count(); i++)
            {
                UpdateAppoitment(appoitments[i], a);

                if (i < appoitments.Count() - 1 && CheckGap(appoitments[i], appoitments[i + 1], a))
                {
                    break;
                }

                if (i < appoitments.Count() - 2 && CheckGap(appoitments[i + 1], appoitments[i + 2], a))
                {
                    UpdateAppoitment(appoitments[i + 1], a);
                    break;
                }
            }
        }

        public void  UpdateAppoitment(Appointment appointment, TimeSpan appoitmentExtend)
        {
            Appointment appointmentToBeUpdated = new Appointment();
            appointmentToBeUpdated.DoctorId = appointment.DoctorId;
            appointmentToBeUpdated.PatientId = appointment.PatientId;
            appointmentToBeUpdated.Status = "Waiting";
            appointmentToBeUpdated.DateTime = appointment.DateTime.Date + appointment.DateTime.TimeOfDay + appoitmentExtend;
            _appointmentService.UpdateAppointment(appointment, appointmentToBeUpdated);
            //SendEmail(FindPatient(appointment.PatientId), appointment.DateTime);
        }

        public bool CheckGap(Appointment appoitment, Appointment appoitmentNext, TimeSpan a)
        {
            if (appoitmentNext.DateTime.TimeOfDay - appoitment.DateTime.TimeOfDay > a)
            {
                return true;
            }
            return false;
        }

        public bool Check(Appointment appointment, TimeSpan ts)
        {
            TimeSpan vr = DateTime.Now.TimeOfDay - ts;
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
    }
}

