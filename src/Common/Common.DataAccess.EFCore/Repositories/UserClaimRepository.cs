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
    public class UserClaimRepository : IUserClaimRepository<UserClaim>
    {
        private DataContext dbContext;

        public UserClaimRepository(DataContext context)
        {
            dbContext = context;
        }

        protected DataContext GetContext(ContextSession session)
        {
            dbContext.Session = session;
            return dbContext;
        }

        public async Task<UserClaim> Add(UserClaim userClaim, ContextSession session)
        {
            var context = GetContext(session);

            context.Entry(userClaim).State = EntityState.Added;
            await context.SaveChangesAsync();
            return userClaim;
        }

        public async Task<IList<UserClaim>> EditMany(IList<UserClaim> userClaims, ContextSession session)
        {
            var context = GetContext(session);

            foreach (var uc in userClaims)
            {
                context.Entry(uc).State = EntityState.Modified;
            }

            await context.SaveChangesAsync();

            return userClaims;
        }

        public async Task Delete(Guid userId, string claimType, string claimValue, ContextSession session)
        {
            var context = GetContext(session);

            var itemsToDelete = await context.Set<UserClaim>().Where(cl => cl.UserId == userId && cl.ClaimType == claimType && cl.ClaimValue == claimValue).ToListAsync();
            context.Set<UserClaim>().RemoveRange(itemsToDelete);
            await context.SaveChangesAsync();
        }

        public async Task<IList<UserClaim>> GetByUserId(Guid userId, ContextSession session)
        {
            var context = GetContext(session);

            var list = await context.Set<UserClaim>()
                    .AsNoTracking()
                    .Where(obj => obj.UserId == userId)
                    .ToListAsync();

            return list.ToList();
        }

        public async Task<IList<UserClaim>> GetList(Guid userId, string claimType, string claimValue, ContextSession session)
        {
            var context = GetContext(session);

            var list = await context.Set<UserClaim>()
                .AsNoTracking()
                .Where(obj => obj.UserId == userId && obj.ClaimType == claimType && obj.ClaimValue == claimValue)
                .ToListAsync();

            return list.ToList();
        }
    }
}