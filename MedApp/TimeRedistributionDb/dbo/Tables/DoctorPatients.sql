CREATE TABLE [dbo].[DoctorPatients] (
    [DoctorId]  UNIQUEIDENTIFIER NOT NULL,
    [PatientId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_DoctorPatients] PRIMARY KEY CLUSTERED ([DoctorId] ASC, [PatientId] ASC),
    CONSTRAINT [FK_DoctorPatients_Doctors_DoctorId] FOREIGN KEY ([DoctorId]) REFERENCES [dbo].[Doctors] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_DoctorPatients_Patients_PatientId] FOREIGN KEY ([PatientId]) REFERENCES [dbo].[Patients] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_DoctorPatients_PatientId]
    ON [dbo].[DoctorPatients]([PatientId] ASC);

