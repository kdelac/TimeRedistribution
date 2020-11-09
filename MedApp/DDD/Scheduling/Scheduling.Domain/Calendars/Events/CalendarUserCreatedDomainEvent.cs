using DDD.BuildingBlocks.Domain;
using Scheduling.Domain.MedicalStaff;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduling.Domain.Calendars.Eventc
{
    public class CalendarUserCreatedDomainEvent : DomainEventBase
    {
        public CalendarUserCreatedDomainEvent(CalendarId calendarId, MedicalStuffId medicalStuffId)
        {
            CalendarId = calendarId;
            MedicalStuffId = medicalStuffId;
        }

        public CalendarId CalendarId { get; }
        public MedicalStuffId MedicalStuffId { get; }

    }
}
