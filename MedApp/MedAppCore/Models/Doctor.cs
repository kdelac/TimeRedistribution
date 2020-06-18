using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MedAppCore.Models
{
    public class Doctor
    {
        public Doctor()
        {
            Appointments = new Collection<Appointment>();
            DoctorPatients = new Collection<DoctorPatient>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<DoctorPatient> DoctorPatients { get; set; }
    }
}
