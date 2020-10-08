using MedAppCore.Models;
using MedAppCore.Repositories;

namespace MedAppCore.MongoDB
{
    public interface IDoctorMongoRepository : IMongoRepository<Doctor>
    {
    }
}
