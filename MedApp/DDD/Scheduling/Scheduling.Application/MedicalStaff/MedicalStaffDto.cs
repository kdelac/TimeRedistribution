using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduling.Application.MedicalStaff
{
    public class MedicalStaffDto
    {
        public Guid Id { get; set; }
        public Guid ClinicId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string RoleCode { get; set; }
        public Guid CalendarId { get; set; }
    }
}
