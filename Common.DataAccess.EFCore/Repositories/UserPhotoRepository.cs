using System;
using System.Linq;
using System.Threading.Tasks;
using Common.Entities;
using Common.Entities.System;
using Common.Services.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Common.DataAccess.EFCore.Repositories
{
    public class UserPhotoRepository : BaseRepository<UserPhoto, DataContext>, IUserPhotoRepository
    {
        public UserPhotoRepository(DataContext context) : base(context)
        {
        }

        public override async Task<bool> Exists(UserPhoto obj, ContextSession session)
        {
            var context = GetContext(session);
            return await context.UserPhotos.Where(x => x.Id == obj.Id).AsNoTracking().CountAsync() > 0;
        }

        public override async Task<UserPhoto> Get(Guid id, ContextSession session)
        {
            var context = GetContext(session);
            return await context.UserPhotos.Where(obj => obj.Id == id).AsNoTracking().FirstOrDefaultAsync();
        }
    }
}