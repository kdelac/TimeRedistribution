using System;
using System.Collections.Generic;
using System.Text;

namespace MedAppCore.Models
{
    [Serializable]
    public class AppoitmentAdd
    {
        public Guid DoctorId { get; set; }
        public Guid PatientId { get; set; }
        public DateTime DateTime { get; set; }
        public string Status { get; set; }
    }
}
