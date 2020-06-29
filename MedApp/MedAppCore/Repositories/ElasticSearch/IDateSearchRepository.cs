using MedAppCore.Models.ElasticSearch;
using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace MedAppCore.Repositories.ElasticSearch
{
    public interface IDateSearchRepository : IElasticSearchRepository<Date>
    {
        ISearchResponse<Date> OnGet(DateTime keyWord, string indexName, int? skip, int? size, string type);
    }
}
