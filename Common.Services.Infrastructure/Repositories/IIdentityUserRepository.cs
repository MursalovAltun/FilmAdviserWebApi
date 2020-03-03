using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Entities;
using Common.Entities.System;

namespace Common.Services.Infrastructure.Repositories
{
    public interface IIdentityUserRepository<TUser> where TUser : User
    {
        Task Delete(Guid id, ContextSession session);
        Task<TUser> GetById(Guid id, ContextSession session);
        Task<TUser> GetByUserName(string username, ContextSession session);
        Task<IList<TUser>> GetUsersByRole(Guid roleId, ContextSession session);
        Task<IList<TUser>> GetUsersByClaim(string claimType, string claimValue, ContextSession session);
        Task<TUser> GetByEmail(string email, ContextSession session);
        Task<TUser> Edit(TUser user, ContextSession session);
    }
}