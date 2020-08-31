using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MedAppData.Migrations
{
    public partial class changess : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "BillId",
                table: "TransactionSetupEnds",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "AppoitmentId",
                table: "TransactionSetupEnds",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "TransactionSetupEnds",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "TransactionSetupEnds");

            migrationBuilder.AlterColumn<Guid>(
                name: "BillId",
                table: "TransactionSetupEnds",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "AppoitmentId",
                table: "TransactionSetupEnds",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);
        }
    }
}
