using MedAppCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MedAppCore.Services
{
    public interface ILogService
    {
        Task<TransactionSetup> GetLogByAppoitmentId(Guid id);
        Task<TransactionSetup> CreateLog(TransactionSetup newLog);
        Task UpdateLog(TransactionSetup logToBeUpdated, TransactionSetup log);
        Task DeleteLog(TransactionSetup log);
        Task<IEnumerable<TransactionSetup>> GetAll();
        Task<IEnumerable<TransactionSetup>> GetAllWhere();
    }
}
