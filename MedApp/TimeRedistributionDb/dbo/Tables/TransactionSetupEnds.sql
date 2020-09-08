CREATE TABLE [dbo].[TransactionSetupEnds] (
    [Id]                UNIQUEIDENTIFIER NOT NULL,
    [TransactionStatus] INT              NOT NULL,
    [AppoitmentId]      UNIQUEIDENTIFIER NULL,
    [BillId]            UNIQUEIDENTIFIER NULL,
    [Date]              DATETIME2 (7)    DEFAULT ('0001-01-01T00:00:00.0000000') NOT NULL,
    CONSTRAINT [PK_TransactionSetupEnds] PRIMARY KEY CLUSTERED ([Id] ASC)
);

