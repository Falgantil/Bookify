using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Bookify.Core
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<TEntity> Find(int id);
        Task<IQueryable<TEntity>> GetAll();
        Task<IQueryable<TEntity>> Get(Expression<Func<TEntity, bool>> predicate);
        Task Add(TEntity entity);
        Task Update(TEntity entity);
        Task Remove(TEntity entity);
        Task<int> SaveChanges();
    }
}
