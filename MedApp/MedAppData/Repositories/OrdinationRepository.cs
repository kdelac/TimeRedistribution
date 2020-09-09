using MedAppCore.Models;
using MedAppCore.Repositories;

namespace MedAppData.Repositories
{
    class OrdinationRepository : Repository<Ordination>, IOrdinationRepository
    {
        public OrdinationRepository(MedAppDbContext context)
            : base(context)
        { }
    }
}
