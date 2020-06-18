using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MedAppCore.Services.ElasticSearch
{
    public interface IDateSearchService
    {
        void CreateIndex();
        Task DeleteIndex();
        Task DeleteFromIndex();
        List<string> GetUrisIndex(DateTime keyWord);
        Task AddRangeToIndexAsync();
    }
}
