using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MedAppCore.Models
{
    public class Patient
    {
        public Patient()
        {
            Appointments = new Collection<Appointment>();
        }
        
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }
}
