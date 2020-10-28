using Scheduling.Domain.Calendars;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scheduling.Infrastructure.Domain.Calendars
{
    public class CalendarRepository : ICalendarRepository
    {
        private readonly SchedulingContext _calendarContext;

        internal CalendarRepository(SchedulingContext calendarContext)
        {
            _calendarContext = calendarContext;
        }

        public async Task AddAsync(Calendar calendar)
        {
            await _calendarContext.Calendars.AddAsync(calendar);
        }

        public async Task<Calendar> GetByIdAsync(CalendarId id)
        {
            return await _calendarContext.Calendars.FindAsync(id);
        }
    }
}
