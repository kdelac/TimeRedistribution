using System;
using System.Collections.Generic;

namespace AppoitmentRedistribution.Models
{
    public partial class Doctors
    {
        public Doctors()
        {
            Appointments = new HashSet<Appointments>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Specialization { get; set; }
        public string Oib { get; set; }
        public string EndBreak { get; set; }
        public string EndOfWorl { get; set; }
        public string SrartBreak { get; set; }
        public string StartOfWork { get; set; }

        public virtual ICollection<Appointments> Appointments { get; set; }
    }
}
