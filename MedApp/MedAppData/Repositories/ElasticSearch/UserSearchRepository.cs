using Elasticsearch.Net;
using MedAppCore.Models.ElasticSearch;
using MedAppCore.Repositories.ElasticSearch;
using Nest;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedAppData.Repositories.ElasticSearch
{
    public class UserSearchRepository : ElasticSearchRepository<User>, IUserSearchRepository
    {
        public UserSearchRepository(ElasticClient client)
            : base(client)
        { }

        public ISearchResponse<User> OnGet(string keyWord, string indexName, int? skip, int? size, string type)
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

        public IEnumerable<User> AutocompleteSearch(string keyWord, string indexName)
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
           var suggestions =
                from suggest in searchResponse.Suggest["suggest"]
                from option in suggest.Options
                select new User
                {
                    Id = option.Source.Id,
                    Path = option.Source.Path
                };


            return suggestions;
        }

        private ElasticClient ElasticClient
        {
            get { return Client as ElasticClient; }
        }
    }
}
