using DDD.BuildingBlocks.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Scheduling.Domain.Appointments;
using Scheduling.Domain.Calendars;
using Scheduling.Domain.Clinics;
using Scheduling.Domain.MedicalStaff;
using Scheduling.Infrastructure.Domain.Appointments;
using Scheduling.Infrastructure.Domain.Calendars;
using Scheduling.Infrastructure.Domain.Clinics;
using Scheduling.Infrastructure.Domain.MedicalStuffs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduling.Infrastructure
{
    public class SchedulingContext : DbContext
    {
        public DbSet<Calendar> Calendars { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Clinic> Clinics { get; set; }
        public DbSet<MedicalStuff> MedicalStuffs { get; set; }

        public SchedulingContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ReplaceService<IValueConverterSelector, StronglyTypedIdValueConverterSelector>();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CalendarEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AppoitmentEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ClinicEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new MedicalStuffEntityTypeConfiguration());
        }
    }
}
