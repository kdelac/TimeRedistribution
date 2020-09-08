﻿CREATE TABLE [dbo].[ApiResourceProperties] (
    [Id]            INT             IDENTITY (1, 1) NOT NULL,
    [Key]           NVARCHAR (250)  NOT NULL,
    [Value]         NVARCHAR (2000) NOT NULL,
    [ApiResourceId] INT             NOT NULL,
    CONSTRAINT [PK_ApiResourceProperties] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ApiResourceProperties_ApiResources_ApiResourceId] FOREIGN KEY ([ApiResourceId]) REFERENCES [dbo].[ApiResources] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_ApiResourceProperties_ApiResourceId]
    ON [dbo].[ApiResourceProperties]([ApiResourceId] ASC);

