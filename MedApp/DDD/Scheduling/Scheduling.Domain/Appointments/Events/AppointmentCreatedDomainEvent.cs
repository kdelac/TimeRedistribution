using DDD.BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduling.Domain.Appointments.Events
{
    public class AppointmentCreatedDomainEvent : DomainEventBase
    {
        public AppointmentCreatedDomainEvent(AppoitmentId appoitmentId)
        {
            AppoitmentId = appoitmentId;
        }

        public AppoitmentId AppoitmentId { get; }

    }
}
