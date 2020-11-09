using DDD.BuildingBlocks.Domain;
using Scheduling.Domain.Calendars;
using Scheduling.Domain.Clinics.Events;
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

        public Clinic(string name, 
            string location)
        {
            Id = new ClinicId(Guid.NewGuid());
            _name = name;
            _location = location;
            AddDomainEvent(new ClinicCreatedDomainEvent(Id));
        }

        public Clinic(ClinicId clinicId,
            string name, 
            string location)
        {
            Id = clinicId;
            _name = name;
            _location = location;
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

        public object CreateNew()
        {
            return new { Id = Id.Value, Name = _name, Location = _location };
        }

        public object Update(Guid clinicId)
        {
            return new { Id = clinicId, Name = _name, Location = _location };
        }
    }
}
