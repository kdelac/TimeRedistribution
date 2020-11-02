using Scheduling.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduling.Application.Calendars.AddNurseToCalendar
{
    public class AddNurseToCalendarCommand : CommandBase
    {
        public Guid DoctorId { get; set; }
        public Guid NurseId { get; set; }

        public AddNurseToCalendarCommand(Guid doctorId, Guid nurseId)
        {
            DoctorId = doctorId;
            NurseId = nurseId;
        }
    }
}
