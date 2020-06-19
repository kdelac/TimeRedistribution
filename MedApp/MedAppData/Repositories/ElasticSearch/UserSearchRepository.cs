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

        public ISearchResponse<User> OnGet(string keyWord, string indexName, int? skip, int? size, Type type)
        {
            var result =
                 ElasticClient.Search<User>(s => s
                 .Index(indexName)
                .Query(q => q
                .Match(m => m.Field(f => f.Type)
                            .Query(type.ToString()))
                && q.MultiMatch(m => m
                    .Fields(f => f
                    .Field(ff => ff.Name)
                    .Field(a => a.Surname))
                    .Operator(Operator.Or)
                    .Query(keyWord))).From(skip).Size(size));

            return result;
        }

        private ElasticClient ElasticClient
        {
            get { return Client as ElasticClient; }
        }
    }
}
