using MedAppCore.Models;
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
        Task<Doctor> CreateDoctor(Doctor user);
        Task UpdateDoctor(Doctor doctorToBeUpdated, Doctor doctor);
        Task DeleteDoctor(Doctor doctor);
        Task AddRangeAsync(IEnumerable<Doctor> doctors);
        Task<List<Doctor>> GetAllWithAppointmentExistAsync();
        Task<IEnumerable<Doctor>> GetAll();
        List<Users> GetAllMongo();
        Task UpdateDoctorMongo(Doctor doctorToBeUpdated, Doctor doctor);
        Task<Users> GetDoctorByIdMongo(Guid id);
        void AddRangeAsyncMongo(IEnumerable<Users> doctors);
        Task DeleteDoctorMongo(Doctor doctor);
        Task<Users> CreateDoctorMongo(Users user);
    }
}
