CREATE TABLE [dbo].[Appointments] (
    [DoctorId]  UNIQUEIDENTIFIER NOT NULL,
    [PatientId] UNIQUEIDENTIFIER NOT NULL,
    [Id]        UNIQUEIDENTIFIER NOT NULL,
    [DateTime]  DATETIME2 (7)    NOT NULL,
    [Status]    NVARCHAR (450)   NULL,
    CONSTRAINT [PK_Appointments] PRIMARY KEY CLUSTERED ([DoctorId] ASC, [PatientId] ASC),
    CONSTRAINT [FK_Appointments_Doctors_DoctorId] FOREIGN KEY ([DoctorId]) REFERENCES [dbo].[Doctors] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Appointments_Patients_PatientId] FOREIGN KEY ([PatientId]) REFERENCES [dbo].[Patients] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Appointments_PatientId]
    ON [dbo].[Appointments]([PatientId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Appointments_DateTime_Status]
    ON [dbo].[Appointments]([DateTime] ASC, [Status] ASC);

