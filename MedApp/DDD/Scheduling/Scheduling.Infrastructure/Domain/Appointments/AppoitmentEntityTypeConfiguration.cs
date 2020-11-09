using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Scheduling.Domain.Appointments;
using Scheduling.Domain.Calendars;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduling.Infrastructure.Domain.Appointments
{
    public class AppoitmentEntityTypeConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.ToTable("Appoitments", "calendars");
            builder.HasKey(x => x.Id);
            builder.Property<string>("_patientId").HasColumnName("PatientId");
            builder.Property<CalendarId>("_calendarId").HasColumnName("CalendarId");

            builder.OwnsOne<AppointmentTerm>("_term", b => {
                b.Property(p => p.StartAppoitment).HasColumnName("StartAppoitmentDate");
                b.Property(p => p.EndAppoitment).HasColumnName("EndAppoitmentAppoitmentDate");
            });
        }
    }
}
