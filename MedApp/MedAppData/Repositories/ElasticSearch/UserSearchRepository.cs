using Elasticsearch.Net;
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
            if (!size.HasValue && skip.HasValue)
            {
                size = 10000 - skip;
            }

            if (!size.HasValue && !skip.HasValue)
            {
                size = 10000;
            }

            if (type != null)
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
            else
            {
                var result =
                 ElasticClient.Search<User>(s => s
                 .Index(indexName)
                .Query(q => q.MultiMatch(m => m
                    .Fields(f => f
                    .Field(ff => ff.Name)
                    .Field(a => a.Surname))
                    .Operator(Operator.Or)
                    .Query(keyWord))).From(skip).Size(size));

                return result;
            }
        }

        public ISearchResponse<User> AutocompleteSearch(string keyWord, string indexName)
        {
            ISearchResponse<User> searchResponse = ElasticClient.Search<User>(s => s
                                     .Index(indexName)
                                     .Suggest(su => su
                                          .Completion("suggest", c => c
                                               .Field(f => f.Suggest)
                                               .Prefix(keyWord)
                                               .Fuzzy(f => f
                                                   .Fuzziness(Fuzziness.Auto)
                                               )
                                               .Size(10000))
                                             ));
            return searchResponse;
        }



        public void CreateSearchIndex(string indexName)
        {
            var createIndexDescriptor = ElasticClient.Indices.Create(indexName,
            ind => ind.Map<User>(m => m
                                .AutoMap(typeof(User))));
        }

        private ElasticClient ElasticClient
        {
            get { return Client as ElasticClient; }
        }
    }
}
