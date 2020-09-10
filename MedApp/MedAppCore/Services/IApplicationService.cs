using MedAppCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MedAppCore.Services
{
    public interface IApplicationService
    {
        Task<int> NumberInside(Guid ordinationId);
        Task<int> NumberOutside(Guid ordinationId);
        Task<Application> CreateApplication(Application newApplication);
        Task DeleteApplication(Application application);
        Task<Application> GetApplicationByPatientId(Guid id);
        Task<Application> GetAppoitmentFirstDate();
        Task UpdateApplication(Application applicationToBeUpdated, Status2 status);
        Task<IEnumerable<Application>> GetAll();
    }
}
