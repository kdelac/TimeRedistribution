using MedAppCore.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MedAppCore.Services
{
    public interface IDoctorService
    {
        Task<IEnumerable<Doctor>> GetAllWithAppointment();
        Task<Doctor> GetDoctorById(Guid id);
        Task<Doctor> CreateDoctor(Doctor newDoctor);
        Task UpdateDoctor(Doctor doctorToBeUpdated, Doctor doctor);
        Task DeleteDoctor(Doctor doctor);
        Task AddRangeAsync(IEnumerable<Doctor> doctors);
        Task<List<Doctor>> GetAllWithAppointmentExistAsync();
        Task<IEnumerable<Doctor>> GetAll();
        Task<IdentityResult> CreateNewUser(ApplicationUser newDoctor, string password);
        Task<IdentityResult> CreateRoleForUser(ApplicationUser newDoctor);
    }
}
