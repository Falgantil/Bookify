using System.Threading.Tasks;

using Bookify.Common.Commands.Auth;
using Bookify.Common.Models;
using Bookify.DataAccess.Models;

namespace Bookify.DataAccess.Interfaces.Repositories
{
    public interface IPersonRepository
    {
        Task<PersonDto> CreatePersonIfNotExists(string email);

        Task<PersonDto> GetByEmail(string email);

        Task<PersonDto> CreatePerson(CreateAccountCommand command);

        Task<PersonDto> GetById(int userId);

        Task<PersonDto> EditPerson(int id, UpdatePersonCommand command);
    }
}
