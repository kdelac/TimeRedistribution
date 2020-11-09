using DDD.BuildingBlocks.Domain;
using Scheduling.Domain.Appointments.Events;
using Scheduling.Domain.Calendars;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduling.Domain.Appointments
{
    public class Appointment : Entity, IAggregateRoot
    {
        public AppoitmentId Id { get; set; }
        private CalendarId _calendarId;
        private AppointmentTerm _term;
        private string _patientId;

        public Appointment()
        {

        }

        internal static Appointment CreateNewAppoitment(
            CalendarId calendarId,
            AppointmentTerm appointmentTerm,
            string patientId)
        {
            return new Appointment(
                calendarId,
                appointmentTerm,
                patientId
                );
        }

        public Appointment(CalendarId calendarId,
            AppointmentTerm appointmentTerm,
            string patientId)
        {
            Id = new AppoitmentId(Guid.NewGuid());
            _calendarId = calendarId;
            _term = appointmentTerm;
            _patientId = patientId;
            AddDomainEvent(new AppointmentCreatedDomainEvent(Id));
        }
    }
}
