using Scheduling.Application.Calendars;
using Scheduling.Application.Clinics.GetAllClinics;
using Scheduling.Domain.Calendars;
using Scheduling.Domain.Clinics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduling.Infrastructure.Domain.Clinics
{
    public static class ClinicMapper
    {
        public static Clinic Mapp(this ClinicsDto clinicsDto)
        {
            var clinicId = new ClinicId(clinicsDto.Id);
            return new Clinic(clinicId, clinicsDto.Name, clinicsDto.Location);
        }
    }
}
