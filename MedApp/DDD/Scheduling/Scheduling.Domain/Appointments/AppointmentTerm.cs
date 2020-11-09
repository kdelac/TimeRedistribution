using DDD.BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduling.Domain.Appointments
{
    public class AppointmentTerm : ValueObject
    {
        public DateTime? StartAppoitment{ get; }
        public DateTime? EndAppoitment { get; }

        public AppointmentTerm()
        {

        }

        public static AppointmentTerm CreateNewStartAndEnd(DateTime startAppoitment, DateTime endAppointment)
        {
            return new AppointmentTerm(startAppoitment, endAppointment);
        }

        public AppointmentTerm(DateTime startAppoitment, DateTime endAppointment)
        {
            StartAppoitment = startAppoitment;
            EndAppoitment = endAppointment;
        }
    }
}
