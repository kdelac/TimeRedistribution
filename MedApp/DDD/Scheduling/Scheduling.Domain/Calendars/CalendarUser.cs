using DDD.BuildingBlocks.Domain;
using Scheduling.Domain.Calendars;
using Scheduling.Domain.Calendars.Eventc;
using Scheduling.Domain.MedicalStaff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scheduling.Domain.CalendarUsers
{
    public class CalendarUser : Entity
    {
        internal CalendarId  CalendarId { get; private set; }
        internal MedicalStuffId MedicalStuffId { get; private set; }
        private MedicalStuffRole _role;

        public CalendarUser()
        {

        }

        internal static CalendarUser CreateNew(
            CalendarId calendarId,
            MedicalStuffId medicalStuffId,
            MedicalStuffRole medicalStuffRole
            )
        {
            return new CalendarUser(calendarId, medicalStuffId, medicalStuffRole);
        }


        private CalendarUser(
            CalendarId calendarId,
            MedicalStuffId medicalStuffId,
            MedicalStuffRole medicalStuffRole)
        {
            CalendarId = calendarId;
            MedicalStuffId = medicalStuffId;
            _role = medicalStuffRole;
            AddDomainEvent(new CalendarUserCreatedDomainEvent(CalendarId, MedicalStuffId));
        }

        public object CreateNew() => new { CalendarId = CalendarId.Value, MedicalStuffId = MedicalStuffId.Value, RoleCode = _role.Value };
        public List<IDomainEvent> GetDomainEvents() => this.DomainEvents.ToList();
    }
}
