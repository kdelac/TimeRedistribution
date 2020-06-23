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
    class PatientRepository : Repository<Patient>, IPatientRepository
    {
        public PatientRepository(MedAppDbContext context)
            : base(context)
        { }

        public async Task<IEnumerable<Patient>> GetAllWithAppointmentAsync()
        {
            return await MedAppDbContext.Patients
                .Include(_ => _.Appointments)
                .ToListAsync();
        }

        public  IQueryable<Patient> GetAllWitPatientsByDoctorIdAsync(Guid doctorId)
        {
            var blogs = MedAppDbContext.Patients.FromSqlRaw($"SELECT p.Id, p.Name, p.Surname, p.Email, p.DateOfBirth FROM Patients  p right JOIN DoctorPatients dp ON p.Id = dp.PatientId WHERE dp.DoctorId = '{doctorId}'");
            return blogs;
        }

        public async Task<Patient> GetWithAppointmentByIdAsync(Guid id)
        {
            return await MedAppDbContext.Patients
                .Include(_ => _.Appointments)
                .SingleOrDefaultAsync(_ => _.Id == id);
        }

        private MedAppDbContext MedAppDbContext
        {
            get { return Context as MedAppDbContext; }
        }
    }
}
