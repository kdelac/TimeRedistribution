using MedAppCore;
using MedAppCore.Models;
using MedAppCore.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MedAppServices
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppointmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddRangeAsync(IEnumerable<Appointment> appointments)
        {
            await _unitOfWork.Appointments.AddRangeAsync(appointments);
            await _unitOfWork.Save();
        }

        public async Task<Appointment> CreateAppointment(Appointment newAppointment)
        {
            newAppointment.Id = Guid.NewGuid();
            await _unitOfWork.Appointments.AddAsync(newAppointment);
            await _unitOfWork.Save();
            return newAppointment;
        }

        public async Task DeleteAppointment(Appointment appointment)
        {
            _unitOfWork.Appointments.Remove(appointment);
            await _unitOfWork.Save();
        }

        public async Task<IEnumerable<Appointment>> GetAll()
        {
            return await _unitOfWork.Appointments.GetAllAsync();
        }

        public async Task<List<Appointment>> GetAllForDateAndStatus(DateTime dateTime, string status)
        {
            return await _unitOfWork.Appointments.GetAllForDateAndStatus(dateTime, status);
        }

        public async Task<IEnumerable<Appointment>> GetAllForDoctorAndDate(Guid doctorId, DateTime date)
        {
            return await _unitOfWork.Appointments.GetAllForDoctorAndDate(doctorId, date);
        }

        public Task<IEnumerable<Appointment>> GetAllWithDoctor()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Appointment>> GetAllWithPatientsAndDoctorAsync()
        {
            return await _unitOfWork.Appointments.GetAllWithPatientsAndDoctorAsync();
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentByDoctorId(Guid doctorId)
        {
            return await _unitOfWork.Appointments.GetAllWithPatientsByDoctorIdAsync(doctorId);
        }

        public async Task<Appointment> GetAppointmentForDoctorAndPatient(Guid doctorId, Guid patientId)
        {
            return await _unitOfWork.Appointments.GetAppointment(doctorId, patientId);
        }

        public async Task<IEnumerable<Appointment>> GetMusicAppointmentByPatientId(Guid patientId)
        {
            return await _unitOfWork.Appointments.GetAllWithPatientsByDoctorIdAsync(patientId);
        }

        public async Task UpdateAppointment(Appointment appointmentToBeUpdated, Appointment appointment)
        {
            appointmentToBeUpdated.DateTime = appointment.DateTime;
            appointmentToBeUpdated.Status = appointment.Status;

            await _unitOfWork.Save();
        }

        public async Task UpdateAppointment(DateTime date, Guid doctorId, Guid patientId)
        {
            await _unitOfWork.Appointments.UpdateAppointment(date, doctorId, patientId);
        }
    }
}
