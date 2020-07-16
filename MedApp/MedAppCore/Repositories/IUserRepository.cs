using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MedAppCore.Repositories
{
    public interface IUserRepository<TEntity> where TEntity : class
    {
        Task<IdentityResult> AddAsync(TEntity entity, string password);
        Task<IdentityResult> AddToRoleAsync(TEntity entity, string role);
    }
}
