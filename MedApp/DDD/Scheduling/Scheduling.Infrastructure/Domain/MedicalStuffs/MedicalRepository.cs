using Scheduling.Domain.MedicalStaff;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scheduling.Infrastructure.Domain.MedicalStuffs
{
    public class MedicalRepository : IMedicalRepository
    {
        private readonly SchedulingContext _schedulingContext;

        internal MedicalRepository(SchedulingContext schedulingContext)
        {
            _schedulingContext = schedulingContext;
        }
        public async Task AddAsync(MedicalStuff medical)
        {
            await _schedulingContext.MedicalStuffs.AddAsync(medical);
        }

        public async Task<MedicalStuff> GetByIdAsync(MedicalStuffId id)
        {
            return await _schedulingContext.MedicalStuffs.FindAsync(id);
        }

        public async Task Save()
        {
            await _schedulingContext.SaveChangesAsync();
        }
    }
}
