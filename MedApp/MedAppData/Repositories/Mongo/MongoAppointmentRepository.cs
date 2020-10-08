using MedAppCore;
using MedAppCore.Models;
using MedAppCore.Repositories.MongoDb;
using System;
using System.Collections.Generic;
using System.Text;

namespace MedAppData.Repositories.Mongo
{
    public class MongoAppointmentRepository : MongoRepository<AppointmentBase>, IMongoAppointmentRepository
    {
        public MongoAppointmentRepository(IMongoDBContext context) : base(context) { }
    }
}
