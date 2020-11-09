using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Scheduling.Domain.Calendars;
using Scheduling.Domain.CalendarUsers;
using Scheduling.Domain.MedicalStaff;

namespace Scheduling.Infrastructure.Domain.Calendars
{
    internal class CalendarEntityTypeConfiguration : IEntityTypeConfiguration<Calendar>
    {
        public void Configure(EntityTypeBuilder<Calendar> builder)
        {
            builder.ToTable("Calendars", "calendars");
            builder.HasKey(x => x.Id);
            builder.Property<string>("_title").HasColumnName("Title");


            builder.OwnsMany<CalendarUser>("_calendarUsers", y => {
                y.WithOwner().HasForeignKey("CalendarId");
                y.ToTable("CalendarUsers", "calendars");
                y.Property<CalendarId>("CalendarId");
                y.Property<MedicalStuffId>("MedicalStuffId");

                y.OwnsOne<MedicalStuffRole>("_role", x =>
                {
                    x.Property(z => z.Value).HasColumnName("RoleCode");
                });
            });

        }
    }
}
