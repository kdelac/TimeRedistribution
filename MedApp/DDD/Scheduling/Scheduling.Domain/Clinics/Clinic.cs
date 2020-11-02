using DDD.BuildingBlocks.Domain;
using Scheduling.Domain.Calendars;
using Scheduling.Domain.MedicalStaff;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduling.Domain.Clinics
{
    public class Clinic : Entity, IAggregateRoot
    {
        public ClinicId Id { get; private set; }
        private string _name;
        private string _location;

        public Clinic()
        {
        }

        public MedicalStuff AddNewDoctor(
            string firstname,
            string lastname,
            DateTime dateOfBirth,
            CalendarId calendarId)
        {
            return MedicalStuff.CreateNewDoctor(Id, 
                firstname, 
                lastname, 
                dateOfBirth,
                calendarId);
        }

        public MedicalStuff AddNewNurse(
            string firstname,
            string lastname,
            DateTime dateOfBirth)
        {
            return MedicalStuff.CreateNewNurse(Id, 
                firstname, 
                lastname, 
                dateOfBirth);
        }

        public Clinic(string name, string location)
        {
            Id = new ClinicId(Guid.NewGuid());
            _name = name;
            _location = location;
        }
    }
}
