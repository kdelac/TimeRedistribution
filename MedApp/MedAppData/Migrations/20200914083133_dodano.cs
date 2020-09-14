using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MedAppData.Migrations
{
    public partial class dodano : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Waitings",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    OrdinationId = table.Column<Guid>(nullable: false),
                    NumberIn = table.Column<int>(nullable: false),
                    NumberOut = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Waitings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Waitings_Ordinations_OrdinationId",
                        column: x => x.OrdinationId,
                        principalTable: "Ordinations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Waitings_OrdinationId",
                table: "Waitings",
                column: "OrdinationId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Waitings");
        }
    }
}
