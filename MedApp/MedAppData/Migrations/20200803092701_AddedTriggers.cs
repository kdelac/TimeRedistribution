using Microsoft.EntityFrameworkCore.Migrations;

namespace MedAppData.Migrations
{
    public partial class AddedTriggers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                                    CREATE OR ALTER TRIGGER deleteFromTs
                                    ON TransactionSetups
                                    AFTER INSERT, UPDATE 
                                    AS  
                                    BEGIN
                                    SET NOCOUNT ON;   
                                    IF (SELECT TransactionStatus FROM  inserted) = 5 OR (SELECT TransactionStatus FROM  inserted) = 4 
	                                DELETE FROM TransactionSetups WHERE Id = (SELECT inserted.Id FROM inserted)
                                    END");

            migrationBuilder.Sql(@"
                                    CREATE OR ALTER TRIGGER insertIntoTSEnds
                                    ON TransactionSetups
                                    AFTER DELETE 
                                    AS
                                    BEGIN
                                    INSERT INTO TransactionSetupEnds([Id],[TransactionStatus],[AppoitmentId],[BillId],[Date])
	                                SELECT d.Id, TransactionStatus, AppoitmentId, BillId, GETDATE()
                                    FROM deleted d;
                                    END");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP TRIGGER deleteFromTs");

            migrationBuilder.Sql(@"DROP TRIGGER insertIntoTSEnds");
        }
    }
}
