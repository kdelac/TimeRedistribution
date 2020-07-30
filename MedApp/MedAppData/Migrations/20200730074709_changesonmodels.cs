using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MedAppData.Migrations
{
    public partial class changesonmodels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "billId",
                table: "TransactionSetups",
                newName: "BillId");

            migrationBuilder.RenameColumn(
                name: "appoitmentId",
                table: "TransactionSetups",
                newName: "AppoitmentId");

            migrationBuilder.RenameColumn(
                name: "billId",
                table: "TransactionSetupEnds",
                newName: "BillId");

            migrationBuilder.RenameColumn(
                name: "appoitmentId",
                table: "TransactionSetupEnds",
                newName: "AppoitmentId");

            migrationBuilder.AlterColumn<Guid>(
                name: "BillId",
                table: "TransactionSetups",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "AppoitmentId",
                table: "TransactionSetups",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BillId",
                table: "TransactionSetups",
                newName: "billId");

            migrationBuilder.RenameColumn(
                name: "AppoitmentId",
                table: "TransactionSetups",
                newName: "appoitmentId");

            migrationBuilder.RenameColumn(
                name: "BillId",
                table: "TransactionSetupEnds",
                newName: "billId");

            migrationBuilder.RenameColumn(
                name: "AppoitmentId",
                table: "TransactionSetupEnds",
                newName: "appoitmentId");

            migrationBuilder.AlterColumn<Guid>(
                name: "billId",
                table: "TransactionSetups",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "appoitmentId",
                table: "TransactionSetups",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);
        }
    }
}
