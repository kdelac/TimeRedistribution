using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Scheduling.Infrastructure.Migrations
{
    public partial class attribute : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DoctorId",
                schema: "calendars",
                table: "Calendars");

            migrationBuilder.AddColumn<Guid>(
                name: "CalendarId",
                schema: "calendars",
                table: "MedicalStuffs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CalendarId",
                schema: "calendars",
                table: "MedicalStuffs");

            migrationBuilder.AddColumn<Guid>(
                name: "DoctorId",
                schema: "calendars",
                table: "Calendars",
                type: "uniqueidentifier",
                nullable: true);
        }
    }
}
