using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimeRedistribution.Model
{
    public class Patient
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }

    public class PatientForInsert
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
    }

    public class PatientForUpdate
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
    }
}
