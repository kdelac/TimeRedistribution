using System;
using System.Collections.Generic;
using System.Text;

namespace MedAppCore.Models.ElasticSearch
{
    public class Date
    {
        public Guid Id { get; set; }
        public DateTime DateTime { get; set; }
        public string Path { get; set; }
        public Type Type { get; set; }
    }
}
