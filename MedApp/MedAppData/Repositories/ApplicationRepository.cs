using MedAppCore.Models;
using MedAppCore.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedAppData.Repositories
{
    class ApplicationRepository : Repository<Application>, IApplicationRepository
    {
        public ApplicationRepository(MedAppDbContext context)
            : base(context)
        { }

        public async Task DeleteAppoitmentWithPatientId(Guid patientId)
        {
            var application = await MedAppDbContext.Applications.FirstOrDefaultAsync(_ => _.PatientId == patientId);
            MedAppDbContext.Applications.Remove(application);
        }

        public async Task<int> GetNumberInside(Guid ordinationId)
        {
            var br = await MedAppDbContext.Applications.Where(_ => _.OrdinationId == ordinationId && _.Position == Status2.In).ToListAsync();
            var numberof = br.Count();
            return numberof;
        }

        public async Task<int> GetNumberOutside(Guid ordinationId)
        {
            var br = await MedAppDbContext.Applications.Where(_ => _.OrdinationId == ordinationId && _.Position == Status2.Out).ToListAsync();
            var numberof = br.Count();
            return numberof;
        }

        private MedAppDbContext MedAppDbContext
        {
            get { return Context as MedAppDbContext; }
        }
    }
}
