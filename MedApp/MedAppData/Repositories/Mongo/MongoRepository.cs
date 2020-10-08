using MedAppCore;
using MedAppCore.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedAppData.Repositories.Mongo
{
    public class MongoRepository<TEntity> : IMongoRepository<TEntity> where TEntity : class
    {
        protected readonly IMongoDBContext _mongoContext;
        protected IMongoCollection<TEntity> _dbCollection;

        protected MongoRepository(IMongoDBContext context)
        {
            _mongoContext = context;
            _dbCollection = _mongoContext.GetCollection<TEntity>(typeof(TEntity).Name);
        }
        public async Task Create(TEntity obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(typeof(TEntity).Name + " object is null");
            }
            _dbCollection = _mongoContext.GetCollection<TEntity>(typeof(TEntity).Name);
            await _dbCollection.InsertOneAsync(obj);
        }

        public Task Delete(Guid id)
        {
            return Task.Run(() =>
            {
                _dbCollection.DeleteOneAsync(Builders<TEntity>.Filter.Eq("_id", id));
            });          
        }

        public async Task<TEntity> Get(Guid id)
        {
            FilterDefinition<TEntity> filter = Builders<TEntity>.Filter.Eq("_id", id);

            _dbCollection = _mongoContext.GetCollection<TEntity>(typeof(TEntity).Name);

            return await _dbCollection.FindAsync(filter).Result.FirstOrDefaultAsync();

        }

        public async Task Update(Guid id, TEntity entity)
        {
            FilterDefinition<TEntity> filter = Builders<TEntity>.Filter.Eq("_id", id);

            _dbCollection = _mongoContext.GetCollection<TEntity>(typeof(TEntity).Name);

            await _dbCollection.FindOneAndReplaceAsync(filter, entity);

        }

        public async Task<IEnumerable<TEntity>> Get()
        {
            var all = await _dbCollection.FindAsync(Builders<TEntity>.Filter.Empty);
            return await all.ToListAsync();
        }

        public virtual IQueryable<TEntity> AsQueryable()
        {
            return _mongoContext.GetCollection<TEntity>(typeof(TEntity).Name).AsQueryable();
        }
    }
}
