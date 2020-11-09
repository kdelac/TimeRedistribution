using DDD.BuildingBlocks.Application;
using Scheduling.Application.Calendars;
using Scheduling.Domain.Calendars;
using System;
using System.Collections.Generic;

namespace Scheduling.Infrastructure.Domain.Calendars
{
    public static class CalendarMapper
    {
        public static Calendar Mapp(this CalendarDto calendarDto)
        {
            CalendarId calendarId = new CalendarId(calendarDto.Id);
            return new Calendar(calendarId, calendarDto.Title);
        }
    }
}
