using Scheduling.Application.Contracts;
using System;

namespace Scheduling.Application.Clinics.CreateClinic
{
    public class CreateClinicCommand : CommandBase<Guid>
    {
        public CreateClinicCommand(string name, string location)
        {
            Name = name;
            Location = location;
        }

        public string Name { get; }
        public string Location { get; }
    }
}
