using MedAppCore;
using MedAppCore.Models;
using MedAppCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedAppServices
{
    public class PatientService : IPatientService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMongoUnitOfWork _mongoUnitOfWork;
        private readonly IUsersService _usersService;

        public PatientService(
            IUnitOfWork unitOfWork,
            IMongoUnitOfWork mongoUnitOfWork,
            IUsersService usersService)
        {
            _unitOfWork = unitOfWork;
            _mongoUnitOfWork = mongoUnitOfWork;
            _usersService = usersService;
        }

        public async Task<Patient> CreatePatient(Patient newPatient)
        {
            newPatient.Id = Guid.NewGuid();
            await _unitOfWork.Patients.AddAsync(newPatient);
            await _unitOfWork.Save();

            return newPatient;
        }

        public async Task DeletePatient(Patient patient)
        {
            _unitOfWork.Patients.Remove(patient);
            await _unitOfWork.Save();
        }

        public async Task<IEnumerable<Patient>> GetAllWithAppointment()
        {
            return await _unitOfWork.Patients.GetAllWithAppointmentAsync();
        }

        public async Task<Patient> GetPatientById(Guid id)
        {
            return await _unitOfWork.Patients.GetByIdAsync(id);
        }

        public async Task UpdatePatient(Patient patientToBeUpdated, Patient patient)
        {
            patientToBeUpdated.Name = patient.Name;
            patientToBeUpdated.Surname = patient.Surname;
            patientToBeUpdated.Email = patient.Email;

            await _unitOfWork.Save();
        }

        public async Task AddRangeAsync(IEnumerable<Patient> patients)
        {
            await _unitOfWork.Patients.AddRangeAsync(patients);
            await _unitOfWork.Save();
        }

        public async Task<IEnumerable<Patient>> GetAll()
        {
            return await _unitOfWork.Patients.GetAllAsync();
        }        

        #region mongo
        public async Task<Users> CreatePatientMongo(Users newPatient)
        {
            newPatient.Role = Role.Patient;
            await _mongoUnitOfWork.Users.Create(newPatient);
            return newPatient;
        }

        public async Task<Users> GetPatientByIdMongo(Guid id)
        {
            return await _mongoUnitOfWork.Users.Get(id);
        }

        public List<Users> GetAllMongo()
        {
            return _usersService.GetAllPatients();
        }

        public void AddRangeAsyncMongo(IEnumerable<Users> patients)
        {
            patients.ToList().ForEach(async _ =>
            {
                _.Role = Role.Patient;
                await _mongoUnitOfWork.Users.Create(_);
            });
        }

        #endregion
    }
}
