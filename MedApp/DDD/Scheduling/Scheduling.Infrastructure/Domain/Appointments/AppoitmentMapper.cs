using Scheduling.Application.Appoitments;
using Scheduling.Domain.Appointments;
using Scheduling.Domain.Calendars;

namespace Scheduling.Infrastructure.Domain.Appointments
{
    public static class AppoitmentMapper
    {
        public static Appointment Mapp(this AppointmentDto appointmentDto)
        {
            var calendarId = new CalendarId(appointmentDto.CalendarId);
            return new Appointment(calendarId,
                AppointmentTerm.CreateNewStartAndEnd(appointmentDto.StartAppoitmentDate, 
                appointmentDto.EndAppoitmentAppoitmentDate), 
                appointmentDto.PatientId); 
        }
    }
}
