using System;

namespace MedAppCore.Models
{
    public class Appointment
    {
        public Guid DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public Guid PatientId { get; set; }
        public Patient Patient { get; set; }
        public DateTime DateTime { get; set; }
        public string Status { get; set; }
    }
}
