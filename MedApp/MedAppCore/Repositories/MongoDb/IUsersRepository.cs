using MedAppCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MedAppCore.Repositories.MongoDb
{
    public interface IUsersRepository : IMongoRepository<Users>
    {
    }
}
