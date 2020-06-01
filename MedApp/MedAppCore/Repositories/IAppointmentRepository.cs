﻿using MedAppCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MedAppCore.Repositories
{
    public interface IAppointmentRepository : IRepository<Appointment>
    {
        Task<IEnumerable<Appointment>> GetAllWithDoctorsByPatientIdAsync(Guid patientId);
        Task<IEnumerable<Appointment>> GetAllWithPatientsByDoctorIdAsync(Guid doctorId);
        Task<IEnumerable<Appointment>> GetAllWithPatientsAndDoctorAsync();
        Task<Appointment> GetAppointment(Guid doctorId, Guid patientId);
    }
}
