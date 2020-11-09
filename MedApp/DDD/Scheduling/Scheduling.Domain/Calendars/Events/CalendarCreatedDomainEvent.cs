using DDD.BuildingBlocks.Domain;

namespace Scheduling.Domain.Calendars
{
    public class CalendarCreatedDomainEvent : DomainEventBase
    {
        public CalendarCreatedDomainEvent(CalendarId calendarId)
        {
            CalendarId = calendarId;
        }

        public CalendarId CalendarId { get; }

    }
}
