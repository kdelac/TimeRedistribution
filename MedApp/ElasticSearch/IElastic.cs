using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ElasticSearch
{
    public interface IElastic<TEntity> where TEntity : class
    {
        void CreateIndex(string indexName);
        Task AddToIndex(List<TEntity> records, string indexName);
        Task DeleteIndexAsync(string indexName);
    }
}
