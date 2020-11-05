using MediatR;
using Scheduling.Domain.Calendars.Eventc;
using Scheduling.Domain.MedicalStaff;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Scheduling.Application.Calendars.EventHandlers
{
    internal class NurseAddedToCalendarEventHandler : INotificationHandler<CalendarUserCreatedDomainEvent>
    {
        private readonly IMedicalRepository _medicalRepository;
        public NurseAddedToCalendarEventHandler(IMedicalRepository medicalRepository)
        {
            _medicalRepository = medicalRepository;
        }

        public async Task Handle(CalendarUserCreatedDomainEvent @event, CancellationToken cancellationToken)
        {
            var medSestra = await _medicalRepository.GetById(@event.MedicalStuffId);

            Console.WriteLine($"|||||||||||||||||||| CalendarID: {@event.CalendarId.Value} |||||||||||||||||||| MedicinskaSestra: {medSestra.GetFirstname()} ||||||||||||||||||||");
        }
    }
}
