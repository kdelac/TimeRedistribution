using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduling.API.Modules.Scheduling.MedicalStaff
{
    public class CreateDoctorRequest
    {
        public Guid ClinicId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public List<Guid> NursesIds { get; set; }
    }
}
