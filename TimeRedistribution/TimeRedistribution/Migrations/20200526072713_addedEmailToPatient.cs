using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeRedistribution.Migrations
{
    public partial class addedEmailToPatient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Patients",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Patients");
        }
    }
}
