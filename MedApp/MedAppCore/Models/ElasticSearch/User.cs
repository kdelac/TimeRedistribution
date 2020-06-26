using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace MedAppCore.Models.ElasticSearch
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Path { get; set; }
        public Type Type { get; set; }
        [Completion]
        public CompletionField Suggest { get; set; }
    }
}
