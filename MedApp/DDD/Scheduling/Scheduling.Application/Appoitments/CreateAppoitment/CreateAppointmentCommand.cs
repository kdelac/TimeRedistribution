using Scheduling.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduling.Application.Appoitments.CreateAppoitment
{
    public class CreateAppointmentCommand : CommandBase
    {
        public CreateAppointmentCommand(Guid calendarId, 
            string patientId,
            DateTime start,
            DateTime end)
        {
            CalendarId = calendarId;
            PatientId = patientId;
            StartAppoitment = start;
            EndAppoitment = end;
        }

        public Guid CalendarId { get; }
        public string PatientId { get; }
        public DateTime StartAppoitment { get; }
        public DateTime EndAppoitment { get; }
    }
}
