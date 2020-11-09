using Dapper;
using DDD.BuildingBlocks.Application.Data;
using Scheduling.Application.Configurations.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Data;
using Scheduling.Domain.MedicalStaff;

namespace Scheduling.Application.Calendars.GetAllAppoitmentsForDoctor
{
    internal class GetAllAppoitmentsForDoctorQueryHandler : IQueryHandler<GetAllAppoitmentsForDoctorQuery, List<DoctorAppoitmentsDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        private readonly IMedicalRepository _medicalRepository;

        internal GetAllAppoitmentsForDoctorQueryHandler(ISqlConnectionFactory sqlConnectionFactory,
            IMedicalRepository medicalRepository)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
            _medicalRepository = medicalRepository;
        }

        public async Task<List<DoctorAppoitmentsDto>> Handle(GetAllAppoitmentsForDoctorQuery request, CancellationToken cancellationToken)
        {
            var doctor = await _medicalRepository.GetByIdAsync(new MedicalStuffId(request.DoctorId));

            var connection = _sqlConnectionFactory.GetOpenConnection();            

            var procedure = "[GetAllAppoitmentsForDoctor]";
            var values = new { CalendarId = doctor.GetCalendarId().Value, request.NurseId};

            var clinics = await connection.QueryAsync<DoctorAppoitmentsDto>(procedure, values, commandType: CommandType.StoredProcedure);

            return clinics.AsList();
        }


        //Procedura
        //USE[TestDb]
        //GO
        //SET ANSI_NULLS ON
        //GO
        //SET QUOTED_IDENTIFIER ON
        //GO
        //ALTER PROCEDURE[dbo].[GetAllAppoitmentsForDoctor] @CalendarId uniqueidentifier, @NurseId uniqueidentifier
        //AS
        //  SELECT Appointments.StartAppoitmentDate, Appointments.EndAppoitmentAppoitmentDate, Appointments.PatientId FROM calendars.Calendars cal
        //  JOIN calendars.Appoitments Appointments ON cal.Id = Appointments.CalendarId
        //  JOIN calendars.CalendarUsers cu ON cu.CalendarId = Appointments.CalendarId
        //  WHERE cal.Id = @CalendarId
        //  AND cu.MedicalStuffId = @NurseId
    }
}
