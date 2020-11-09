using DDD.BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduling.Domain.MedicalStaff
{
    public class MedicalStuffRole : ValueObject
    {
        public static MedicalStuffRole Doctor => new MedicalStuffRole("Doctor");

        public static MedicalStuffRole Nurse => new MedicalStuffRole("Nurse");

        public string Value { get; }

        private MedicalStuffRole(string value)
        {
            this.Value = value;
        }
    }
}
