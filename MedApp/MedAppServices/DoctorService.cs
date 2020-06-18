﻿using ElasticSearch;
using MedAppCore;
using MedAppCore.Models;
using MedAppCore.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MedAppServices
{
    public class DoctorService : IDoctorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Work _work;

        public DoctorService(IUnitOfWork unitOfWork, Work work)
        {
            _unitOfWork = unitOfWork;
            _work = work;
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
    }
}
