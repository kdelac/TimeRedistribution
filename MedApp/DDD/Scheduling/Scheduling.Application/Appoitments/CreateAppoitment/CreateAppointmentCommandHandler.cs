using FluentValidation;
using MediatR;
using Scheduling.Application.Configurations.Commands;
using Scheduling.Domain.Appointments;
using Scheduling.Domain.Calendars;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Scheduling.Application.Appoitments.CreateAppoitment
{
    internal class CreateAppointmentCommandHandler : ICommandHandler<CreateAppointmentCommand>
    {
        private IAppointmentRepository _appointmentRepository;
        private ICalendarRepository _calendarRepository;

        public CreateAppointmentCommandHandler(
            IAppointmentRepository appointmentRepository,
            ICalendarRepository calendarRepository)
        {
            _appointmentRepository = appointmentRepository;
            _calendarRepository = calendarRepository;
        }

        public async Task<Unit> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
        {
            var calendar = await _calendarRepository.GetByIdAsync(new CalendarId(request.CalendarId));

            var appoitment = calendar
                .AddAppoitment(AppointmentTerm.CreateNewStartAndEnd(
                    request.StartAppoitment, 
                    request.EndAppoitment),
                    request.PatientId);
            await _appointmentRepository.AddAsync(appoitment);
            await _appointmentRepository.Save();
            


            return Unit.Value;
        }
    }
}
