using MedAppCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MedAppCore.Services
{
    public interface IDoctorPatientService
    {
        Task<IEnumerable<DoctorPatient>> GetPatientByDoctorId(Guid doctorId);
    }
}
