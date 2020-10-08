using MedAppCore;
using MedAppCore.MongoDB;
using MedAppCore.Repositories.MongoDb;
using MedAppData.Repositories.Mongo;

namespace MedAppData
{
    public class MongoUnitOfWork : IMongoUnitOfWork
    {
        private readonly IMongoDBContext _context;

        private DoctorMongoRepository _doctorRepository;
        private UsersRepository _usersRepository;
        private MongoAppointmentRepository _appointmentRepository;

        public MongoUnitOfWork(IMongoDBContext context)
        {
            _context = context;
        }

        public IDoctorMongoRepository Doctors => _doctorRepository = _doctorRepository ?? new DoctorMongoRepository(_context);
        public IUsersRepository Users => _usersRepository = _usersRepository ?? new UsersRepository(_context);
        public IMongoAppointmentRepository Appointments => _appointmentRepository = _appointmentRepository ?? new MongoAppointmentRepository(_context);
    }
}
