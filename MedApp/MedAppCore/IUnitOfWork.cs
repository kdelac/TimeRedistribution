using MedAppCore.Repositories;
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
        Task<int> Save();
    }
}
