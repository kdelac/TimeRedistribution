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
        public async Task<TransactionSetup> CreateLog(TransactionSetup newLog)
        {
            newLog.Id = Guid.NewGuid();
            await _unitOfWork.LogRepository.AddAsync(newLog);
            await _unitOfWork.Save();
            return newLog;
        }

        public Task DeleteLog(TransactionSetup log)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TransactionSetup>> GetAll()
        {
            return await _unitOfWork.LogRepository.GetAllAsync();
        }
        
        public async Task<IEnumerable<TransactionSetup>> GetAllWhere()
        {
            return await _unitOfWork.LogRepository.GetAllWhereAsync();
        }

        public async Task<TransactionSetup> GetLogByAppoitmentId(Guid id)
        {
            return await _unitOfWork.LogRepository.GetByAppoitmentIdAsync(id);
        }

        public async Task UpdateLog(TransactionSetup logToBeUpdated, TransactionSetup log)
        {
            logToBeUpdated.AppoitmentId = log.AppoitmentId;
            logToBeUpdated.BillId = log.BillId;
            logToBeUpdated.EventRaised = log.EventRaised;
            logToBeUpdated.TransactionStatus = log.TransactionStatus;

            await _unitOfWork.Save();
        }
    }
}
