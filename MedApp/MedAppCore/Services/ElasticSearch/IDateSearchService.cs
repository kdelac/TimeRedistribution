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
        Task DeleteIndex(string name);
        Task DeleteFromIndex();
        List<string> GetUris(DateTime keyWord, int? skip, int? size, Type type);
        Task AddRangeToIndexAsync();
    }
}
