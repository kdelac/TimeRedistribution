using MedAppCore.Models.ElasticSearch;
using MedAppCore.Repositories.ElasticSearch;
using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace MedAppData.Repositories.ElasticSearch
{
    public class UserSearchRepository : ElasticSearchRepository<User>, IUserSearchRepository
    {
        public UserSearchRepository(ElasticClient client)
            : base(client)
        { }

        public ISearchResponse<User> OnGet(string keyWord, string indexName)
        {
            var result =
                 ElasticClient.Search<User>(s => s
                 .Index(indexName)
                .Query(q => q
                .MultiMatch(m => m
                    .Fields(f => f
                        .Field(ff => ff.Name)
                        .Field(a => a.Surname)
                    )
                    .Operator(Operator.Or)
                    .Query(keyWord))).Size(10000));

            return result;
        }

        private ElasticClient ElasticClient
        {
            get { return Client as ElasticClient; }
        }
    }
}
