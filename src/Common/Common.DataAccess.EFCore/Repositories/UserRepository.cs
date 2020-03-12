using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Entities;
using Common.Entities.System;
using Common.Services.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Common.DataAccess.EFCore.Repositories
{
    public class UserRepository : BaseRepository<User, DataContext>, IUserRepository<User>
    {
        public UserRepository(DataContext context) : base(context) { }

        public override async Task<bool> Exists(User obj, ContextSession session)
        {
            var context = GetContext(session);
            return await context.Set<User>().Where(x => x.Id == obj.Id).AsNoTracking().CountAsync() > 0;
        }

        public override async Task<User> Edit(User obj, ContextSession session)
        {
            var objectExists = await Exists(obj, session);
            var context = GetContext(session);
            context.Entry(obj).State = objectExists ? EntityState.Modified : EntityState.Added;

            if (string.IsNullOrEmpty(obj.Password))
            {
                context.Entry(obj).Property(x => x.Password).IsModified = false;
            }
            await context.SaveChangesAsync();
            return obj;
        }

        public async Task<IEnumerable<User>> GetAll(ContextSession session)
        {
            var context = GetContext(session);
            return await context.Set<User>()
                .Include(u => u.Settings)
                .Include(u => u.UserRoles)
                .ThenInclude(x => x.Role)
                .AsNoTracking()
                .ToListAsync();
        }

        public override async Task<User> Get(Guid id, ContextSession session)
        {
            var context = GetContext(session);
            return await context.Set<User>()
                   .Where(obj => obj.Id == id)
                   .Include(u => u.Settings)
                   .Include(u => u.UserRoles)
                       .ThenInclude(x => x.Role)
                   .AsNoTracking()
                   .FirstOrDefaultAsync();
        }

        public async Task<User> GetByUserName(string username, ContextSession session)
        {
            var context = GetContext(session);
            return await context.Set<User>()
                    .Where(obj => obj.UserName == username)
                    .Include(u => u.Settings)
                    .Include(u => u.UserRoles)
                        .ThenInclude(x => x.Role)
                    .AsNoTracking()
                    .FirstOrDefaultAsync();
        }

        public async Task<User> GetByEmail(string email, ContextSession session)
        {
            var context = GetContext(session);
            return await context.Set<User>()
                    .Where(obj => obj.Email == email)
                    .Include(u => u.Settings)
                    .Include(u => u.UserRoles)
                        .ThenInclude(x => x.Role)
                    .AsNoTracking()
                    .FirstOrDefaultAsync();
        }
    }
}