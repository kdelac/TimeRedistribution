using System.Threading.Tasks;

namespace Scheduling.Domain.Clinics
{
    public interface IClinicRepository
    {
        Task AddAsync(Clinic calendar);

        Task<Clinic> GetByIdAsync(ClinicId id);
    }
}
