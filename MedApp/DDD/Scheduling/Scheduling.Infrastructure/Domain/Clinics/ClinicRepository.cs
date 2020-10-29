using Scheduling.Domain.Clinics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scheduling.Infrastructure.Domain.Clinics
{
    public class ClinicRepository : IClinicRepository
    {
        private readonly SchedulingContext _calendarContext;

        internal ClinicRepository(SchedulingContext calendarContext)
        {
            _calendarContext = calendarContext;
        }

        public async Task AddAsync(Clinic clinic)
        {
            await _calendarContext.Clinics.AddAsync(clinic);
            await _calendarContext.SaveChangesAsync();
        }

        public async Task<Clinic> GetByIdAsync(ClinicId id)
        {
            return await _calendarContext.Clinics.FindAsync(id);
        }
    }
}
