using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MedAppCore.Models.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
        new IdentityRole
        {
            Name = "Doctor",
            NormalizedName = "DOCTOR"
        },
        new IdentityRole
        {
            Name = "Patient",
            NormalizedName = "PATIENT"
        });
        }
    }
}
