CREATE TABLE [dbo].[Patients] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [Name]        NVARCHAR (MAX)   NULL,
    [Surname]     NVARCHAR (MAX)   NULL,
    [Email]       NVARCHAR (MAX)   NULL,
    [DateOfBirth] DATETIME2 (7)    NOT NULL,
    CONSTRAINT [PK_Patients] PRIMARY KEY CLUSTERED ([Id] ASC)
);

