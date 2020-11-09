using Dapper;
using DDD.BuildingBlocks.Application.Data;
using Scheduling.Application.Configurations.Commands;
using Scheduling.Domain.Clinics;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Scheduling.Application.Clinics.CreateClinic
{
    internal class CreateClinicCommandHandler : ICommandHandler<CreateClinicCommand, Guid>
    {
        private IClinicRepository _clinicRepository;
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public CreateClinicCommandHandler(
            IClinicRepository clinicRepository,
            ISqlConnectionFactory sqlConnectionFactory)
        {
            _clinicRepository = clinicRepository;
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<Guid> Handle(CreateClinicCommand request, CancellationToken cancellationToken)
        {
            var clinic = new Clinic(request.Name, request.Location);

            var connection = _sqlConnectionFactory.GetOpenConnection();

            var procedure = "[InsertIntoClinic]";

            await connection.QueryAsync(procedure, clinic.CreateNew() , commandType: CommandType.StoredProcedure);


            //await _clinicRepository.AddAsync(clinic);
            //await _clinicRepository.Save();

            return clinic.Id.Value;
        }


        //USE[TestDb]
        //GO
        //SET ANSI_NULLS ON
        //GO
        //SET QUOTED_IDENTIFIER ON
        //GO
        //ALTER PROCEDURE[dbo].[InsertIntoClinic]
        //        @Id uniqueidentifier,
        //@Name nvarchar(MAX),
        //@Location nvarchar(MAX)
        //AS
        //SET NOCOUNT ON

        //IF(NOT EXISTS (SELECT* FROM calendars.Clinics WHERE Clinics.Id = @Id))
        //	BEGIN
        //        INSERT INTO calendars.Clinics(Id, Name, Location) VALUES(@Id, @Name, @Location)

        //    END
        //ELSE

        //    BEGIN
        //        UPDATE calendars.Clinics SET

        //        Name = @Name,
        //		Location = @Location
        //        WHERE Id = @Id;
        //	END
    }
}
