using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedAppCore.Models;
using MedAppCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MedAppData.Repositories
{
    class DoctorRepository : Repository<Doctor>, IDoctorRepository
    {
        public DoctorRepository(MedAppDbContext context)
            : base(context)
        { }

        public async Task<IEnumerable<Doctor>> GetAllWithAppointmentAsync()
        {
            return await MedAppDbContext.Doctors
                .Include(_ => _.Appointments)
                .ToListAsync();
        }

        public async Task<List<Doctor>> GetAllWithAppointmentExistAsync()
        {
            return await MedAppDbContext.Doctors
                .Include(_ => _.Appointments)
                .Where(_ => _.Appointments.Count > 0)
                .ToListAsync();
        }

        public async Task<Doctor> GetWithAppointmentByIdAsync(Guid id)
        {
            return await MedAppDbContext.Doctors
                .Include(_ => _.Appointments)
                .SingleOrDefaultAsync(_ => _.Id == id);
        }

        private MedAppDbContext MedAppDbContext
        {
            get { return Context as MedAppDbContext; }
        }
    }
}
