using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeRedistribution.Model;

namespace TimeRedistribution
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>()
                .HasKey(bc => new { bc.DoctorId, bc.PatientId});
            modelBuilder.Entity<Appointment>()
                .HasOne(bc => bc.Doctor)
                .WithMany(b => b.Appointments)
                .HasForeignKey(bc => bc.DoctorId);
            modelBuilder.Entity<Appointment>()
                .HasOne(bc => bc.Patient)
                .WithMany(c => c.Appointments)
                .HasForeignKey(bc => bc.PatientId);
        }

    }
}
