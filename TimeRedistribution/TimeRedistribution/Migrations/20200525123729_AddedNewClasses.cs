using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeRedistribution.Migrations
{
    public partial class AddedNewClasses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Specialization",
                table: "Doctors",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Specialization",
                table: "Doctors");
        }
    }
}
