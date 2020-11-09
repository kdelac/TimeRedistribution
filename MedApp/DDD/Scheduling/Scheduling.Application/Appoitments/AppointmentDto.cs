using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduling.Application.Appoitments
{
    public class AppointmentDto
    {
        public Guid Id { get; set; }
        public Guid CalendarId { get; set; }
        public string PatientId { get; set; }
        public DateTime StartAppoitmentDate { get; set; }
        public DateTime EndAppoitmentAppoitmentDate { get; set; }
    }
}
