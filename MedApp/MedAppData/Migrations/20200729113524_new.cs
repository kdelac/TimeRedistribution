using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MedAppData.Migrations
{
    public partial class @new : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bills",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Total = table.Column<string>(nullable: true),
                    DoctorId = table.Column<Guid>(nullable: false),
                    PatientId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bills_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bills_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransactionSetupEnds",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TransactionStatus = table.Column<int>(nullable: false),
                    appoitmentId = table.Column<Guid>(nullable: false),
                    billId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionSetupEnds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransactionSetups",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TransactionStatus = table.Column<int>(nullable: false),
                    appoitmentId = table.Column<Guid>(nullable: false),
                    billId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionSetups", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bills_DoctorId",
                table: "Bills",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_PatientId",
                table: "Bills",
                column: "PatientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bills");

            migrationBuilder.DropTable(
                name: "TransactionSetupEnds");

            migrationBuilder.DropTable(
                name: "TransactionSetups");
        }
    }
}
