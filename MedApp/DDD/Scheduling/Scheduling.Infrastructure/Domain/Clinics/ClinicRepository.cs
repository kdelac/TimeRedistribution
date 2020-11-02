using Scheduling.Domain.Clinics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scheduling.Infrastructure.Domain.Clinics
{
    public class ClinicRepository : IClinicRepository
    {
        private readonly SchedulingContext _schedulingContext;

        public ClinicRepository(SchedulingContext schedulingContext)
        {
            _schedulingContext = schedulingContext;
        }

        public async Task AddAsync(Clinic clinic)
        {
            await _schedulingContext.Clinics.AddAsync(clinic);
        }

        public async Task<Clinic> GetByIdAsync(ClinicId id)
        {
            return await _schedulingContext.Clinics.FindAsync(id);
        }

        public async Task Save()
        {
            await _schedulingContext.SaveChangesAsync();
        }
    }
}
