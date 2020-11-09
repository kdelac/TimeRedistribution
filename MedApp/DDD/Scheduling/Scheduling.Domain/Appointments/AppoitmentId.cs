using DDD.BuildingBlocks.Domain;
using System;

namespace Scheduling.Domain.Appointments
{
    public class AppoitmentId : TypedIdValueBase
    {
        public AppoitmentId(Guid value)
            : base(value)
        {
        }
    }
}
