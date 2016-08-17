using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Bookify.DataAccess.Interfaces.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
    }
}
