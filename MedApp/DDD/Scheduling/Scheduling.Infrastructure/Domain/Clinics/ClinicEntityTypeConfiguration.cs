using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Scheduling.Domain.Clinics;
using Scheduling.Domain.MedicalStaff;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduling.Infrastructure.Domain.Clinics
{
    public class ClinicEntityTypeConfiguration : IEntityTypeConfiguration<Clinic>
    {
        public void Configure(EntityTypeBuilder<Clinic> builder)
        {
            builder.ToTable("Clinics", "calendars");            
            builder.HasKey(x => x.Id);
            builder.Property<string>("_name").HasColumnName("Name");
            builder.Property<string>("_location").HasColumnName("Location");
        }
    }
}
