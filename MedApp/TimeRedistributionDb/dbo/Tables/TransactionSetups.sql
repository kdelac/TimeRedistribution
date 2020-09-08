CREATE TABLE [dbo].[TransactionSetups] (
    [Id]                UNIQUEIDENTIFIER NOT NULL,
    [TransactionStatus] INT              NOT NULL,
    [AppoitmentId]      UNIQUEIDENTIFIER NULL,
    [BillId]            UNIQUEIDENTIFIER NULL,
    [EventRaised]       BIT              DEFAULT (CONVERT([bit],(0))) NOT NULL,
    CONSTRAINT [PK_TransactionSetups] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO

                                    CREATE   TRIGGER deleteFromTs
                                    ON TransactionSetups
                                    AFTER INSERT, UPDATE 
                                    AS  
                                    BEGIN
                                    SET NOCOUNT ON;   
                                    IF (SELECT TransactionStatus FROM  inserted) = 5 OR (SELECT TransactionStatus FROM  inserted) = 4 
	                                DELETE FROM TransactionSetups WHERE Id = (SELECT inserted.Id FROM inserted)
                                    END

GO

                                    CREATE   TRIGGER insertIntoTSEnds
                                    ON TransactionSetups
                                    AFTER DELETE 
                                    AS
                                    BEGIN
                                    INSERT INTO TransactionSetupEnds([Id],[TransactionStatus],[AppoitmentId],[BillId],[Date])
	                                SELECT d.Id, TransactionStatus, AppoitmentId, BillId, GETDATE()
                                    FROM deleted d;
                                    END
