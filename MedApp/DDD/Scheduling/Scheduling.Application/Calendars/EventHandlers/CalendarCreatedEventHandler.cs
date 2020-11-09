using MediatR;
using Scheduling.Domain.Calendars;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Scheduling.Application.Calendars.EventHandlers
{
    internal class CalendarCreatedEventHandler : INotificationHandler<CalendarCreatedDomainEvent>
    {
        private readonly ICalendarRepository _calendarRepository;
        public CalendarCreatedEventHandler(ICalendarRepository calendarRepository)
        {
            _calendarRepository = calendarRepository;
        }

        public async Task Handle(CalendarCreatedDomainEvent @event, CancellationToken cancellationToken)
        {
            var calendar = await _calendarRepository.GetById(@event.CalendarId);

            Console.WriteLine($"|||||||||||||||||||| CalendarID: {@event.CalendarId.Value} ||||||||| Title: {calendar.GetCalendatTitle()} ||||||||||| OcuredOn: {@event.OccurredOn} ||||||||||||||||||||");
        }
    }
}
