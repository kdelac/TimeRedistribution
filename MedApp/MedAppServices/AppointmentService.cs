using MedAppCore;
using MedAppCore.Models;
using MedAppCore.Services;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MedAppServices
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMongoUnitOfWork _mongounitOfWork;
        private readonly IDoctorService _doctorService;
        private readonly IPatientService _patientService;
        private readonly IUsersService _usersService;

        public AppointmentService(
            IUnitOfWork unitOfWork,
            IMongoUnitOfWork mongoUnitOfWork,
            IDoctorService doctorService,
            IPatientService patientService,
            IUsersService usersService
            )
        {
            _unitOfWork = unitOfWork;
            _mongounitOfWork = mongoUnitOfWork;
            _doctorService = doctorService;
            _patientService = patientService;
            _usersService = usersService;
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

        public async Task<Appointment> GetAppointmentById(Guid id)
        {
            return await _unitOfWork.Appointments.GetAppointmentById(id);
        }

        #region mongo
        public async Task<AppointmentBase> CreateAppointmentMongo(AppointmentBase newAppointment)
        {
            await _mongounitOfWork.Appointments.Create(newAppointment);
            return newAppointment;
        }

        public async Task DeleteAppointmentMongo(AppointmentBase appointment)
        {
            await _mongounitOfWork.Appointments.Delete(appointment.Id);
        }

        public async Task<IEnumerable<AppointmentBase>> GetAllMogno()
        {
            return await _mongounitOfWork.Appointments.Get();
        }

        public async Task<AppointmentBase> GetAppointmentByIdMongo(Guid id)
        {
            return await _mongounitOfWork.Appointments.Get(id);
        }
        public async Task UpdateAppointmentMongo(AppointmentBase appointmentToBeUpdated, AppointmentBase appointment)
        {
            appointmentToBeUpdated.Status = appointment.Status;
            appointmentToBeUpdated.DateTime = appointment.DateTime;

            await _mongounitOfWork.Appointments.Update(appointmentToBeUpdated.Id, appointment);
        }
        #endregion

    }
}
