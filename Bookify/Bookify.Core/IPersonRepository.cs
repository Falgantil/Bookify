using Bookify.Models;
using System.Threading.Tasks;

namespace Bookify.Core
{
    public interface IPersonRepository : IGenericRepository<Person>
    {
        Task<Person> CreatePersonIfNotExists(string email);
    }
}
