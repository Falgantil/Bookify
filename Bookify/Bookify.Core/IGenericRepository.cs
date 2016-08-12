using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Bookify.Core
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<TEntity> Find(object id);
        Task<IEnumerable<TEntity>> GetAll();
        Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);

        Task<int> SaveChanges();
    }
}
