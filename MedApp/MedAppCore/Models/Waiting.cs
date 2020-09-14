using System;
using System.Collections.Generic;
using System.Text;

namespace MedAppCore.Models
{
    public class Waiting
    {
        public Guid Id { get; set; }
        public Guid OrdinationId { get; set; }
        public Ordination Ordination { get; set; }
        public int NumberIn { get; set; }
        public int NumberOut { get; set; }
    }
}
