using Microsoft.EntityFrameworkCore;
using MedAppCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MedAppData
{
    public class MedAppDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<DoctorPatient> DoctorPatients { get; set; }
        public DbSet<TransactionSetupEnd> TransactionSetupEnds { get; set; }
        public DbSet<TransactionSetup> TransactionSetups { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<Ordination> Ordinations { get; set; }
        public DbSet<Waiting> Waitings { get; set; }

        public MedAppDbContext(DbContextOptions<MedAppDbContext> options)
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

            modelBuilder.Entity<Bill>()
                .HasOne(bc => bc.Patient)
                .WithMany(c => c.Bills)
                .HasForeignKey(bc => bc.PatientId);

            modelBuilder.Entity<Bill>()
                .HasOne(bc => bc.Doctor)
                .WithMany(c => c.Bills)
                .HasForeignKey(bc => bc.DoctorId);

            modelBuilder.Entity<TransactionSetup>()
                .ToTable("TransactionSetups");

            modelBuilder.Entity<Application>()
                .HasOne(bc => bc.Ordination)
                .WithMany(bc => bc.Applications)
                .HasForeignKey(bc => bc.OrdinationId);

            modelBuilder.Entity<Waiting>()
                .HasOne(_ => _.Ordination)
                .WithOne(_ => _.Waiting);
        }
    }
}
