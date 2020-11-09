using Scheduling.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduling.Application.MedicalStaff.CreateMedicalStaff.CreateNurse
{
    public class CreateNurseCommand : CommandBase<Guid>
    {
        public CreateNurseCommand(Guid clinicId, string firstname, string lastname, DateTime dateOfBirth)
        {
            ClinicId = clinicId;
            Firstname = firstname;
            Lastname = lastname;
            DateOfBirth = dateOfBirth;
        }

        public Guid ClinicId { get; }
        public string Firstname { get; }
        public string Lastname { get; }
        public DateTime DateOfBirth { get; }
    }
}
