using System;
using System.Collections.Generic;

namespace AppoitmentRedistribution.Models
{
    public partial class Patients
    {
        public Patients()
        {
            Appointments = new HashSet<Appointments>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Appointments> Appointments { get; set; }
    }
}
