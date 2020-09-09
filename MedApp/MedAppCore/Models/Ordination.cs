using System;
using System.Collections.Generic;
using System.Text;

namespace MedAppCore.Models
{
    public class Ordination
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int MaxIn { get; set; }
        public int MaxOut { get; set; }
        public ICollection<Application> Applications { get; set; }
    }
}
