using ElasticSearch.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MedAppCore.Services
{
    public interface INameSearchService
    {
        void CreateIndex();
        Task DeleteIndex();
        Task DeleteFromIndex();
        List<Users> GetFromIndex(string keyWord);
        Task AddRangeToIndexAsync();
    }
}
