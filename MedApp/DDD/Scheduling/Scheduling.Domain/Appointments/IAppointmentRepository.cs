using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scheduling.Domain.Appointments
{
    public interface IAppointmentRepository
    {
        Task AddAsync(Appointment calendar);

        Task<Appointment> GetByIdAsync(AppoitmentId id);

        Task Save();
    }
}
