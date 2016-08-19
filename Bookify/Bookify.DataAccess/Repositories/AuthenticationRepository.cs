using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Bookify.Common.Commands.Auth;
using Bookify.Common.Exceptions;
using Bookify.Common.Models;
using Bookify.Common.Repositories;
using Bookify.DataAccess.Models;

namespace Bookify.DataAccess.Repositories
{
    public class AuthenticationRepository : GenericRepository<Person>, IAuthenticationRepository
    {
        private const string SecretKey = "yuoypr3QeRZkwGcfj24y4XGODwnkXOy1";
        private const string Issdate = "issdate";
        private const string Expiredate = "expdate";
        private const string UserId = "userid";

        public AuthenticationRepository(BookifyContext context)
            : base(context)
        {
        }

        public async Task<AuthTokenDto> Login(AuthenticateCommand command)
        {
            var queryable = await this.Get(p => p.Email == command.Email);
            var person = await queryable.SingleOrDefaultAsync();
            if (person == null || !EncryptSha512.VerifyPassword(command.Password, person.Password))
            {
                throw new InvalidCredentialsException("Invalid email & password");
            }

            var now = DateTimeOffset.Now.ToUniversalTime();
            var unixNow = now.ToUnixTimeSeconds();
            var unixExpired = now.AddYears(1).ToUnixTimeSeconds();
            var payload = new Dictionary<string, object>
            {
                { Issdate, unixNow },
                { Expiredate, unixExpired },
                { UserId, person.Id }
            };
            var token = JWT.JsonWebToken.Encode(payload, SecretKey, JWT.JwtHashAlgorithm.HS256);
            return new AuthTokenDto
            {
                Token = token,
                Roles = person.Roles.Select(x=>x.ToPersonRoleDto())
            };
        }

        public async Task<PersonDto> Register(CreateAccountCommand command)
        {
            var queryable = await this.Get(p => p.Email == command.Email);
            var person = await queryable.FirstOrDefaultAsync();
            if (person != null)
            {
                throw new BadRequestException("An account already exists with the specified email.");
            }

            var entity = new Person
            {
                Firstname = command.FirstName,
                Lastname = command.LastName,
                Email = command.Email,
                Password = EncryptSha512.GetPassword(command.Password),
                Alias = command.Username
            };
            entity = await this.Add(entity);
            return entity.ToDto();
        }

        public async Task<PersonAuthDto> VerifyToken(string accessToken)
        {
            var obj = JWT.JsonWebToken.DecodeToObject<Dictionary<string, object>>(accessToken, SecretKey);
            var issDate = long.Parse(obj[Issdate].ToString());
            var issuedDate = DateTimeOffset.FromUnixTimeSeconds(issDate);
            if (issuedDate > DateTimeOffset.Now)
            {
                throw new InvalidAccessTokenException();
            }

            var expDate = long.Parse(obj[Expiredate].ToString());
            var expirationDate = DateTimeOffset.FromUnixTimeSeconds(expDate);
            if (expirationDate.ToLocalTime() < DateTimeOffset.Now)
            {
                throw new InvalidAccessTokenException();
            }

            var userId = (int) obj[UserId];
            var userQuery = await this.Get(x => x.Id == userId);
            userQuery = userQuery.Include(x => x.Roles);
            var user = userQuery.Single();
            if (user == null)
                throw new NullReferenceException();
            return new PersonAuthDto
            {
                PersonDto = user.ToDto(),
                AuthTokenDto = new AuthTokenDto
                {
                    Token = accessToken,
                    Roles = user.Roles.Select(x => x.ToPersonRoleDto())
                }
            };
                
        }
    }
}
