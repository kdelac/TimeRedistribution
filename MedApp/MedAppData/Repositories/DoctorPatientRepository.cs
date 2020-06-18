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
    class DoctorPatientRepository : Repository<DoctorPatient>, IDoctorPatientRepository
    {
        public DoctorPatientRepository(MedAppDbContext context)
            : base(context)
        { }       

        public async Task<IEnumerable<DoctorPatient>> GetAllWithPatientsByDoctorIdAsync(Guid doctorId)
        {
            return await MedAppDbContext.DoctorPatients
                .Include(_ => _.Doctor)
                .Include(_ => _.Patient)
                .Where(_ => _.DoctorId == doctorId)
                .ToListAsync();
        }

        private MedAppDbContext MedAppDbContext
        {
            get { return Context as MedAppDbContext; }
        }
    }
}
