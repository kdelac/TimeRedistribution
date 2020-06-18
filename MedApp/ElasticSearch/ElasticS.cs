using Nest;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ElasticSearch
{
    public class ElasticS<TEntity> : IElastic<TEntity> where TEntity : class
    {
        protected readonly ElasticClient Client;

        public ElasticS(ElasticClient client)
        {
            Client = client;
        }

        public async Task DeleteIndexAsync(string indexName)
        {
            var index = await Client.Indices.ExistsAsync(indexName);

            if (index.Exists)
            {
                await Client.Indices.DeleteAsync(indexName);
            }
        }

        public void CreateIndex(string indexName)
        {
            var createIndexResponse = Client.Indices.Create(indexName,
            ind => ind.Map<TEntity>(x => x.AutoMap())
            );
        }

        public async Task AddToIndex(List<TEntity> records, string indexName)
        {
            var bullkResult =
                await Client
                .BulkAsync(b => b
                    .Index(indexName)
                    .CreateMany(records)
                );
        }

        public async Task DeleteAllFromIndex(string indexName)
        {
                await Client.DeleteByQueryAsync<TEntity>(_ => _.MatchAll());
        }
    }
}
