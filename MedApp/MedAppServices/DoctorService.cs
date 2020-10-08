using MedAppCore;
using MedAppCore.Models;
using MedAppCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedAppServices
{
    public class DoctorService : IDoctorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMongoUnitOfWork _mongoUnitOfWork;
        private readonly IUsersService _usersService;

        public DoctorService(
            IUnitOfWork unitOfWork,
            IMongoUnitOfWork mongoUnitOfWork,
            IUsersService usersService
            )
        {
            _unitOfWork = unitOfWork;
            _mongoUnitOfWork = mongoUnitOfWork;
            _usersService = usersService;
        }

        public async Task AddRangeAsync(IEnumerable<Doctor> doctors)
        {
            await _unitOfWork.Doctors.AddRangeAsync(doctors);
            await _unitOfWork.Save();
        }

        public async Task<Doctor> CreateDoctor(Doctor newDoctor)
        {
            newDoctor.Id = Guid.NewGuid();
            await _unitOfWork.Doctors.AddAsync(newDoctor);
            await _unitOfWork.Save();
            return newDoctor;
        }

        public async Task DeleteDoctor(Doctor doctor)
        {
            _unitOfWork.Doctors.Remove(doctor);
            await _unitOfWork.Save();
        }

        public async Task<IEnumerable<Doctor>> GetAllWithAppointment()
        {
            return await _unitOfWork.Doctors.GetAllWithAppointmentAsync();
        }

        public Task<List<Doctor>> GetAllWithAppointmentExistAsync()
        {
            return _unitOfWork.Doctors.GetAllWithAppointmentExistAsync();
        }

        public async Task<Doctor> GetDoctorById(Guid id)
        {
            return await _unitOfWork.Doctors.GetByIdAsync(id);
        }

        public async Task UpdateDoctor(Doctor doctorToBeUpdated, Doctor doctor)
        {
            doctorToBeUpdated.Name = doctor.Name;
            doctorToBeUpdated.Surname = doctor.Surname;

            await _unitOfWork.Save();
        }

        public async Task<IEnumerable<Doctor>> GetAll()
        {
            return await _unitOfWork.Doctors.GetAllAsync();
        }

        #region mongo
        public List<Users> GetAllMongo()
        {
            return _usersService.GetAllDoctors();
        }

        public async Task UpdateDoctorMongo(Doctor doctorToBeUpdated, Doctor doctor)
        {
            doctorToBeUpdated.Name = doctor.Name;
            doctorToBeUpdated.Surname = doctor.Surname;

            await _mongoUnitOfWork.Doctors.Update(doctorToBeUpdated.Id, doctorToBeUpdated);
        }

        public async Task<Users> GetDoctorByIdMongo(Guid id)
        {
            return await _mongoUnitOfWork.Users.Get(id);
        }

        public void AddRangeAsyncMongo(IEnumerable<Users> doctors)
        {
            doctors.ToList().ForEach(async _ =>
            {
                _.Role = Role.Doctor;
                await _mongoUnitOfWork.Users.Create(_);
            });
        }

        public async Task DeleteDoctorMongo(Doctor doctor)
        {
            await _mongoUnitOfWork.Users.Delete(doctor.Id);
        }

        public async Task<Users> CreateDoctorMongo(Users user)
        {
            user.Role = Role.Doctor;
            await _usersService.CreateUser(user);
            return user;
        }
        #endregion
    }
}
