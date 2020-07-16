using MedAppCore.Models;
using MedAppCore.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MedAppData.Repositories
{
    public class UserRepository<TEntity> : IUserRepository<TEntity> where TEntity : class
    {
        protected readonly UserManager<TEntity> UserManager;

        public UserRepository(UserManager<TEntity> userManager)
        {
            UserManager = userManager;
        }

        public async Task<IdentityResult> AddAsync(TEntity entity, string password)
        {
            var result = await UserManager.CreateAsync(entity, password);
            return result;
        }

        public async Task<IdentityResult> AddToRoleAsync(TEntity entity, string role)
        {
            var result = await UserManager.AddToRoleAsync(entity, role);
            return result;
        }
    }
}
