using Scheduling.Domain.Appointments;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scheduling.Infrastructure.Domain.Appointments
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly SchedulingContext _schedulingContext;

        internal AppointmentRepository(SchedulingContext schedulingContext)
        {
            _schedulingContext = schedulingContext;
        }
        public async Task AddAsync(Appointment appointment)
        {
            await _schedulingContext.Appointments.AddAsync(appointment);

        }

        public async Task<Appointment> GetByIdAsync(AppoitmentId id)
        {
            return await _schedulingContext.Appointments.FindAsync(id);
        }

        public async Task Save()
        {
            await _schedulingContext.SaveChangesAsync();
        }
    }
}
