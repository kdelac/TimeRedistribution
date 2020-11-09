using Scheduling.Application.Configurations.Queries;
using System;
using System.Collections.Generic;

namespace Scheduling.Application.Calendars.GetAllAppoitmentsForDoctor
{
    public class GetAllAppoitmentsForDoctorQuery : QueryBase<List<DoctorAppoitmentsDto>>
    {
        public Guid DoctorId { get; set; }
        public Guid NurseId { get; set; }

        public GetAllAppoitmentsForDoctorQuery(Guid doctorId, Guid nurseId)
        {
            DoctorId = doctorId;
            NurseId = nurseId;
        }
    }
}
