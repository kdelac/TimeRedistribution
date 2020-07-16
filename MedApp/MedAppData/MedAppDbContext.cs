using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MedAppCore.Models;
using Microsoft.SqlServer.Management.Smo;
using MedAppCore.Models.Configuration;

namespace MedAppData
{
    public class MedAppDbContext : IdentityDbContext<ApplicationUser>
    {
        public MedAppDbContext(DbContextOptions options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Appointment>()
                .HasKey(bc => new { bc.DoctorId, bc.PatientId });
            #region Appoitment
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

            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            #endregion

            #region DoctorPatient
            modelBuilder.Entity<DoctorPatient>()
                .HasKey(bc => new { bc.DoctorId, bc.PatientId });

            modelBuilder.Entity<DoctorPatient>()

                .HasOne(bc => bc.Doctor)
                .WithMany(b => b.DoctorPatients)
                .HasForeignKey(bc => bc.DoctorId);

            modelBuilder.Entity<DoctorPatient>()
                .HasOne(bc => bc.Patient)
                .WithMany(c => c.DoctorPatients)
                .HasForeignKey(bc => bc.PatientId);
            #endregion

        }

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<DoctorPatient> DoctorPatients { get; set; }
    }
}
