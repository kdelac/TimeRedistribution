using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduling.API.Modules.Scheduling.Calendar
{
    public class AddAccesToCalendarRequest
    {
        public Guid DoctorId { get; set; }
        public Guid NurseId { get; set; }
    }
}
