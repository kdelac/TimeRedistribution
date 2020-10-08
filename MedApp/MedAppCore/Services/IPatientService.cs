using MedAppCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MedAppCore.Services
{
    public interface IPatientService
    {
        Task<Patient> CreatePatient(Patient newPatient);
        Task DeletePatient(Patient patient);
        Task<IEnumerable<Patient>> GetAllWithAppointment();
        Task<Patient> GetPatientById(Guid id);
        Task UpdatePatient(Patient patientToBeUpdated, Patient patient);
        Task AddRangeAsync(IEnumerable<Patient> patients);
        Task<IEnumerable<Patient>> GetAll();
        List<Users> GetAllMongo();
        Task<Users> CreatePatientMongo(Users newPatient);
        Task<Users> GetPatientByIdMongo(Guid id);
        void AddRangeAsyncMongo(IEnumerable<Users> patients);
    }
}
