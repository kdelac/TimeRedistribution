using FluentValidation;
using MediatR;
using Scheduling.Application.Configurations.Commands;
using Scheduling.Domain.Calendars;
using Scheduling.Domain.MedicalStaff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Scheduling.Application.Calendars.AddNurseToCalendar
{
    internal class AddNurseToCalendarCommandHandler : ICommandHandler<AddNurseToCalendarCommand>
    {
        private ICalendarRepository _calendarRepository;
        private IMedicalRepository _medicalRepository;

        public AddNurseToCalendarCommandHandler(
            ICalendarRepository calendarRepository,
            IMedicalRepository medicalRepository)
        {
            _calendarRepository = calendarRepository;
            _medicalRepository = medicalRepository;
        }

        public async Task<Unit> Handle(AddNurseToCalendarCommand request, CancellationToken cancellationToken)
        {
            //var doctor = await _medicalRepository.GetByIdAsync(new MedicalStuffId(request.DoctorId));
            var doctor = await _medicalRepository.GetById(new MedicalStuffId(request.DoctorId));

            //var calendar = await _calendarRepository.GetByIdAsync(doctor.GetCalendarId());
            var calendar = await _calendarRepository.GetById(doctor.GetCalendarId());

            //calendar.AddAccesToCalendar(new MedicalStuffId(request.NurseId));
            calendar.AddAccesToCalendar(new MedicalStuffId(request.NurseId));

            await _calendarRepository.Add(calendar);


            //await _calendarRepository.Save();

            return Unit.Value;
        }
    }
}
