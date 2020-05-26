using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeRedistribution.Migrations
{
    public partial class addedattributes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EndBreak",
                table: "Doctors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EndOfWorl",
                table: "Doctors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SrartBreak",
                table: "Doctors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StartOfWork",
                table: "Doctors",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndBreak",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "EndOfWorl",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "SrartBreak",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "StartOfWork",
                table: "Doctors");
        }
    }
}
