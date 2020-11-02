using DDD.BuildingBlocks.Domain;
using Scheduling.Domain.Appointments;
using Scheduling.Domain.CalendarUsers;
using Scheduling.Domain.MedicalStaff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scheduling.Domain.Calendars
{
    public class Calendar : Entity, IAggregateRoot
    {
        public CalendarId Id { get; private set; }
        private string _title;
        private List<CalendarUser> _calendarUsers;

        private Calendar()
        {
            _calendarUsers = new List<CalendarUser>();
        }

        public Calendar(
            string title,
            List<MedicalStuffId> nursesIds)
        {
            Id = new CalendarId(Guid.NewGuid());
            _title = title;

            if (nursesIds.Any())
            {
                _calendarUsers = new List<CalendarUser>();
                nursesIds.ForEach(_ => {
                    _calendarUsers.Add(CalendarUser.CreateNew(Id,
                    _, MedicalStuffRole.Nurse));
                });                
            }            
        }

        public void AddAccesToCalendar(
            MedicalStuffId medicalStuffId)
        {
            _calendarUsers.Add(CalendarUser.CreateNew(
                Id,
                medicalStuffId,
                MedicalStuffRole.Nurse                
                ));
        }

        public Appointment AddAppoitment(
            AppointmentTerm appointmentTerm,
            string patientId
            )
        {
            return Appointment.CreateNewAppoitment(Id,
                appointmentTerm,
                patientId
                );
        }
    }
}
