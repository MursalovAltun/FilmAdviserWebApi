using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Entities;
using Common.Entities.System;

namespace Common.Services.Infrastructure.Repositories
{
    public interface IUserRepository<TUser> where TUser : User
    {
        Task Delete(Guid id, ContextSession session);
        Task<TUser> GetByUserName(string username, ContextSession session);
        Task<TUser> GetByEmail(string email, ContextSession session);
        Task<TUser> Get(Guid id, ContextSession session);
        Task<TUser> Edit(TUser user, ContextSession session);

        Task<IEnumerable<TUser>> GetAll(ContextSession session);
    }
}