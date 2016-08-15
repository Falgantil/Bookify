using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookify.Core
{
    public interface IAuthenticationService
    {
        Task<string> CreateAuthToken(string username, string password);
    }
}