using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeRedistribution.Resources
{
    public class AppointmentResource
    {
        public Guid DoctorId { get; set; }
        public Guid PatientId { get; set; }
        public DateTime DateTime { get; set; }
        public string Status { get; set; }
    }
}
