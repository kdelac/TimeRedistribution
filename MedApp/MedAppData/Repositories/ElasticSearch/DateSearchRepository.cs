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
            DateTime dt = new DateTime(keyWord.Year, keyWord.Month, keyWord.Day, 0, 0, 0);
            var result =
                 ElasticClient.Search<Date>(s => s      
                 .Index(indexName)
                 .AllowNoIndices()
                 .Query(q => q.DateRange(d => d
                                        .Field(fl => fl.DateTime)
                                        .GreaterThanOrEquals(dt)
                                        .LessThan(dt.AddDays(1)))).Size(10000));

            return result;
        }

        private ElasticClient ElasticClient
        {
            get { return Client as ElasticClient; }
        }
    }
}
