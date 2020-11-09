using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using DDD.BuildingBlocks.Application.Data;
using MediatR;
using Scheduling.Application.Configurations.Queries;

namespace Scheduling.Application.Clinics.GetAllClinics
{
    internal class GetaAllClinicsQueryHandler : IQueryHandler<GetaAllClinicsQuery, List<ClinicsDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        internal GetaAllClinicsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<List<ClinicsDto>> Handle(GetaAllClinicsQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = "SELECT [Id], [Location], [Name]  FROM [calendars].[Clinics]";
            var clinics = await connection.QueryAsync<ClinicsDto>(sql);

            return clinics.AsList();
        }
    }    
        
}
