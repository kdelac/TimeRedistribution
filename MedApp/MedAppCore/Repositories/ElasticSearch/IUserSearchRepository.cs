﻿using MedAppCore.Models.ElasticSearch;
using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace MedAppCore.Repositories.ElasticSearch
{
    public interface IUserSearchRepository : IElasticSearchRepository<User>
    {
        ISearchResponse<User> OnGet(string keyWord, string indexName, int? skip, int? size, string type);
        IEnumerable<User> AutocompleteSearch(string keyWord, string indexName);
    }
}
