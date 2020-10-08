using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedAppCore.Repositories
{
    public interface IMongoRepository<TEntity> where TEntity : class
    {
        Task Create(TEntity obj);
        Task Update(Guid id, TEntity entity);
        Task Delete(Guid id);
        Task<TEntity> Get(Guid id);
        Task<IEnumerable<TEntity>> Get();
        IQueryable<TEntity> AsQueryable();
    }
}
