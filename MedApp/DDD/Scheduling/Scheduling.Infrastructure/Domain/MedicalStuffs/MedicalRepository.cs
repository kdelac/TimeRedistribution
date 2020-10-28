using Scheduling.Domain.MedicalStaff;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scheduling.Infrastructure.Domain.MedicalStuffs
{
    public class MedicalRepository : IMedicalRepository
    {
        private readonly SchedulingContext _calendarContext;

        internal MedicalRepository(SchedulingContext calendarContext)
        {
            _calendarContext = calendarContext;
        }
        public async Task AddAsync(MedicalStuff medical)
        {
            await _calendarContext.MedicalStuffs.AddAsync(medical);
        }

        public async Task<MedicalStuff> GetByIdAsync(MedicalStuffId id)
        {
            return await _calendarContext.MedicalStuffs.FindAsync(id);
        }
    }
}
