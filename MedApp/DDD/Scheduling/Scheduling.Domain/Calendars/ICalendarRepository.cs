using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scheduling.Domain.Calendars
{
    public interface ICalendarRepository
    {
        Task AddAsync(Calendar calendar);
        Task Add(Calendar calendar);

        Task<Calendar> GetByIdAsync(CalendarId id);
        Task<Calendar> GetById(CalendarId id);
        Task Save();
    }
}
