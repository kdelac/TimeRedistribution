using MedAppCore.Models;
using MedAppCore.Repositories;

namespace MedAppData.Repositories
{
    class BillingRepository : Repository<Bill>, IBillingRepository
    {
        public BillingRepository(MedAppDbContext context)
            : base(context)
        { }

        private MedAppDbContext MedAppDbContext
        {
            get { return Context as MedAppDbContext; }
        }
    }
}
