using System;
using System.Threading.Tasks;
using Common.Entities;
using Common.Entities.System;

namespace Common.Services.Infrastructure.Repositories
{
    public interface IUserPhotoRepository
    {
        Task<UserPhoto> Get(Guid id, ContextSession session);
    }
}