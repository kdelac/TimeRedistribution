using DDD.BuildingBlocks.Domain;
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
    }
}
