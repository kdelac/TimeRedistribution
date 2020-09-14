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


									IF (SELECT COUNT(Position) AS NumberOfProducts FROM Applications Where Position = 0 AND OrdinationId = (SELECT OrdinationId FROM inserted)) 
									IS null AND (SELECT COUNT(Position) AS NumberOfProducts FROM Applications Where Position = 1 AND OrdinationId = (SELECT OrdinationId FROM inserted)) IS NOT null
									BEGIN
									UPDATE Waitings SET NumberIn = 0, 
									NumberOut = (SELECT COUNT(Position) AS NumberOfProducts FROM Applications Where Position = 1 AND OrdinationId = (SELECT OrdinationId FROM inserted)) WHERE 
									OrdinationId = (SELECT OrdinationId FROM inserted)
									END

									IF (SELECT COUNT(Position) AS NumberOfProducts FROM Applications Where Position = 0 AND OrdinationId = (SELECT OrdinationId FROM inserted))
									IS NOT null AND (SELECT COUNT(Position) AS NumberOfProducts FROM Applications Where Position = 1 AND OrdinationId = (SELECT OrdinationId FROM inserted)) IS NOT null
									BEGIN
									UPDATE Waitings SET NumberIn = (SELECT COUNT(Position) AS NumberOfProducts FROM Applications Where Position = 0 AND OrdinationId = (SELECT OrdinationId FROM inserted)), 
									NumberOut = (SELECT COUNT(Position) AS NumberOfProducts FROM Applications Where Position = 1 AND OrdinationId = (SELECT OrdinationId FROM inserted)) WHERE 
									OrdinationId = (SELECT OrdinationId FROM inserted)
									END

									IF (SELECT COUNT(Position) AS NumberOfProducts FROM Applications Where Position = 0 AND OrdinationId = (SELECT OrdinationId FROM inserted))
									IS null AND (SELECT COUNT(Position) AS NumberOfProducts FROM Applications Where Position = 1 AND OrdinationId = (SELECT OrdinationId FROM inserted)) IS null
									BEGIN
									UPDATE Waitings SET NumberIn = 0, 
									NumberOut = 0 WHERE 
									OrdinationId = (SELECT OrdinationId FROM inserted)
									END

									IF (SELECT COUNT(Position) AS NumberOfProducts FROM Applications Where Position = 0 AND OrdinationId = (SELECT OrdinationId FROM inserted))
									IS NOT null AND (SELECT COUNT(Position) AS NumberOfProducts FROM Applications Where Position = 1 AND OrdinationId = (SELECT OrdinationId FROM inserted)) IS null
									BEGIN
									UPDATE Waitings SET NumberIn = (SELECT COUNT(Position) AS NumberOfProducts FROM Applications Where Position = 0 AND OrdinationId = (SELECT OrdinationId FROM inserted)), 
									NumberOut = 0 WHERE 
									OrdinationId = (SELECT OrdinationId FROM inserted)
									END		
									

									IF (SELECT COUNT(Position) AS NumberOfProducts FROM Applications Where Position = 0 AND OrdinationId = (SELECT OrdinationId FROM deleted)) 
									IS null AND (SELECT COUNT(Position) AS NumberOfProducts FROM Applications Where Position = 1 AND OrdinationId = (SELECT OrdinationId FROM deleted)) IS NOT null
									BEGIN
									UPDATE Waitings SET NumberIn = 0, 
									NumberOut = (SELECT COUNT(Position) AS NumberOfProducts FROM Applications Where Position = 1 AND OrdinationId = (SELECT OrdinationId FROM deleted)) WHERE 
									OrdinationId = (SELECT OrdinationId FROM deleted)
									END

									IF (SELECT COUNT(Position) AS NumberOfProducts FROM Applications Where Position = 0 AND OrdinationId = (SELECT OrdinationId FROM deleted))
									IS NOT null AND (SELECT COUNT(Position) AS NumberOfProducts FROM Applications Where Position = 1 AND OrdinationId = (SELECT OrdinationId FROM deleted)) IS NOT null
									BEGIN
									UPDATE Waitings SET NumberIn = (SELECT COUNT(Position) AS NumberOfProducts FROM Applications Where Position = 0 AND OrdinationId = (SELECT OrdinationId FROM deleted)), 
									NumberOut = (SELECT COUNT(Position) AS NumberOfProducts FROM Applications Where Position = 1 AND OrdinationId = (SELECT OrdinationId FROM deleted)) WHERE 
									OrdinationId = (SELECT OrdinationId FROM deleted)
									END

									IF (SELECT COUNT(Position) AS NumberOfProducts FROM Applications Where Position = 0 AND OrdinationId = (SELECT OrdinationId FROM deleted))
									IS null AND (SELECT COUNT(Position) AS NumberOfProducts FROM Applications Where Position = 1 AND OrdinationId = (SELECT OrdinationId FROM deleted)) IS null
									BEGIN
									UPDATE Waitings SET NumberIn = 0, 
									NumberOut = 0 WHERE 
									OrdinationId = (SELECT OrdinationId FROM deleted)
									END

									IF (SELECT COUNT(Position) AS NumberOfProducts FROM Applications Where Position = 0 AND OrdinationId = (SELECT OrdinationId FROM deleted))
									IS NOT null AND (SELECT COUNT(Position) AS NumberOfProducts FROM Applications Where Position = 1 AND OrdinationId = (SELECT OrdinationId FROM deleted)) IS null
									BEGIN
									UPDATE Waitings SET NumberIn = (SELECT COUNT(Position) AS NumberOfProducts FROM Applications Where Position = 0 AND OrdinationId = (SELECT OrdinationId FROM deleted)), 
									NumberOut = 0 WHERE 
									OrdinationId = (SELECT OrdinationId FROM deleted)
									END		
                                    END");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP TRIGGER couting");
        }
    }
}
