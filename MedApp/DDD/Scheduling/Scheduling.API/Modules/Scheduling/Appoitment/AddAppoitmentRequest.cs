using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduling.API.Modules.Scheduling.Appoitment
{
    public class AddAppoitmentRequest
    {
        public Guid CalendarId { get; set; }
        public string PatientId { get; set; }
        public DateTime StartAppoitment { get; set; }
        public DateTime EndAppoitment { get; set; }
    }
}
