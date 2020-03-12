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
    public class UserRoleRepository : IUserRoleRepository<UserRole>
    {
        protected DataContext dbContext;

        public UserRoleRepository(DataContext context)
        {
            dbContext = context;
        }

        protected DataContext GetContext(ContextSession session)
        {
            dbContext.Session = session;
            return dbContext;
        }

        public async Task<UserRole> Add(UserRole userRole, ContextSession session)
        {
            var context = GetContext(session);
            context.Entry(userRole).State = EntityState.Added;
            await context.SaveChangesAsync();

            await context.Entry(userRole).Reference(ur => ur.Role).LoadAsync();

            return userRole;
        }

        public async Task Delete(Guid userId, Guid roleId, ContextSession session)
        {
            var context = GetContext(session);
            var itemToDelete = new UserRole { UserId = userId, RoleId = roleId };
            context.Entry(itemToDelete).State = EntityState.Deleted;
            await context.SaveChangesAsync();
        }

        public async Task<UserRole> Get(Guid userId, Guid roleId, ContextSession session)
        {
            var context = GetContext(session);
            return await context.Set<UserRole>().Where(obj => obj.RoleId == roleId && obj.UserId == userId).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<IList<string>> GetByUserId(Guid userId, ContextSession session)
        {
            var context = GetContext(session);
            return await context.Set<UserRole>()
                .AsNoTracking()
                .Where(obj => obj.UserId == userId)
                .Include(obj => obj.Role)
                .Select(obj => obj.Role.Name)
                .ToListAsync();
        }
    }
}