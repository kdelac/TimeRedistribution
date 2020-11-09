using DDD.BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduling.Domain.Clinics
{
    public class ClinicId : TypedIdValueBase
    {
        public ClinicId(Guid value)
            : base(value)
        {
        }
    }
}
