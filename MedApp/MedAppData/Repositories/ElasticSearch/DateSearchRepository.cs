using MedAppCore.Models.ElasticSearch;
using MedAppCore.Repositories.ElasticSearch;
using Nest;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace MedAppData.Repositories.ElasticSearch
{
    public class DateSearchRepository : ElasticSearchRepository<Date> ,IDateSearchRepository
    {
        public DateSearchRepository(ElasticClient client)
            : base(client)
        { }

        public ISearchResponse<Date> OnGet(DateTime keyWord, string indexName, int? skip, int? size, string type)
        {
            if (!size.HasValue && skip.HasValue)
            {
                size = 10000-skip;
            }

            if (!size.HasValue && !skip.HasValue)
            {
                size = 10000;
            }

            DateTime dt = new DateTime(keyWord.Year, keyWord.Month, keyWord.Day, 0, 0, 0);
            if (type != null)
            {                
                var result =
                     ElasticClient.Search<Date>(s => s
                     .Index(indexName)
                     .AllowNoIndices()
                     .Query(q => q
                            .Match(m => m.Field(f => f.Type)
                            .Query(type.ToString())) && 
                            q.DateRange(d => d
                                .Field(fl => fl.DateTime)
                                .GreaterThanOrEquals(dt)
                                .LessThan(dt.AddDays(1))))
                     .Sort(ss => ss
                     .Descending(f => f.Type))
                     .From(skip).Size(size)); ;
                return result;
            }
            else
            {
                var result =
                     ElasticClient.Search<Date>(s => s
                     .Index(indexName)
                     .AllowNoIndices()
                     .Query(q => q.DateRange(d => d
                                            .Field(fl => fl.DateTime)
                                            .GreaterThanOrEquals(dt)
                                            .LessThan(dt.AddDays(1))))
                     .Sort(ss => ss
                     .Descending(f => f.DateTime))
                     .From(skip).Size(size));
                return result;
            }
                        
        }


        private ElasticClient ElasticClient
        {
            get { return Client as ElasticClient; }
        }
    }
}
