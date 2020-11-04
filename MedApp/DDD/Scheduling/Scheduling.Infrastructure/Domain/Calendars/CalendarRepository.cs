using Dapper;
using DDD.BuildingBlocks.Application;
using DDD.BuildingBlocks.Application.Data;
using Microsoft.EntityFrameworkCore;
using Scheduling.Application.Calendars;
using Scheduling.Application.Clinics.GetAllClinics;
using Scheduling.Domain.Calendars;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Scheduling.Infrastructure.Domain.Calendars
{
    public class CalendarRepository : ICalendarRepository
    {
        private readonly SchedulingContext _schedulingContext;
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        private readonly IConverter<CalendarDto, Calendar> _converter;

        internal CalendarRepository(SchedulingContext schedulingContext,
             ISqlConnectionFactory sqlConnectionFactory,
             IConverter<CalendarDto, Calendar> converter)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
            _schedulingContext = schedulingContext;
            _converter = converter;
        }

        public async Task AddAsync(Calendar calendar)
        {
            await _schedulingContext.Calendars.AddAsync(calendar);
        }

        public async Task<Calendar> GetByIdAsync(CalendarId id)
        {
            return await _schedulingContext.Calendars.FindAsync(id);
        }

        public async Task<Calendar> GetById(CalendarId id)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = "SELECT * FROM[TestDb].[calendars].[Calendars] WHERE calendars.Id = @Id";
            var param = new { Id = id.Value };
            var calendar = await connection.QuerySingleAsync<CalendarDto>(sql, param);            
            return _converter.Convert(calendar);
        }

        public async Task Save()
        {
            await _schedulingContext.SaveChangesAsync();
        }

        public async Task Add(Calendar calendar)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            var procedure = "[InserCalendarIfDoesntExist]";
            await connection.QueryAsync(procedure, calendar.CreateNew(), commandType: CommandType.StoredProcedure);

            if (calendar.UsersCount() > 0)
            {
                calendar.GetCalendarUsers().ForEach(async _ => {
                    var procedure = "[InsertOrUpdateCalendarUser]";
                    await connection.QueryAsync(procedure, _.CreateNew(), commandType: CommandType.StoredProcedure);
                });
            }
        }
    }
}
