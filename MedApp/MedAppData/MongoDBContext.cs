using MedAppCore;
using MedAppCore.MongoDB;
using MongoDB.Driver;

namespace MedAppData
{
    public class MongoDBContext : IMongoDBContext
    {
        private IMongoDatabase _db { get; set; }
        private MongoClient _mongoClient { get; set; }
        public IClientSessionHandle Session { get; set; }
        public MongoDBContext()
        {
            _mongoClient = new MongoClient("mongodb://localhost:27017");
            _db = _mongoClient.GetDatabase("TimeRedistribution");
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _db.GetCollection<T>(name);
        }
    }
}
