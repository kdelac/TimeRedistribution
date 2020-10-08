using MedAppCore.Models;
using MongoDB.Driver;

namespace MedAppCore
{
    public interface IMongoDBContext
    {
        IMongoCollection<Users> GetCollection<Users>(string name);
    }
}
