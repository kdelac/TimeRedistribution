﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.BuildingBlocks.Domain
{
    public class DomainEventBase : IDomainEvent
    {
        public Guid Id { get; }

        public DateTime OccurredOn { get; }

        public DomainEventBase()
        {
            this.Id = Guid.NewGuid();
            this.OccurredOn = DateTime.UtcNow;
        }
    }
}
