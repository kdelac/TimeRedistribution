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
        List<string> GetUris(string keyWord, int? skip, int? size, Type type);
        Task AddRangeToIndexAsync();
    }
}
