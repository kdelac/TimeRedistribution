using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scheduling.Domain.MedicalStaff
{
    public interface IMedicalRepository
    {
        Task AddAsync(MedicalStuff calendar);

        Task<MedicalStuff> GetByIdAsync(MedicalStuffId id);

        Task Save();
    }
}
