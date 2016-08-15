using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Bookify.Core;
using Bookify.Core.Interfaces;

namespace Bookify.DataAccess.Repositories
{
    public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        internal BookifyContext _ctx;
        internal DbSet<TEntity> _dbSet;

        public GenericRepository(BookifyContext ctx)
        {
            this._ctx = ctx;
            this._dbSet = ctx.Set<TEntity>();
        }

        public virtual async Task<TEntity> Add(TEntity entity)
        {
            entity = this._dbSet.Add(entity);
            await this._ctx.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<TEntity> Find(object id)
        {
            return await this._dbSet.FindAsync(id);
        }

        public virtual async Task<IQueryable<TEntity>> Get(Expression<Func<TEntity, bool>> predicate)
        {
            var result = await this._dbSet.Where(predicate).ToListAsync();
            return result.AsQueryable();
        }

        public virtual async Task<IQueryable<TEntity>> GetAll()
        {
            var result =await this._dbSet.ToListAsync();
            return result.AsQueryable();
        }

        public virtual async Task Remove(TEntity entity)
        {
            this._ctx.Entry(entity).State = EntityState.Deleted;
            await this._ctx.SaveChangesAsync();
        }

        public virtual async Task<int> SaveChanges()
        {
            return await this._ctx.SaveChangesAsync();
        }

        public virtual async Task<TEntity> Update(TEntity entity)
        {
            var dbEntityEntry = this._ctx.Entry(entity);
            dbEntityEntry.State = EntityState.Modified;
            await this._ctx.SaveChangesAsync();
            return dbEntityEntry.Entity;
        }
    }
}
