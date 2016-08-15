using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Bookify.Core.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<TEntity> Find(object id);
        Task<IQueryable<TEntity>> GetAll();
        Task<IQueryable<TEntity>> Get(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> Add(TEntity entity);
        Task<TEntity> Update(TEntity entity);
        Task Remove(TEntity entity);
        Task<int> SaveChanges();
    }
}
