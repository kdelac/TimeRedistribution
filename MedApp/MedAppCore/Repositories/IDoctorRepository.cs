using MedAppCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MedAppCore.Repositories
{
    public interface IDoctorRepository : IRepository<Doctor>
    {
        Task<IEnumerable<Doctor>> GetAllWithAppointmentAsync();
        Task<Doctor> GetWithAppointmentByIdAsync(Guid id);
        Task<List<Doctor>> GetAllWithAppointmentExistAsync(DateTime date);
    }
}
