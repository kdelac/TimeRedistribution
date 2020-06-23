using MedAppCore.Models.ElasticSearch;
using System;
using System.Collections.Generic;
using System.Text;

namespace MedAppCore.Services.ElasticSearch
{
    public interface IUriService
    {
        public List<string> CreateUris(List<UriCreator> uriCreators);
    }
}
