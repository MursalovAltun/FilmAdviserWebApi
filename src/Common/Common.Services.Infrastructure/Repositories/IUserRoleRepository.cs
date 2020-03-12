using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Entities;
using Common.Entities.System;

namespace Common.Services.Infrastructure.Repositories
{
    public interface IUserRoleRepository<TUserRole> where TUserRole : UserRole
    {
        Task<TUserRole> Add(TUserRole userRole, ContextSession session);
        Task<TUserRole> Get(Guid userId, Guid roleId, ContextSession session);
        Task Delete(Guid userId, Guid roleId, ContextSession session);
        Task<IList<string>> GetByUserId(Guid userId, ContextSession session);
    }
}