using System;
using System.Threading.Tasks;
using Common.Entities;
using Common.Entities.System;
using Microsoft.EntityFrameworkCore;

namespace Common.DataAccess.EFCore.Repositories
{
    public abstract class BaseRepository<TType, TContext>
        where TType : BaseEntity, new()
        where TContext : DataContext
    {
        protected TContext dbContext;

        protected BaseRepository(TContext context)
        {
            this.dbContext = context;
        }

        protected TContext GetContext(ContextSession session)
        {
            dbContext.Session = session;
            return dbContext;
        }

        public abstract Task<TType> Get(Guid id, ContextSession session);
        public abstract Task<bool> Exists(TType obj, ContextSession session);

        public virtual async Task<TType> Edit(TType obj, ContextSession session)
        {
            var objectExists = await Exists(obj, session);
            var context = GetContext(session);

            context.Entry(obj).State = objectExists ? EntityState.Modified : EntityState.Added;
            await context.SaveChangesAsync();
            return obj;

        }

        public virtual async Task Delete(Guid id, ContextSession session)
        {
            var context = GetContext(session);

            var itemToDelete = await Get(id, session);
            itemToDelete.IsDelete = true;
            context.Entry(itemToDelete).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }
    }
}