using MedAppCore.Models;
using MedAppCore.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedAppCore
{
    public interface ILogRepository : IRepository<TransactionSetup>
    {
        Task<TransactionSetup> GetByAppoitmentIdAsync(Guid id);
        Task<IEnumerable<TransactionSetup>> GetAllWhereAsync();
    }
}