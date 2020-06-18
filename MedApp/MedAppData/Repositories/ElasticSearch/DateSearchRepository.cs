using MedAppCore.Models.ElasticSearch;
using MedAppCore.Repositories.ElasticSearch;
using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace MedAppData.Repositories.ElasticSearch
{
    public class DateSearchRepository : ElasticSearchRepository<Date> ,IDateSearchRepository
    {
        public DateSearchRepository(ElasticClient client)
            : base(client)
        { }

        public ISearchResponse<Date> OnGet(DateTime keyWord, string indexName)
        {
            var result =
                 ElasticClient.Search<Date>(s => s      
                 .Index(indexName)
                 .AllowNoIndices()
                 .Query(q => q.Match(m => m
                            .Query(keyWord.ToString()))).Size(10000));

            return result;
        }

        private ElasticClient ElasticClient
        {
            get { return Client as ElasticClient; }
        }
    }
}
