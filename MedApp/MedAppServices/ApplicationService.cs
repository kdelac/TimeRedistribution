using MedAppCore;
using MedAppCore.Models;
using MedAppCore.Services;
using System;
using System.Threading.Tasks;

namespace MedAppServices
{
    public class ApplicationService : IApplicationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ApplicationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Application> CreateApplication(Application newApplication)
        {
            await _unitOfWork.Applications.AddAsync(newApplication);
            await _unitOfWork.Save();
            return newApplication;
        }

        public async Task DeleteApplication(Guid patientId)
        {
            await _unitOfWork.Applications.DeleteAppoitmentWithPatientId(patientId);
            await _unitOfWork.Save();
        }

        public async Task<int> NumberInside(Guid ordinationId)
        {
            return await _unitOfWork.Applications.GetNumberInside(ordinationId);
        }

        public async Task<int> NumberOutside(Guid ordinationId)
        {
            return await _unitOfWork.Applications.GetNumberOutside(ordinationId);
        }
    }
}