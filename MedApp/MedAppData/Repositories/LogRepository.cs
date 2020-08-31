using MedAppCore;
using MedAppCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedAppData.Repositories
{
    class LogRepository : Repository<TransactionSetup>, ILogRepository
    {
        public LogRepository(MedAppDbContext context)
            : base(context)
        { }

        public async Task<TransactionSetup> GetByAppoitmentIdAsync(Guid id)
        {
            return await MedAppDbContext.TransactionSetups.FirstOrDefaultAsync(_ => _.AppoitmentId == id);
        }

        public async Task<IEnumerable<TransactionSetup>> GetAllWhereAsync()
        {
            return await MedAppDbContext.TransactionSetups.Where(_ => 
                    !_.EventRaised && _.TransactionStatus == Status.BilligSucces 
                    && _.TransactionStatus == Status.Start).ToListAsync();
        }

        private MedAppDbContext MedAppDbContext
        {
            get { return Context as MedAppDbContext; }
        }
    }
}
