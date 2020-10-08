using MedAppCore.MongoDB;
using MedAppCore.Repositories.MongoDb;
using System;
using System.Collections.Generic;
using System.Text;

namespace MedAppCore
{
    public interface IMongoUnitOfWork
    {
        IDoctorMongoRepository Doctors { get; }
        IUsersRepository Users { get; }
        IMongoAppointmentRepository Appointments { get; }
    }
}
