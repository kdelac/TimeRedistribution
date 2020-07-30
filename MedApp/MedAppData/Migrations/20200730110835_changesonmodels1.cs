using Microsoft.EntityFrameworkCore.Migrations;

namespace MedAppData.Migrations
{
    public partial class changesonmodels1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EventRaised",
                table: "TransactionSetups",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventRaised",
                table: "TransactionSetups");
        }
    }
}
