using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookify.Models;

namespace Bookify.Core.Interfaces.Services
{
    public interface IAuthenticationService
    {
        Task<string> Login(string email, string password);
        Task<Person> Register(string username, string firstName, string lastName, string email, string password);
    }
}
