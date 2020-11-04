using DDD.BuildingBlocks.Application;
using Scheduling.Application.Calendars;
using Scheduling.Domain.Calendars;
using System;
using System.Collections.Generic;

namespace Scheduling.Infrastructure.Domain.Calendars
{
    public class CalendarMapper : IConverter<CalendarDto, Calendar>
    {
        public Calendar Convert(CalendarDto source_object)
        {
            CalendarId calendarId = new CalendarId(source_object.Id);
            return new Calendar(calendarId, source_object.Title);
        }

        public CalendarDto Convert(Calendar source_object)
        {
            throw new NotImplementedException();
        }

        public List<Calendar> ConvertList(List<CalendarDto> source_object)
        {
            throw new NotImplementedException();
        }

        public List<CalendarDto> ConvertList(List<Calendar> source_object)
        {
            throw new NotImplementedException();
        }
    }
}
