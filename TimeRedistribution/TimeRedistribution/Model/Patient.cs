using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeRedistribution.Model
{
    public class Patient
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }
}
