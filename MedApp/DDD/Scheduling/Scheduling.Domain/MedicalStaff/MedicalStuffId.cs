using DDD.BuildingBlocks.Domain;
using System;

namespace Scheduling.Domain.MedicalStaff
{
    public class MedicalStuffId : TypedIdValueBase
    {
        public MedicalStuffId(Guid value)
            : base(value)
        {
        }
    }
}
