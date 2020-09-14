using Microsoft.EntityFrameworkCore.Migrations;

namespace MedAppData.Migrations
{
    public partial class triger : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                                    CREATE OR ALTER TRIGGER insertIntaWaiting
                                    ON Ordinations
                                    AFTER INSERT 
                                    AS  
                                    BEGIN
                                    SET NOCOUNT ON;   
                                    INSERT INTO Waitings (Id, OrdinationId, NumberIn, NumberOut) Values (NEWID(), (SELECT Id FROM  inserted), 0, 0)
                                    END");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP TRIGGER insertIntaWaiting");
        }
    }
}
