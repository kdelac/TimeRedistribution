using MedAppCore;
using MedAppCore.Repositories;
using MedAppCore.Repositories.ElasticSearch;
using MedAppData.Repositories;
using MedAppData.Repositories.ElasticSearch;
using Nest;
using System.Threading.Tasks;

namespace MedAppData
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MedAppDbContext _context;
        private readonly ElasticClient _client;
        private PatientRepository _patientRepository;
        private DoctorRepository _doctorRepository;
        private AppointmentRepository _appointmentRepository;
        private DoctorPatientRepository _doctorPatientRepository;
        private BillingRepository _billingRepository;
        private LogRepository _logRepository;

        //private UserSearchRepository _userSearchRepository;
        //private DateSearchRepository _dateSearchRepository;

        public UnitOfWork(MedAppDbContext context)
        {
            _context = context;
            //_client = client;
        }


        public IDoctorRepository Doctors => _doctorRepository = _doctorRepository ?? new DoctorRepository(_context);
        public IAppointmentRepository Appointments => _appointmentRepository = _appointmentRepository ?? new AppointmentRepository(_context);
        public IDoctorPatientRepository DoctorPatients => _doctorPatientRepository = _doctorPatientRepository ?? new DoctorPatientRepository(_context);
        public IPatientRepository Patients => _patientRepository = _patientRepository ?? new PatientRepository(_context);
        public IBillingRepository Billings => _billingRepository = _billingRepository ?? new BillingRepository(_context);
        public ILogRepository LogRepository => _logRepository = _logRepository ?? new LogRepository(_context);

        //public IUserSearchRepository UserSearch => _userSearchRepository = _userSearchRepository ?? new UserSearchRepository(_client);

        //public IDateSearchRepository DateSearch => _dateSearchRepository = _dateSearchRepository ?? new DateSearchRepository(_client);       

        public async Task<int> Save()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
