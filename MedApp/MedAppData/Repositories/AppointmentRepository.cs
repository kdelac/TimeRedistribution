using MedAppCore.Models;
using MedAppCore.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedAppData.Repositories
{
    class AppointmentRepository : Repository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(MedAppDbContext context)
            : base(context)
        { }    

        public async Task<IEnumerable<Appointment>> GetAllWithDoctorsByPatientIdAsync(Guid patientId)
        {
            return await MedAppDbContext.Appointments
                .Include(_ => _.Doctor)
                .Where(_ => _.PatientId == patientId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetAllWithPatientsByDoctorIdAsync(Guid doctorId)
        {
            return await MedAppDbContext.Appointments
                .Include(_ => _.Patient)
                .Where(_ => _.DoctorId == doctorId)
                .ToListAsync();
        }

        public async Task<Appointment> GetAppointment(Guid doctorId, Guid patientId)
        {
            return await MedAppDbContext.Appointments
                .Include(_ => _.Doctor)
                .Include(_ => _.Patient)
                .FirstOrDefaultAsync(_ => _.DoctorId == doctorId && _.PatientId == patientId);
        }

        public async Task<IEnumerable<Appointment>> GetAllWithPatientsAndDoctorAsync()
        {
            return await MedAppDbContext.Appointments
                .Include(_ => _.Doctor)
                .Include(_ => _.Patient)
                .ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetAllForDoctorAndDate(Guid doctorId, DateTime date)
        {
            return await MedAppDbContext.Appointments
                .Where(_ => _.DoctorId == doctorId && _.DateTime == date)
                .OrderByDescending(_ => _.DateTime)
                .ToListAsync();
        }

        public async Task<List<Appointment>> GetAllForDateAndStatus(DateTime dateTime, string status)
        {
            return await MedAppDbContext.Appointments
                .Include(_ => _.Patient)
                .Include(_ => _.Doctor)
                .Where(_ => _.DateTime.Date == dateTime.Date && _.Status == status)
                .ToListAsync();
        }

        private MedAppDbContext MedAppDbContext
        {
            get { return Context as MedAppDbContext; }
        }
    }
}
