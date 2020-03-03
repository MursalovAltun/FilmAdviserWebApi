using System;
using System.Threading.Tasks;
using Common.Entities;
using Common.Entities.System;

namespace Common.Services.Infrastructure.Repositories
{
    public interface IRoleRepository<TRole> where TRole : Role
    {
        Task Delete(Guid id, ContextSession session);
        Task<TRole> Get(Guid id, ContextSession session);
        Task<TRole> Get(string name, ContextSession session);
        Task<TRole> Edit(TRole role, ContextSession session);
    }
}