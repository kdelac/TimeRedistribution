using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedAppCore.Repositories.ElasticSearch
{
    public interface IElasticSearchRepository<TEntity> where TEntity : class
    {
        void CreateIndex(string indexName);
        Task AddToIndex(List<TEntity> records, string indexName);
        Task DeleteIndexAsync(string indexName);
        Task DeleteAllFromIndex(string indexNames);
    }
}
