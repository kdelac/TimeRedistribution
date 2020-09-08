CREATE TABLE [dbo].[Bills] (
    [Id]        UNIQUEIDENTIFIER NOT NULL,
    [Name]      NVARCHAR (MAX)   NULL,
    [Total]     NVARCHAR (MAX)   NULL,
    [DoctorId]  UNIQUEIDENTIFIER NOT NULL,
    [PatientId] UNIQUEIDENTIFIER NOT NULL,
    [Subtotal] NCHAR(10) NULL, 
    CONSTRAINT [PK_Bills] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Bills_Doctors_DoctorId] FOREIGN KEY ([DoctorId]) REFERENCES [dbo].[Doctors] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Bills_Patients_PatientId] FOREIGN KEY ([PatientId]) REFERENCES [dbo].[Patients] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Bills_DoctorId]
    ON [dbo].[Bills]([DoctorId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Bills_PatientId]
    ON [dbo].[Bills]([PatientId] ASC);

