using System;
using System.Threading.Tasks;
using Common.Entities;
using Common.Entities.System;

namespace Common.Services.Infrastructure.Repositories
{
    public interface ISettingsRepository
    {
        Task<Settings> Get(Guid id, ContextSession session);
        Task<Settings> Edit(Settings setting, ContextSession session);
    }
}