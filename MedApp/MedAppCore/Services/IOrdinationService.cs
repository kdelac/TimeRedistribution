using MedAppCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MedAppCore.Services
{
    public interface IOrdinationService
    {
        Task<Ordination> GetOrdinationById(Guid id);
        Task<IEnumerable<Ordination>> GetAllOrdination();
    }
}
