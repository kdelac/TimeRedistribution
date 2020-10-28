using DDD.BuildingBlocks.Domain;
using Scheduling.Domain.Appointments;
using Scheduling.Domain.CalendarUsers;
using Scheduling.Domain.MedicalStaff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scheduling.Domain.Calendars
{
    public class Calendar : Entity, IAggregateRoot
    {
        public CalendarId Id { get; private set; }
        private string _title;
        private List<CalendarUser> _calendarUsers;

        private Calendar()
        {
            _calendarUsers = new List<CalendarUser>();
        }

        public Calendar(
            string title,
            MedicalStuffId medicalStuffId)
        {
            Id = new CalendarId(Guid.NewGuid());
            _title = title;

            _calendarUsers = new List<CalendarUser>();

            _calendarUsers.Add(CalendarUser.CreateNew(Id, 
                medicalStuffId, MedicalStuffRole.Doctor));
        }

        public CalendarUser AddAccesToCalendar(
            MedicalStuffId medicalStuffId)
        {
            return CalendarUser.CreateNew(Id, medicalStuffId,
                MedicalStuffRole.Nurse);
        }
    }
}
