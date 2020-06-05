using Microsoft.EntityFrameworkCore;
using MedAppCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MedAppData
{
    public class MedAppDbContext : DbContext
    {
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        public MedAppDbContext(DbContextOptions<MedAppDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>()
                .HasKey(bc => new { bc.DoctorId, bc.PatientId });
            modelBuilder.Entity<Appointment>()
                .HasOne(bc => bc.Doctor)
                .WithMany(b => b.Appointments)
                .HasForeignKey(bc => bc.DoctorId);
            modelBuilder.Entity<Appointment>()
                .HasOne(bc => bc.Patient)
                .WithMany(c => c.Appointments)
                .HasForeignKey(bc => bc.PatientId);
            modelBuilder.Entity<Appointment>()
                .HasIndex(p => new { p.DateTime, p.Status });
        }
    }
}
