using System.Threading.Tasks;
using Bookify.Common.Commands.Auth;
using Bookify.Common.Models;

namespace Bookify.Common.Repositories
{
    public interface IPersonRepository
    {
        /// <summary>
        /// Creates a person object in the database if the email is not used
        /// </summary>
        /// <param name="email">email is used for validation</param>
        /// <returns></returns>
        Task<PersonDto> CreatePersonIfNotExists(string email);
        /// <summary>
        /// search for person object using the email as validator
        /// </summary>
        /// <param name="email">email is used for validation</param>
        /// <returns></returns>
        Task<PersonDto> GetByEmail(string email);
        /// <summary>
        /// Creates a person in the database 
        /// </summary>
        /// <param name="command">comamnd includes all the parameters for creating hte person</param>
        /// <returns></returns>
        Task<PersonDto> CreatePerson(CreateAccountCommand command);
        /// <summary>
        /// get the person by id
        /// </summary>
        /// <param name="id">identifier used for validation</param>
        /// <returns></returns>
        Task<PersonDto> GetById(int id);
        /// <summary>
        /// updates a exsitsing person 
        /// </summary>
        /// <param name="id">identifier used for validation</param>
        /// <param name="command">contains the changes field</param>
        /// <returns></returns>
        Task<PersonDto> EditPerson(int id, UpdatePersonCommand command);
        /// <summary>
        /// used for new subsciption
        /// </summary>
        /// <param name="personId">id of a existsing person</param>
        /// <param name="paid">amount what the person paid for the subsciption</param>
        /// <returns></returns>
        Task Subscibe(int personId, decimal paid);
        /// <summary>
        /// checks if the person has a subscription running now
        /// </summary>
        /// <param name="personId">id of a existsing person</param>
        /// <returns></returns>
        Task<bool> HasSubscription(int personId);
    }
}
