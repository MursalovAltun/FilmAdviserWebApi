using System;
using System.Linq;
using System.Threading.Tasks;
using Common.Entities;
using Common.Entities.System;
using Common.Services.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Common.DataAccess.EFCore.Repositories
{
    public class SettingsRepository : BaseRepository<Settings, DataContext>, ISettingsRepository
    {
        public SettingsRepository(DataContext context) : base(context) { }

        public override async Task<bool> Exists(Settings obj, ContextSession session)
        {
            var context = GetContext(session);
            return await context.Settings.Where(x => x.Id == obj.Id).AsNoTracking().CountAsync() > 0;
        }

        public override async Task<Settings> Get(Guid id, ContextSession session)
        {
            var context = GetContext(session);
            return await context.Settings.Where(obj => obj.Id == id).AsNoTracking().FirstOrDefaultAsync();
        }
    }
}