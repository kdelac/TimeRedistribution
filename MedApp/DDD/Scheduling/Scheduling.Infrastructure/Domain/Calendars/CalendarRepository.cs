using Dapper;
using DDD.BuildingBlocks.Application;
using DDD.BuildingBlocks.Application.Data;
using DDD.BuildingBlocks.Infrastructure.DomainEventsDispatching;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Scheduling.Application.Calendars;
using Scheduling.Application.Clinics.GetAllClinics;
using Scheduling.Domain.Calendars;
using Scheduling.Domain.CalendarUsers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Scheduling.Infrastructure.Domain.Calendars
{
    public class CalendarRepository : ICalendarRepository
    {
        private readonly SchedulingContext _schedulingContext;
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        private readonly IDomainEventsDispatcher _domainEventsDispatcher;

        internal CalendarRepository(SchedulingContext schedulingContext,
             ISqlConnectionFactory sqlConnectionFactory,
             IDomainEventsDispatcher domainEventsDispatcher
             )
        {
            _sqlConnectionFactory = sqlConnectionFactory;
            _schedulingContext = schedulingContext;
            _domainEventsDispatcher = domainEventsDispatcher;
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

            try
            {
                var calendar = await connection.QuerySingleAsync<CalendarDto>(sql, param);
                return calendar.Mapp();
            }
            catch (Exception)
            {
                return null;
            }      
        }

        public async Task Save()
        {
            await _schedulingContext.SaveChangesAsync();
        }

        public async Task Add(Calendar calendar)
        {
            var connection =  _sqlConnectionFactory.GetOpenConnection();
            var exist = await GetById(calendar.Id);       

            if (exist == null)
            {
                var procedure = "[InserCalendarIfDoesntExist]";
                await connection.ExecuteAsync(procedure, calendar.CreateNew(), commandType: CommandType.StoredProcedure);

                await _domainEventsDispatcher.DispatchEventsAsync(calendar.GetDomainEvents());
            } 

            if (calendar.UsersCount() > 0)
            {
                await Iterate(calendar.GetCalendarUsers());
            }
        }

        private async Task AddCalendarUser(CalendarUser calendarUser)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();
            var procedure = "[InsertOrUpdateCalendarUser]";
            Thread.Sleep(100);
            await connection.QueryAsync(procedure, calendarUser.CreateNew(), commandType: CommandType.StoredProcedure);

            await _domainEventsDispatcher.DispatchEventsAsync(calendarUser.GetDomainEvents());
        }

        private async Task Iterate(List<CalendarUser> calendarUsers)
        {
            var calendarUser = calendarUsers.FirstOrDefault();
            await AddCalendarUser(calendarUser);
            calendarUsers.Remove(calendarUser);

            if (calendarUsers.Count > 0)
            {
                await Iterate(calendarUsers);
            }
            
        }
    }
}
