using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using Bookify.Core.Exceptions;
using Bookify.Core.Interfaces;
using Bookify.Core.Interfaces.Services;
using Bookify.Models;

namespace Bookify.Core.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IPersonRepository _personRepository;

        public AuthenticationService(IPersonRepository personRepository)
        {
            this._personRepository = personRepository;
        }

        public async Task<string> Login(string email, string password)
        {
            var person = (await this._personRepository.Get(k => k.Email == email)).SingleOrDefault();
            if (person == null || !EncryptSha512.VerifyPassword(password, person.Password))
            {
                throw new AuthenticationException("Invalid email & password");
            }

            var now = DateTimeOffset.Now.ToUniversalTime();
            var unixNow = now.ToUnixTimeSeconds();
            var unixExpired = now.AddYears(1).ToUnixTimeSeconds();
            var payload = new Dictionary<string, object>
            {
                { "issdate", unixNow },
                { "expdate", unixExpired },
                { "userid", person.Id }
            };
            return JWT.JsonWebToken.Encode(payload, SecretKey, JWT.JwtHashAlgorithm.HS256);
        }

        public async Task<Person> Register(string username, string firstName, string lastName, string email, string password)
        {
            var person = (await this._personRepository.Get(p => p.Email == email)).FirstOrDefault();
            if (person != null)
            {
                throw new PersonRegistrationException(email);
            }

            person = await this._personRepository.Add(new Person
            {
                Firstname = firstName,
                Lastname = lastName,
                Email = email,
                Password = EncryptSha512.GetPassword(password),
                Alias = username
            });
            return person;
        }

        private const string SecretKey = "yuoypr3QeRZkwGcfj24y4XGODwnkXOy1";
        
        public async Task<Person> VerifyToken(string accessToken)
        {
            var obj = JWT.JsonWebToken.DecodeToObject<Dictionary<string, object>>(accessToken, SecretKey);
            var issuedDate = DateTimeOffset.FromUnixTimeSeconds((long)obj["issdate"]);
            if (issuedDate > DateTimeOffset.Now)
            {
                throw new InvalidAccessTokenException();
            }

            var expirationDate = DateTimeOffset.FromUnixTimeSeconds((long)obj["expdate"]);
            if (expirationDate.ToLocalTime() < DateTimeOffset.Now)
            {
                throw new InvalidAccessTokenException();
            }

            var userId = (int)obj["userid"];
            return await this._personRepository.Find(userId);
        }
    }
}
