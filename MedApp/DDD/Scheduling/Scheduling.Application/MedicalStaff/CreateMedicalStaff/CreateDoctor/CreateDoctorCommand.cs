using Scheduling.Application.Contracts;
using System;
using System.Collections.Generic;

namespace Scheduling.Application.MedicalStaff.CreateMedicalStaff
{
    public class CreateDoctorCommand : CommandBase<Guid>
    {
        public CreateDoctorCommand(Guid clinicId, string firstname, string lastname, DateTime dateOfBirth, List<Guid> nursesIds)
        {
            ClinicId = clinicId;
            Firstname = firstname;
            Lastname = lastname;
            DateOfBirth = dateOfBirth;
            NursesIds = nursesIds;
        }

        public Guid ClinicId { get; }
        public string Firstname { get; }
        public string Lastname { get; }
        public DateTime DateOfBirth { get; }
        public List<Guid> NursesIds { get; set; }
    }
}
