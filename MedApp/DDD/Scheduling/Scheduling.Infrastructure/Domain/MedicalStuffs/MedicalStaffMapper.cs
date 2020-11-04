using DDD.BuildingBlocks.Application;
using Scheduling.Application.MedicalStaff;
using Scheduling.Domain.Calendars;
using Scheduling.Domain.Clinics;
using Scheduling.Domain.MedicalStaff;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduling.Infrastructure.Domain.MedicalStuffs
{
    public class MedicalStaffMapper : IConverter<MedicalStaffDto, MedicalStuff>
    {
        public MedicalStuff Convert(MedicalStaffDto source_object)
        {
            var medId = new MedicalStuffId(source_object.Id);
            var calendarId = new CalendarId(source_object.CalendarId);
            var clinicId = new ClinicId(source_object.ClinicId);
            if (source_object.RoleCode == MedicalStuffRole.Doctor.ToString())
            {
                return new MedicalStuff(medId, clinicId, source_object.Firstname, source_object.Lastname, source_object.DateOfBirth, calendarId, MedicalStuffRole.Doctor);
            }
            else
            {
                return new MedicalStuff(medId, clinicId, source_object.Firstname, source_object.Lastname, source_object.DateOfBirth, calendarId, MedicalStuffRole.Nurse);
            }
            
        }

        public MedicalStaffDto Convert(MedicalStuff source_object)
        {
            throw new NotImplementedException();
        }

        public List<MedicalStuff> ConvertList(List<MedicalStaffDto> source_object)
        {
            throw new NotImplementedException();
        }

        public List<MedicalStaffDto> ConvertList(List<MedicalStuff> source_object)
        {
            throw new NotImplementedException();
        }
    }
}