using DDD.BuildingBlocks.Domain;
using Scheduling.Domain.Calendars;
using Scheduling.Domain.Clinics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduling.Domain.MedicalStaff
{
    public class MedicalStuff : Entity, IAggregateRoot
    {
        public MedicalStuffId Id { get; private set; }
        private string _firstname;
        private string _lastname;
        private DateTime _dateOfBirth;
        private ClinicId _clinicId;
        private CalendarId _calendarId;
        private MedicalStuffRole _medicalStuffRole;

        public MedicalStuff()
        {

        }

        public MedicalStuff(MedicalStuffId medicalStuffId,
            ClinicId clinicId,
            string firstname, 
            string lastname, 
            DateTime dateOfBirth,             
            CalendarId? calendarId, 
            MedicalStuffRole medicalStuffRole)
        {
            Id = medicalStuffId;
            _firstname = firstname;
            _lastname = lastname;
            _dateOfBirth = dateOfBirth;
            _clinicId = clinicId;
            _calendarId = calendarId;
            _medicalStuffRole = medicalStuffRole;
        }

        public MedicalStuff(MedicalStuffId medicalStuffId,
            ClinicId clinicId,
            string firstname,
            string lastname,
            DateTime dateOfBirth,
            MedicalStuffRole medicalStuffRole)
        {
            Id = medicalStuffId;
            _firstname = firstname;
            _lastname = lastname;
            _dateOfBirth = dateOfBirth;
            _clinicId = clinicId;
            _medicalStuffRole = medicalStuffRole;
        }

        internal static MedicalStuff CreateNewDoctor(
            ClinicId clinicId,
            string firstname,
            string lastname,
            DateTime dateOfBirth,
            CalendarId calendarId)
        {            
            return new MedicalStuff(
                clinicId,
                firstname,
                lastname,
                dateOfBirth,
                MedicalStuffRole.Doctor,
                calendarId);
        }

        internal static MedicalStuff CreateNewNurse(
            ClinicId clinicId,
            string firstname,
            string lastname,
            DateTime dateOfBirth)
        {
            return new MedicalStuff(
                clinicId,
                firstname,
                lastname,
                dateOfBirth,
                MedicalStuffRole.Nurse,
                null);
        }

        public MedicalStuff(
            ClinicId clinicId,
            string firstname,
            string lastname,
            DateTime dateOfBirth,
            MedicalStuffRole medicalStuffRole,
            CalendarId calendarId)
        {
            Id = new MedicalStuffId(Guid.NewGuid());
            _firstname = firstname;
            _lastname = lastname;
            _dateOfBirth = dateOfBirth;
            _medicalStuffRole = medicalStuffRole;
            _clinicId = clinicId;
            _calendarId = calendarId;

        }

        public CalendarId GetCalendarId() => _calendarId;
        public string GetFirstname() => _firstname;
    }
}
