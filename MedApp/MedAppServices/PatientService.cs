using MedAppCore;
using MedAppCore.Models;
using MedAppCore.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MedAppServices
{
    public class PatientService : IPatientService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PatientService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
    }
}
