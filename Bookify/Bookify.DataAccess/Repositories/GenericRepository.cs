using Bookify.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Bookify.DataAccess
{
    internal class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        internal BookifyContext _ctx;
        internal DbSet<TEntity> _dbSet;

        public GenericRepository(BookifyContext ctx)
        {
            _ctx = ctx;
            _dbSet = ctx.Set<TEntity>();
        }

        public virtual async void Add(TEntity entity)
        {
            this._dbSet.Add(entity);
            await this._ctx.SaveChangesAsync();
        }

        public virtual async Task<TEntity> Find(object id)
        {
            return await this._dbSet.FindAsync(id);
        }

        public virtual async Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>> predicate)
        {
            var set = this._ctx.Set<TEntity>().AsQueryable();
            return await set.Where(predicate).ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            return await this._dbSet.ToListAsync();
        }

        public virtual async void Remove(TEntity entity)
        {
            this.Remove(entity);
            await this._ctx.SaveChangesAsync();
        }

        public virtual async Task<int> SaveChanges()
        {
            return await this._ctx.SaveChangesAsync();
        }

        public virtual async void Update(TEntity entity)
        {
            this._ctx.Entry(entity).State = EntityState.Modified;
            await this._ctx.SaveChangesAsync();
        }
    }
}
