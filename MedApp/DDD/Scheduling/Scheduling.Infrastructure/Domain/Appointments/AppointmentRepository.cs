using Scheduling.Domain.Appointments;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scheduling.Infrastructure.Domain.Appointments
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly SchedulingContext _calendarContext;

        internal AppointmentRepository(SchedulingContext calendarContext)
        {
            _calendarContext = calendarContext;
        }
        public async Task AddAsync(Appointment appointment)
        {
            await _calendarContext.Appointments.AddAsync(appointment);
        }

        public async Task<Appointment> GetByIdAsync(AppoitmentId id)
        {
            return await _calendarContext.Appointments.FindAsync(id);
        }
    }
}
