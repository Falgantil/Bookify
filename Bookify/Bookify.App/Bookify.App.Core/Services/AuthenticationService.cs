using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using Bookify.App.Core.Helpers;
using Bookify.App.Core.Interfaces.Services;
using Bookify.Models;

using ModernHttpClient;

namespace Bookify.App.Core.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        public event EventHandler<Person> AuthChanged;


        public Person LoggedOnAccount { get; private set; }

        public async Task<Person> Authenticate(string username, string password)
        {
            await Task.Delay(1500);

            var person = new Person
            {
                Firstname = "Bjarke",
                Lastname = "Søgaard"
            };
            this.LoggedOnAccount = person;
            return person;
        }
    }
}