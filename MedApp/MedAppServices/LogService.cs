using MedAppCore;
using MedAppCore.Models;
using MedAppCore.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MedAppServices
{
    public class LogService : ILogService
    {
        private readonly IUnitOfWork _unitOfWork;
        public LogService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Task<TransactionSetup> CreateLog(TransactionSetup newLog)
        {
            throw new NotImplementedException();
        }

        public Task DeleteLog(TransactionSetup log)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TransactionSetup>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<TransactionSetup> GetLogByAppoitmentId(Guid? id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateLog(TransactionSetup logToBeUpdated, TransactionSetup log)
        {
            throw new NotImplementedException();
        }
    }
}
