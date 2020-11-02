using Scheduling.Application.Configurations.Commands;
using Scheduling.Domain.Calendars;
using Scheduling.Domain.Clinics;
using Scheduling.Domain.MedicalStaff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Scheduling.Application.MedicalStaff.CreateMedicalStaff
{
    internal class CreateDoctorCommandHandler : ICommandHandler<CreateDoctorCommand, Guid>
    {
        private IClinicRepository _clinicRepository;
        private IMedicalRepository _medicalRepository;
        private ICalendarRepository _calendarRepository;

        public CreateDoctorCommandHandler(
            IClinicRepository clinicRepository,
            IMedicalRepository medicalRepository,
            ICalendarRepository calendarRepository)
        {
            _clinicRepository = clinicRepository;
            _medicalRepository = medicalRepository;
            _calendarRepository = calendarRepository;
        }

        public async Task<Guid> Handle(CreateDoctorCommand request, CancellationToken cancellationToken)
        {
            var clinic = await _clinicRepository.GetByIdAsync(new ClinicId(request.ClinicId));

            var nursesIds = request.NursesIds.Select(x => new MedicalStuffId(x)).ToList();

            var calendar = new Calendar($"{request.Firstname}'s Calendar", nursesIds);
            await _calendarRepository.AddAsync(calendar);

            var doctor = clinic.AddNewDoctor(request.Firstname, request.Lastname, request.DateOfBirth, calendar.Id);

            await _medicalRepository.AddAsync(doctor);

            await _calendarRepository.Save();
            await _medicalRepository.Save();

            return doctor.Id.Value;
        }
    }
}
