using MedAppCore;
using MedAppCore.Models;
using MedAppCore.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedAppServices
{
    public class UsersService : IUsersService
    {
        private readonly IMongoUnitOfWork _mongoUnitOfWork;

        public UsersService(
            IMongoUnitOfWork mongoUnitOfWork
            )
        {
            _mongoUnitOfWork = mongoUnitOfWork;
        }

        public async Task<Users> CreateUser(Users user)
        {
            await _mongoUnitOfWork.Users.Create(user);
            return user;
        }

        public Task DeleteUser(Users doctor)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Users>> GetAll()
        {
            return await _mongoUnitOfWork.Users.Get();
        }

        public async Task<Users> GetUserById(Guid id)
        {
            return await _mongoUnitOfWork.Users.Get(id);
        }

        public Task UpdateUser(Users doctorToBeUpdated, Users doctor)
        {
            throw new NotImplementedException();
        }

        public List<Users> GetAllDoctors()
        {
            var task =  _mongoUnitOfWork.Users.AsQueryable().Where(_ => _.Role == Role.Doctor).ToList();
            return task;
        }

        public List<Users> GetAllPatients()
        {
            var task = _mongoUnitOfWork.Users.AsQueryable().Where(_ => _.Role == Role.Patient).ToList();
            return task;
        }
    }
}
