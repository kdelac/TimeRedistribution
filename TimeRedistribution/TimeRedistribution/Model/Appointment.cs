using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeRedistribution.Model
{
    public class Appointment
    {
        public Guid DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public Guid PatientId { get; set; }
        public Patient Patient { get; set; }
        public DateTime Date { get; set; }
        public string Time { get; set; }
    }
}
