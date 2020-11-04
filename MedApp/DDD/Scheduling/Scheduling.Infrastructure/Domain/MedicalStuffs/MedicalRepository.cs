using Dapper;
using DDD.BuildingBlocks.Application;
using DDD.BuildingBlocks.Application.Data;
using Scheduling.Application.MedicalStaff;
using Scheduling.Domain.MedicalStaff;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scheduling.Infrastructure.Domain.MedicalStuffs
{
    public class MedicalRepository : IMedicalRepository
    {
        private readonly SchedulingContext _schedulingContext;
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        private readonly IConverter<MedicalStaffDto, MedicalStuff> _converter;

        internal MedicalRepository(SchedulingContext schedulingContext,
            ISqlConnectionFactory sqlConnectionFactory,
            IConverter<MedicalStaffDto, MedicalStuff> converter)
        {
            _schedulingContext = schedulingContext;
            _sqlConnectionFactory = sqlConnectionFactory;
            _converter = converter;
        }
        public async Task AddAsync(MedicalStuff medical)
        {
            await _schedulingContext.MedicalStuffs.AddAsync(medical);
        }

        public async Task<MedicalStuff> GetByIdAsync(MedicalStuffId id)
        {
            return await _schedulingContext.MedicalStuffs.FindAsync(id);
        }

        public async Task<MedicalStuff> GetById(MedicalStuffId id)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = "SELECT *  FROM [TestDb].[calendars].[MedicalStuffs] WHERE MedicalStuffs.Id = @Id";
            var param = new { Id = id.Value };
            var medicalStaff = await connection.QuerySingleAsync<MedicalStaffDto>(sql, param);
            return _converter.Convert(medicalStaff);
        }

        public async Task Save()
        {
            await _schedulingContext.SaveChangesAsync();
        }

        public async Task Add(MedicalStuff calendar)
        {
            throw new NotImplementedException();
        }
    }
}
