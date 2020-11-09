using DDD.BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduling.Domain.MedicalStaff.Events
{
    public class DoctorCreatedDomainEvent : DomainEventBase
    {
        public DoctorCreatedDomainEvent(MedicalStuffId medicalStuffId)
        {
            MedicalStuffId = medicalStuffId;
        }

        public MedicalStuffId MedicalStuffId { get; }

    }
}
