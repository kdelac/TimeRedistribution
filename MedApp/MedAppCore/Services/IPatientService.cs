using MedAppCore.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MedAppCore.Services
{
    public interface IPatientService
    {
        Task<IEnumerable<Patient>> GetAllWithAppointment();
        Task<Patient> GetPatientById(Guid id);
        Task<Patient> CreatePatient(Patient newPatient);
        Task UpdatePatient(Patient patientToBeUpdated, Patient patient);
        Task DeletePatient(Patient patient);
        Task AddRangeAsync(IEnumerable<Patient> patients);
        Task<IEnumerable<Patient>> GetAll();
        Task<IdentityResult> CreateNewUser(ApplicationUser newDoctor, string password);
        Task<IdentityResult> CreateRoleForUser(ApplicationUser newDoctor);
    }
}
