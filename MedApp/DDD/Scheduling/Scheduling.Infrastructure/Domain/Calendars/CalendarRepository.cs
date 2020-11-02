using Microsoft.EntityFrameworkCore;
using Scheduling.Domain.Calendars;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scheduling.Infrastructure.Domain.Calendars
{
    public class CalendarRepository : ICalendarRepository
    {
        private readonly SchedulingContext _schedulingContext;

        internal CalendarRepository(SchedulingContext schedulingContext)
        {
            _schedulingContext = schedulingContext;
        }

        public async Task AddAsync(Calendar calendar)
        {
            await _schedulingContext.Calendars.AddAsync(calendar);
        }

        public async Task<Calendar> GetByIdAsync(CalendarId id)
        {
            return await _schedulingContext.Calendars.FindAsync(id);
        }

        public async Task Save()
        {
            await _schedulingContext.SaveChangesAsync();
        }
    }
}
