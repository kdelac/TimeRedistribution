using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Scheduling.Domain.Calendars;
using Scheduling.Domain.Clinics;
using Scheduling.Domain.MedicalStaff;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduling.Infrastructure.Domain.MedicalStuffs
{
    public class MedicalStuffEntityTypeConfiguration : IEntityTypeConfiguration<MedicalStuff>
    {
        public void Configure(EntityTypeBuilder<MedicalStuff> builder)
        {
            builder.ToTable("MedicalStuffs", "calendars");
            builder.HasKey(x => x.Id);
            builder.Property<string>("_firstname").HasColumnName("Firstname");
            builder.Property<string>("_lastname").HasColumnName("Lastname");
            builder.Property<DateTime>("_dateOfBirth").HasColumnName("DateOfBirth");
            builder.Property<ClinicId>("_clinicId").HasColumnName("ClinicId");
            builder.Property<CalendarId>("_calendarId").HasColumnName("CalendarId");
            builder.OwnsOne<MedicalStuffRole>("_medicalStuffRole", y => 
            {
                y.Property(x => x.Value).HasColumnName("RoleCode");
            });
        }
    }
}
