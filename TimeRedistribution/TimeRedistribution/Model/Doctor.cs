using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeRedistribution.Model
{
    public class Doctor
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Specialization { get; set; }
        public string OIB { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }
}
