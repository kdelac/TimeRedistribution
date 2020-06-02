using MedAppCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MedAppCore.Services
{
    public interface IAppointmentService
    {
        Task<IEnumerable<Appointment>> GetAll();
        Task<IEnumerable<Appointment>> GetMusicAppointmentByPatientId(Guid patientId);
        Task<IEnumerable<Appointment>> GetAppointmentByDoctorId(Guid doctorId);
        Task<Appointment> GetAppointmentForDoctorAndPatient(Guid doctorId, Guid patientId);
        Task<Appointment> CreateAppointment(Appointment newAppointment);
        Task UpdateAppointment(Appointment appointmentToBeUpdated, Appointment appointment);
        Task DeleteAppointment(Appointment appointment);
        Task<IEnumerable<Appointment>> GetAllWithPatientsAndDoctorAsync();
        Task AddRangeAsync(IEnumerable<Appointment> appointments);
        Task<IEnumerable<Appointment>> GetAllForDoctorAndDate(Guid doctorId, DateTime date);
        Task<List<Appointment>> GetAllForDateAndStatus(DateTime dateTime, string status);
    }
}
