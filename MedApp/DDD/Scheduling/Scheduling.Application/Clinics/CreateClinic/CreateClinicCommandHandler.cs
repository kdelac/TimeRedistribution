using Scheduling.Application.Configurations.Commands;
using Scheduling.Domain.Clinics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Scheduling.Application.Clinics.CreateClinic
{
    internal class CreateClinicCommandHandler : ICommandHandler<CreateClinicCommand, Guid>
    {
        private IClinicRepository _clinicRepository;

        public CreateClinicCommandHandler(
            IClinicRepository clinicRepository)
        {
            _clinicRepository = clinicRepository;
        }

        public async Task<Guid> Handle(CreateClinicCommand request, CancellationToken cancellationToken)
        {
            var clinic = new Clinic(request.Name, request.Location);
            await _clinicRepository.AddAsync(clinic);
            await _clinicRepository.Save();

            return clinic.Id.Value;
        }
    }
}
