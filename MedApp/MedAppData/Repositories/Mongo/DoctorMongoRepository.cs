using MedAppCore;
using MedAppCore.Models;
using MedAppCore.MongoDB;

namespace MedAppData.Repositories.Mongo
{
    public class DoctorMongoRepository : MongoRepository<Doctor>, IDoctorMongoRepository
    {
        public DoctorMongoRepository(IMongoDBContext context) : base(context){}
    }
}
