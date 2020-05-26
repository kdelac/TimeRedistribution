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
        public string StartOfWork { get; set; }
        public string EndOfWorl { get; set; }
        public string SrartBreak { get; set; }
        public string EndBreak { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }

    public class DoctorFInsert
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Specialization { get; set; }
        public string OIB { get; set; }
        public string StartOfWork { get; set; }
        public string EndOfWorl { get; set; }
        public string SrartBreak { get; set; }
        public string EndBreak { get; set; }
    }

    public class DoctorForUpdate
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Specialization { get; set; }
        public string OIB { get; set; }
        public string StartOfWork { get; set; }
        public string EndOfWorl { get; set; }
        public string SrartBreak { get; set; }
        public string EndBreak { get; set; }
    }
}
