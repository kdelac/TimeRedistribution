using Microsoft.EntityFrameworkCore.Migrations;

namespace MedAppData.Migrations
{
    public partial class trigerzaracunanje : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                                    CREATE OR ALTER TRIGGER couting
                                    ON Applications
                                    AFTER INSERT, UPDATE, DELETE
                                    AS  
                                    BEGIN

									IF (SELECT COUNT(Position) AS NumberOfProducts FROM inserted Where Position = 0 GROUP BY OrdinationId) 
									IS null AND (SELECT COUNT(Position) AS NumberOfProducts FROM inserted Where Position = 1 GROUP BY OrdinationId) IS NOT null
									BEGIN
									UPDATE Waitings SET NumberIn = 0, 
									NumberOut = (SELECT COUNT(Position) AS NumberOfProducts FROM inserted Where Position = 1 GROUP BY OrdinationId) WHERE 
									OrdinationId = (SELECT OrdinationId FROM inserted)
									END

									IF (SELECT COUNT(Position) AS NumberOfProducts FROM inserted Where Position = 0 GROUP BY OrdinationId)
									IS NOT null AND (SELECT COUNT(Position) AS NumberOfProducts FROM inserted Where Position = 1 GROUP BY OrdinationId) IS NOT null
									BEGIN
									UPDATE Waitings SET NumberIn = (SELECT COUNT(Position) AS NumberOfProducts FROM inserted Where Position = 0 GROUP BY OrdinationId), 
									NumberOut = (SELECT COUNT(Position) AS NumberOfProducts FROM inserted Where Position = 1 GROUP BY OrdinationId) WHERE 
									OrdinationId = (SELECT OrdinationId FROM inserted)
									END

									IF (SELECT COUNT(Position) AS NumberOfProducts FROM inserted Where Position = 0 GROUP BY OrdinationId)
									IS null AND (SELECT COUNT(Position) AS NumberOfProducts FROM inserted Where Position = 1 GROUP BY OrdinationId) IS null
									BEGIN
									UPDATE Waitings SET NumberIn = 0, 
									NumberOut = 0 WHERE 
									OrdinationId = (SELECT OrdinationId FROM inserted)
									END

									IF (SELECT COUNT(Position) AS NumberOfProducts FROM inserted Where Position = 0 GROUP BY OrdinationId)
									IS NOT null AND (SELECT COUNT(Position) AS NumberOfProducts FROM inserted Where Position = 1 GROUP BY OrdinationId) IS null
									BEGIN
									UPDATE Waitings SET NumberIn = (SELECT COUNT(Position) AS NumberOfProducts FROM inserted Where Position = 0 GROUP BY OrdinationId), 
									NumberOut = 0 WHERE 
									OrdinationId = (SELECT OrdinationId FROM inserted)
									END						
                                    END");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP TRIGGER couting");
        }
    }
}
