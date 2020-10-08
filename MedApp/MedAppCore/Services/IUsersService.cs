using MedAppCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MedAppCore.Services
{
    public interface IUsersService
    {
        Task<Users> GetUserById(Guid id);
        Task<Users> CreateUser(Users user);
        Task UpdateUser(Users doctorToBeUpdated, Users doctor);
        Task DeleteUser(Users doctor);
        Task<IEnumerable<Users>> GetAll();
        List<Users> GetAllDoctors();
        List<Users> GetAllPatients();
    }
}
