using DDD.BuildingBlocks.Domain;
using System;

namespace Scheduling.Domain.Calendars
{
    public class CalendarId : TypedIdValueBase
    {
        public CalendarId(Guid value)
            : base(value)
        {            
        }
    }
}
