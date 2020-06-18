using MedAppCore;
using MedAppCore.Models;
using MedAppCore.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MedAppServices
{
    public class DoctorPatientService : IDoctorPatientService
    {
        private readonly IUnitOfWork _unitOfWork;
        public DoctorPatientService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<DoctorPatient>> GetPatientByDoctorId(Guid doctorId)
        {
            return await _unitOfWork.DoctorPatients.GetAllWithPatientsByDoctorIdAsync(doctorId);
        }
    }
}
