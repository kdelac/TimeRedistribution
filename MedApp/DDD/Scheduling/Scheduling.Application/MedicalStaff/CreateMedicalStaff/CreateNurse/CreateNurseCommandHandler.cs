using Scheduling.Application.Configurations.Commands;
using Scheduling.Domain.Clinics;
using Scheduling.Domain.MedicalStaff;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Scheduling.Application.MedicalStaff.CreateMedicalStaff.CreateNurse
{
    internal class CreateNurseCommandHandler : ICommandHandler<CreateNurseCommand, Guid>
    {
        private IClinicRepository _clinicRepository;
        private IMedicalRepository _medicalRepository;

        public CreateNurseCommandHandler(
            IClinicRepository clinicRepository,
            IMedicalRepository medicalRepository)
        {
            _clinicRepository = clinicRepository;
            _medicalRepository = medicalRepository;
        }

        public async Task<Guid> Handle(CreateNurseCommand request, CancellationToken cancellationToken)
        {
            var clinic = await _clinicRepository.GetByIdAsync(new ClinicId(request.ClinicId));

            var nurse = clinic.AddNewNurse(request.Firstname, request.Lastname, request.DateOfBirth);

            await _medicalRepository.AddAsync(nurse);
            await _medicalRepository.Save();

            return nurse.Id.Value;
        }
    }
}
