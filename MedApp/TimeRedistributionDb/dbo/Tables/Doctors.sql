CREATE TABLE [dbo].[Doctors] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [Name]        NVARCHAR (MAX)   NULL,
    [Surname]     NVARCHAR (MAX)   NULL,
    [DateOfBirth] DATETIME2 (7)    NOT NULL,
    CONSTRAINT [PK_Doctors] PRIMARY KEY CLUSTERED ([Id] ASC)
);

