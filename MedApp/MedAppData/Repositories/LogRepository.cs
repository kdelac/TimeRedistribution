using MedAppCore;
using MedAppCore.Models;

namespace MedAppData.Repositories
{
    class LogRepository : Repository<TransactionSetup>, ILogRepository
    {
        public LogRepository(MedAppDbContext context)
            : base(context)
        { }

        private MedAppDbContext MedAppDbContext
        {
            get { return Context as MedAppDbContext; }
        }
    }
}
