using Scheduling.Application.Configurations.Queries;
using Scheduling.Domain.MedicalStaff;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Scheduling.Application.MedicalStaff.GetAllMedicalStaff
{
    internal class GetAllMedicalStaffQueryHandler : IQueryHandler<GetAllMedicalStaffQuery, List<MedicalStaffDto>>
    {
        private readonly IMedicalRepository _medicalRepository;

        internal GetAllMedicalStaffQueryHandler(IMedicalRepository medicalRepository)
        {
            _medicalRepository = medicalRepository;
        }

        public async Task<List<MedicalStaffDto>> Handle(GetAllMedicalStaffQuery request, CancellationToken cancellationToken)
        {
            List<MedicalStaffDto> medicalStaffDtos = new List<MedicalStaffDto>();
            var medical = await _medicalRepository.GetAll();


            return medicalStaffDtos;
        }
    }
}
