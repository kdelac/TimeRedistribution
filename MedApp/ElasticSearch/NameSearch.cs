using ElasticSearch.Models;
using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElasticSearch
{
    public class NameSearch : ElasticS<Users>
    {
        public NameSearch(ElasticClient client)
            : base(client)
        { }

        public ISearchResponse<Users> OnGet(string keyWord)
        {
            var result =
                 ElasticClient.Search<Users>(s =>
                s.Query(q => q
                .Match(m => m
                .Field(f => f.Name)
                .Query(keyWord))).Size(10000));

            return result;
        }

        private ElasticClient ElasticClient
        {
            get { return Client as ElasticClient; }
        }
    }
}
