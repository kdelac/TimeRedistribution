using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scheduling.Domain.MedicalStaff
{
    public interface IMedicalRepository
    {
        Task AddAsync(MedicalStuff calendar);
        Task Add(MedicalStuff calendar);

        Task<MedicalStuff> GetByIdAsync(MedicalStuffId id);

        Task<MedicalStuff> GetById(MedicalStuffId id);
        Task<List<MedicalStuff>> GetAll();

        Task Save();
    }
}
