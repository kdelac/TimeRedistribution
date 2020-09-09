﻿using MedAppCore.Models;
using System;
using System.Threading.Tasks;

namespace MedAppCore.Repositories
{
    public interface IApplicationRepository : IRepository<Application>
    {
        Task DeleteAppoitmentWithPatientId(Guid patientId);
        Task<int> GetNumberInside(Guid ordinationId);
        Task<int> GetNumberOutside(Guid ordinationId);
    }
}
