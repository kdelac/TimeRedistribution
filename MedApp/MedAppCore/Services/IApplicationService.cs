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
        Task DeleteApplication(Guid patientId);
    }
}
