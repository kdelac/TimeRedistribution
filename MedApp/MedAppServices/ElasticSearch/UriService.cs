using MedAppCore.Models.ElasticSearch;
using MedAppCore.Services.ElasticSearch;
using System;
using System.Collections.Generic;
using System.Text;

namespace MedAppServices.ElasticSearch
{
    public class UriService : IUriService
    {
        private readonly string url = "https://localhost:44308/";
        public List<string> CreateUris(List<UriCreator> uriCreators)
        {
            List<string> urls = new List<string>();
            uriCreators.ForEach(_ => {
                string urlFull = $"{url}{_.Path}{_.Id}";
                urls.Add(urlFull);
            });

            return urls;
        }
    }
}
