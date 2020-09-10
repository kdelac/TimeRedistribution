using MedAppCore;
using MedAppCore.Models;
using MedAppCore.Services;
using System;
using System.Collections.Generic;
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

        public async Task DeleteApplication(Application application)
        {
            _unitOfWork.Applications.Remove(application);
            await _unitOfWork.Save();
        }

        public async Task<IEnumerable<Application>> GetAll()
        {
            return await _unitOfWork.Applications.GetAllAsync();
        }

        public async Task<Application> GetApplicationByPatientId(Guid id)
        {
            return await _unitOfWork.Applications.GetAppoitmentWithPatientId(id);
        }

        public async Task<Application> GetAppoitmentFirstDate()
        {
            return await _unitOfWork.Applications.GetAppoitmentFirstDate();
        }

        public async Task<int> NumberInside(Guid ordinationId)
        {
            return await _unitOfWork.Applications.GetNumberInside(ordinationId);
        }

        public async Task<int> NumberOutside(Guid ordinationId)
        {
            return await _unitOfWork.Applications.GetNumberOutside(ordinationId);
        }

        public async Task UpdateApplication(Application applicationToBeUpdated, Status2 status)
        {
            applicationToBeUpdated.Position = status;

            await _unitOfWork.Save();
        }
    }
}