using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Bookify.Common.Repositories;

namespace Bookify.DataAccess.Repositories
{
    public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected BookifyContext Context { get; }

        private readonly DbSet<TEntity> dbSet;

        protected GenericRepository(BookifyContext context)
        {
            this.Context = context;
            this.dbSet = context.Set<TEntity>();
        }

        protected virtual async Task<TEntity> Add(TEntity entity)
        {
            entity = this.dbSet.Add(entity);
            await this.Context.SaveChangesAsync();
            return entity;
        }

        protected virtual async Task<TEntity> Find(int id)
        {
            return await this.dbSet.FindAsync(id);
        }

        protected virtual async Task<IQueryable<TEntity>> Get(Expression<Func<TEntity, bool>> predicate)
        {
            var result = this.dbSet.Where(predicate);
            return result.AsQueryable();
        }

        protected virtual async Task<IQueryable<TEntity>> GetAll()
        {
            return this.dbSet.AsQueryable();
        }

        protected virtual async Task Remove(TEntity entity)
        {
            this.Context.Entry(entity).State = EntityState.Deleted;
            await this.Context.SaveChangesAsync();
        }

        protected virtual async Task<int> SaveChanges()
        {
            return await this.Context.SaveChangesAsync();
        }

        protected virtual async Task<TEntity> Update(TEntity entity)
        {
            var dbEntityEntry = this.Context.Entry(entity);
            dbEntityEntry.State = EntityState.Modified;
            await this.Context.SaveChangesAsync();
            return dbEntityEntry.Entity;
        }
    }
}
