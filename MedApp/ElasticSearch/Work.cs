using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElasticSearch
{
    public class Work
    {
        private readonly ElasticClient _client;
        private NameSearch _nameSearch;


        public Work(ElasticClient client)
        {
            _client = client;
        }


        public NameSearch NameSearch => _nameSearch = _nameSearch ?? new NameSearch(_client);

    }
}
