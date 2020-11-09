using DDD.BuildingBlocks.Application;
using Scheduling.Application.MedicalStaff;
using Scheduling.Domain.Calendars;
using Scheduling.Domain.Clinics;
using Scheduling.Domain.MedicalStaff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Scheduling.Infrastructure.Domain.MedicalStuffs
{
    public static class MedicalStaffMapper
    {
        public static MedicalStuff Mapp(this MedicalStaffDto medicalStaffDto)
        {
            var medId = new MedicalStuffId(medicalStaffDto.Id);
            CalendarId calendarId = null;
            if (medicalStaffDto.CalendarId != Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                calendarId = new CalendarId(medicalStaffDto.CalendarId);
            }
            
            var clinicId = new ClinicId(medicalStaffDto.ClinicId);
            if (medicalStaffDto.RoleCode == MedicalStuffRole.Doctor.ToString())
            {
                return new MedicalStuff(medId, clinicId, medicalStaffDto.Firstname, medicalStaffDto.Lastname, medicalStaffDto.DateOfBirth, calendarId, MedicalStuffRole.Doctor);
            }
            else
            {
                return new MedicalStuff(medId, clinicId, medicalStaffDto.Firstname, medicalStaffDto.Lastname, medicalStaffDto.DateOfBirth, MedicalStuffRole.Nurse);
            }
        }

        public static List<MedicalStuff> MappList(this List<MedicalStaffDto> medicalStaffDto)
        {
            List<MedicalStuff> medicalStuff = new List<MedicalStuff>();

            medicalStuff
                .AddRange(medicalStaffDto
                .Where(_ =>
                _.CalendarId != Guid.Parse("00000000-0000-0000-0000-000000000000"))
                .Select(_ => 
                new MedicalStuff(new MedicalStuffId(_.Id), 
                new ClinicId(_.ClinicId),  
                _.Firstname, 
                _.Lastname, 
                _.DateOfBirth, 
                new CalendarId(_.CalendarId), 
                MedicalStuffRole.Doctor)).ToList());

            medicalStuff
                .AddRange(medicalStaffDto
                .Where(_ =>
                _.CalendarId == Guid.Parse("00000000-0000-0000-0000-000000000000"))
                .Select(_ =>
                new MedicalStuff(new MedicalStuffId(_.Id),
                new ClinicId(_.ClinicId),
                _.Firstname,
                _.Lastname,
                _.DateOfBirth,
                MedicalStuffRole.Doctor)).ToList());

            return medicalStuff;

        }
    }
}