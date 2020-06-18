using MedAppCore.Models.ElasticSearch;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MedAppCore.Services
{
    public interface IUserSearchService
    {
        void CreateIndex();
        Task DeleteIndex();
        Task DeleteFromIndex();
        List<string> GetUrisIndex(string keyWord);
        Task AddRangeToIndexAsync();
    }
}
