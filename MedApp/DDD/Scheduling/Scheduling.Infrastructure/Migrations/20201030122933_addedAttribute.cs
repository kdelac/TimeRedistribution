using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Scheduling.Infrastructure.Migrations
{
    public partial class addedAttribute : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DoctorId",
                schema: "calendars",
                table: "Calendars",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DoctorId",
                schema: "calendars",
                table: "Calendars");
        }
    }
}
