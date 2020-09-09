using MedAppCore;
using MedAppCore.Models;
using MedAppCore.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedAppServices
{
    public class OrdinationService : IOrdinationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrdinationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<Ordination>> GetAllOrdination()
        {
            return await _unitOfWork.Ordinations.GetAllAsync();
        }

        public async Task<Ordination> GetOrdinationById(Guid id)
        {
            return await _unitOfWork.Ordinations.GetByIdAsync(id);
        }
    }
}