using MedAppCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MedAppCore.Repositories
{
    public interface IDoctorPatientRepository
    {
        Task<IEnumerable<DoctorPatient>> GetAllWithPatientsByDoctorIdAsync(Guid doctorId);
    }
}
