using MedAppCore;
using MedAppCore.Repositories;
using MedAppData.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MedAppData
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MedAppDbContext _context;
        private PatientRepository _patientRepository;
        private DoctorRepository _doctorRepository;
        private AppointmentRepository _appointmentRepository;

        public UnitOfWork(MedAppDbContext context)
        {
            this._context = context;
        }


        public IDoctorRepository Doctors => _doctorRepository = _doctorRepository ?? new DoctorRepository(_context);

        public IAppointmentRepository Appointments => _appointmentRepository = _appointmentRepository ?? new AppointmentRepository(_context);

        public IPatientRepository Patients => _patientRepository = _patientRepository ?? new PatientRepository(_context);

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
