using System;
using System.Collections.Generic;

namespace AppoitmentRedistribution.Models
{
    public partial class Appointments
    {
        public Guid DoctorId { get; set; }
        public Guid PatientId { get; set; }
        public DateTime DateTime { get; set; }
        public string Status { get; set; }

        public virtual Doctors Doctor { get; set; }
        public virtual Patients Patient { get; set; }
    }
}
