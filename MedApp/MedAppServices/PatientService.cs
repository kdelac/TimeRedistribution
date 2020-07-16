using MedAppCore;
using MedAppCore.Models;
using MedAppCore.Repositories;
using MedAppCore.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MedAppServices
{
    public class PatientService : IPatientService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository<ApplicationUser> _userRepository;
        private readonly string role = "Patient";

        public PatientService(IUnitOfWork unitOfWork, IUserRepository<ApplicationUser> userRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
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

        public async Task<IdentityResult> CreateNewUser(ApplicationUser newDoctor, string password)
        {
            var newUser = await _userRepository.AddAsync(newDoctor, password);
            return newUser;
        }

        public async Task<IdentityResult> CreateRoleForUser(ApplicationUser newDoctor)
        {
            var newUser = await _userRepository.AddToRoleAsync(newDoctor, role);
            return newUser;
        }
    }
}
