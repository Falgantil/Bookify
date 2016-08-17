using System.Threading.Tasks;
using Bookify.Models;

namespace Bookify.Core.Interfaces.Repositories
{
    public interface IPersonRepository : IGenericRepository<Person>
    {
        Task<Person> CreatePersonIfNotExists(string email);
    }
}
