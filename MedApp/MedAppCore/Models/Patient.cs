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
            DoctorPatients = new Collection<DoctorPatient>();
            Bills = new Collection<Bill>();
        }
        
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<DoctorPatient> DoctorPatients { get; set; }
        public ICollection<Bill> Bills { get; set; }
    }
}
