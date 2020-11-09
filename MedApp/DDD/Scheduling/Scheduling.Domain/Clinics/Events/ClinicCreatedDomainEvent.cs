using DDD.BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduling.Domain.Clinics.Events
{
    public class ClinicCreatedDomainEvent : DomainEventBase
    {
        public ClinicCreatedDomainEvent(ClinicId clinicId)
        {
            ClinicId = clinicId;
        }

        public ClinicId ClinicId { get; }

    }
}
