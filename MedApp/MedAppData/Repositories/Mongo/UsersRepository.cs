using MedAppCore;
using MedAppCore.Models;
using MedAppCore.Repositories.MongoDb;
using System.Linq;

namespace MedAppData.Repositories.Mongo
{
    public class UsersRepository : MongoRepository<Users>, IUsersRepository
    {
        public UsersRepository(IMongoDBContext context) : base(context) { }
    }
}
