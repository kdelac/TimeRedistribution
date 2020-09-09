using MedAppCore.Repositories;
using MedAppCore.Repositories.ElasticSearch;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MedAppCore
{
    public interface IUnitOfWork : IDisposable
    {
        IDoctorRepository Doctors { get; }
        IAppointmentRepository Appointments { get; }
        IPatientRepository Patients { get; }
        //IUserSearchRepository UserSearch { get; }
        //IDateSearchRepository DateSearch { get; }
        IDoctorPatientRepository DoctorPatients { get; }
        IBillingRepository Billings { get; }
        ILogRepository LogRepository { get; }
        IApplicationRepository Applications { get; }
        IOrdinationRepository Ordinations { get; }
        Task<int> Save();
    }
}
