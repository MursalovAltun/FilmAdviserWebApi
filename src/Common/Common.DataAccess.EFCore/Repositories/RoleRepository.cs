using System;
using System.Linq;
using System.Threading.Tasks;
using Common.Entities;
using Common.Entities.System;
using Common.Services.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Common.DataAccess.EFCore.Repositories
{
    public class RoleRepository : BaseRepository<Role, DataContext>, IRoleRepository<Role>
    {
        public RoleRepository(DataContext context) : base(context) { }

        public override async Task<bool> Exists(Role obj, ContextSession session)
        {
            var context = GetContext(session);

            return await context.Set<Role>().Where(x => x.Id == obj.Id).AsNoTracking().CountAsync() > 0;
        }

        public async Task<Role> Get(string name, ContextSession session)
        {
            var context = GetContext(session);

            return await context.Set<Role>().Where(obj => obj.Name == name).AsNoTracking().FirstOrDefaultAsync();
        }

        public override async Task<Role> Get(Guid id, ContextSession session)
        {
            var context = GetContext(session);

            return await context.Set<Role>().Where(obj => obj.Id == id).AsNoTracking().FirstOrDefaultAsync();
        }
    }
}