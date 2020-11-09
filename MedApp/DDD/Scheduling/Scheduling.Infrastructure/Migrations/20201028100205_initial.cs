using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Scheduling.Infrastructure.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "calendars");

            migrationBuilder.CreateTable(
                name: "Appoitments",
                schema: "calendars",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CalendarId = table.Column<Guid>(nullable: true),
                    PatientId = table.Column<string>(nullable: true),
                    StartAppoitmentDate = table.Column<DateTime>(nullable: true),
                    EndAppoitmentAppoitmentDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appoitments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Calendars",
                schema: "calendars",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calendars", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clinics",
                schema: "calendars",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Location = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clinics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MedicalStuffs",
                schema: "calendars",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ClinicId = table.Column<Guid>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    Firstname = table.Column<string>(nullable: true),
                    Lastname = table.Column<string>(nullable: true),
                    RoleCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalStuffs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CalendarUsers",
                schema: "calendars",
                columns: table => new
                {
                    CalendarId = table.Column<Guid>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicalStuffId = table.Column<Guid>(nullable: true),
                    RoleCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalendarUsers", x => new { x.CalendarId, x.Id });
                    table.ForeignKey(
                        name: "FK_CalendarUsers_Calendars_CalendarId",
                        column: x => x.CalendarId,
                        principalSchema: "calendars",
                        principalTable: "Calendars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appoitments",
                schema: "calendars");

            migrationBuilder.DropTable(
                name: "CalendarUsers",
                schema: "calendars");

            migrationBuilder.DropTable(
                name: "Clinics",
                schema: "calendars");

            migrationBuilder.DropTable(
                name: "MedicalStuffs",
                schema: "calendars");

            migrationBuilder.DropTable(
                name: "Calendars",
                schema: "calendars");
        }
    }
}
